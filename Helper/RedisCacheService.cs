using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helper;
using Newtonsoft.Json;
using StackExchange.Redis;

public class RedisCacheService : ICacheService
{

    protected IDatabase _cache_read;
    protected IDatabase _cache_write;

    private ConnectionMultiplexer _connection_read;
    private ConnectionMultiplexer _connection_write;


    public RedisCacheService()
    {
        _connection_read = GetManager(AppSettingsManager.self.RedisConfig.readRedisstr);
        _connection_write = GetManager(AppSettingsManager.self.RedisConfig.writeRedisstr);
        _cache_read = _connection_read.GetDatabase();
        _cache_write = _connection_write.GetDatabase();
    }

    private ConnectionMultiplexer GetManager(string connectionString = null)
    {
        connectionString = connectionString ?? AppSettingsManager.self.RedisConfig.readRedisstr;
        var connect = ConnectionMultiplexer.Connect(connectionString);
        return connect;
    }

    public string GetKeyForRedis(string key)
    {
        return key;
    }

    #region 检测
    /// <summary>
    /// 验证缓存项是否存在
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public bool Exists(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        return _cache_write.KeyExists(GetKeyForRedis(key));
    }
    #endregion

    #region 添加缓存

    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
    /// <param name="expiressAbsoulte">绝对过期时长</param>
    /// <returns></returns>
    public bool Add<T>(string key, T inObj, TimeSpan? expiresSliding = null, TimeSpan? expiressAbsoulte = null)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        var allProperties = inObj.GetType().GetProperties();
        var allField = allProperties.Select(x => (RedisValue)x.Name).ToArray();
        RedisValue[] values = _cache_write.HashGet(key, allField);
        List<HashEntry> allKv = new List<HashEntry>();
        for (int i = 0; i < allProperties.Count(); i++)
        {
            var item = allProperties[i];
            object value = item.GetValue(inObj);
            if (value != null && value != DBNull.Value)
            {
                allKv.Add(new HashEntry(item.Name, value.ToString()));
            }
        }
        _cache_write.HashSet(key, allKv.ToArray());
        return _cache_write.KeyExpire(key, expiressAbsoulte);

    }

    public bool Add(string key, string inStr,TimeSpan? expiressAbsoulte = null)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
       
        _cache_write.StringSet(key, inStr);
        return _cache_write.KeyExpire(key, expiressAbsoulte);

    }

    #endregion

    #region 删除缓存

    /// <summary>
    /// 删除缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public bool Remove(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        return _cache_write.KeyDelete(GetKeyForRedis(key));
    }
    /// <summary>
    /// 批量删除缓存
    /// </summary>
    /// <param name="key">缓存Key集合</param>
    /// <returns></returns>
    public void RemoveAll(IEnumerable<string> keys)
    {
        if (keys == null)
        {
            throw new ArgumentNullException(nameof(keys));
        }

        keys.ToList().ForEach(item => Remove(GetKeyForRedis(item)));
    }
    #endregion

    #region 获取缓存
    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <returns></returns>
    public T Get<T>(string key) where T : class, new()
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        T reEnt = new T();
        var allProperties = reEnt.GetType().GetProperties();
        var allField = allProperties.Select(x => (RedisValue)x.Name).ToArray();
        RedisValue[] values = _cache_read.HashGet(key, allField);

        //如果没有获取到值则奶出
        if(values.Count(x=>x.HasValue)<1) return null;

        for (int i = 0; i < allField.Count(); i++)
        {
            if(!allField[i].HasValue) continue;
            var item = allProperties.SingleOrDefault(x => x.Name == allField[i]);
            if (item == null) continue;
            try
            {
                var piType = item.PropertyType;
                if (!piType.IsGenericType)
                {
                    item.SetValue(reEnt, Convert.ChangeType(values[i], piType), null);
                }
                else
                {
                    Type genericTypeDefinition = piType.GetGenericTypeDefinition();
                    item.SetValue(reEnt, Convert.ChangeType(values[i], Nullable.GetUnderlyingType(piType)));
                }
            }
            catch
            {
            }
        }
        return reEnt;
    }


    public string Get(string key)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        var reStr= _cache_read.StringGet(key);

        return reStr;
    }

    /// <summary>
    /// 获取缓存集合
    /// </summary>
    /// <param name="keys">缓存Key集合</param>
    /// <returns></returns>
    public List<T> GetAll<T>(List<string> keys) where T : class, new()
    {
        if (keys == null)
        {
            throw new ArgumentNullException(nameof(keys));
        }

        var dict = new List<T>();

        keys.ToList().ForEach(item => dict.Add(Get<T>(item)));

        return dict;
    }
    #endregion

    #region 修改缓存

    /// <summary>
    /// 修改缓存
    /// </summary>
    /// <param name="key">缓存Key</param>
    /// <param name="value">新的缓存Value</param>
    /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间）</param>
    /// <param name="expiressAbsoulte">绝对过期时长</param>
    /// <returns></returns>
    public bool Replace<T>(string key, T value, TimeSpan? expiresSliding = null, TimeSpan? expiressAbsoulte = null)
    {
        if (key == null)
        {
            throw new ArgumentNullException(nameof(key));
        }
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        if (Exists(key))
            if (!Remove(key)) return false;

        return Add(key, value, expiresSliding, expiressAbsoulte);
    }


    public bool ReplaceHashSetKey<T>(string name, string key, object value, TimeSpan? expiresSliding = null, TimeSpan? expiressAbsoulte = null) where T : class, new()
    {
        return _cache_write.HashSet(name, key, value.ToString());
    }

    #endregion

    public void Dispose()
    {
        if (_connection_read != null)
            _connection_read.Dispose();

        if (_connection_write != null)
            _connection_write.Dispose();
        GC.SuppressFinalize(this);
    }

}