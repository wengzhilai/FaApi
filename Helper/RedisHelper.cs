using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using StackExchange.Redis;

namespace Helper
{

    public class RedisReadHelper
    {
        private static IDatabase cache = GetFactionConn_Read.GetDatabase();
        #region 获取连接

        private static ConnectionMultiplexer _connection;

        private static ConnectionMultiplexer GetFactionConn_Read
        {
            get
            {
                if (_connection == null)
                {
                    _connection = GetManager();
                }
                return _connection;
            }
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? AppSettingsManager.RedisConfig.readRedisstr;
            var connect = ConnectionMultiplexer.Connect(connectionString);
            return connect;
        }
        #endregion

        /// <summary>
        /// 根据名称获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public async static Task<Tuple<T, Result>> GetObject<T>(string name, List<string> keyArry = null) where T : new()
        {
            Result result = new Result();
            T reEnt = new T();
            var allProperties = reEnt.GetType().GetProperties();
            var allField = allProperties.Where(x => (keyArry == null || keyArry.Count() == 0 || keyArry.Contains(x.Name))).Select(x => (RedisValue)x.Name).ToArray();
            RedisValue[] values = await cache.HashGetAsync(name, allField);
            int errNum = values.Count(x => !x.HasValue);
            if (errNum > 0)
            {
                result.IsSuccess = false;
                result.Msg = string.Format("取的数，有{0}个错误", errNum);
                return Tuple.Create<T, Result>(reEnt, result);
            }
            for (int i = 0; i < allField.Count(); i++)
            {
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
                catch (Exception e)
                {
                    result.IsSuccess = false;
                    result.Msg = string.Format("对象[{0}]的性[{1}]值[{2}]转换无效:", reEnt.GetType().Name, item.Name, values[i], e.Message);
                    LogHelper.WriteErrorLog<RedisReadHelper>(result.Msg + e.ToString());
                }
            }
            result.IsSuccess = true;
            return Tuple.Create<T, Result>(reEnt, result);
        }

        /// <summary>
        /// 根据名称获取，对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        public async static Task<List<T>> GetObjects<T>(List<string> names, List<string> keyArry = null) where T : new()
        {
            List<T> reEnts = new List<T>();
            foreach (var name in names)
            {
                Result result = new Result();
                var tmp=await GetObject<T>(name, keyArry);
                result=tmp.Item2;
                reEnts.Add(tmp.Item1);
            }
            return reEnts;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async static Task<string> StringGet(string key)
        {
            return await cache.StringGetAsync(key);
        }

        public async static Task<string> HashGetKey(string hash, string key)
        {
            return await cache.HashGetAsync(hash, key);
        }


        /// <summary>
        /// 判断Key是否存在
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Task<bool> HashExists(string hash, string key)
        {
            return cache.HashExistsAsync(hash, key);
        }

        public static Task<bool> KeyExists(string key)
        {
            return cache.KeyExistsAsync(key);
        }
    }
    public class RedisWriteHelper
    {
        private static IDatabase cache = GetFactionConn_Write.GetDatabase();
        #region 获取连接

        private static ConnectionMultiplexer _connection;
        private static readonly object SyncObject = new object();

        private static ConnectionMultiplexer GetFactionConn_Write
        {
            get
            {
                if (_connection == null)
                {
                    _connection = GetManager();
                }
                return _connection;
            }
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? AppSettingsManager.RedisConfig.writeRedisstr;
            var connect = ConnectionMultiplexer.Connect(connectionString);
            return connect;
        }
        #endregion

        #region 操作hash数据类型
        /// <summary>
        /// 添加或更新hash数据的多个字段值(HashSet)
        /// </summary>
        /// <param name="hash">key</param>
        /// <param name="hashFields">需更新的字段和值</param>
        public static Task HashSetEntry(string hash, Dictionary<string, object> hashFields)
        {
            var usKv = hashFields.Where(x => x.Value != null).Select(x => new HashEntry(x.Key, x.Value.ToString())).ToArray();
            return cache.HashSetAsync(hash, usKv);
        }

        /// <summary>
        /// 实体类，保存到redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="inObj"></param>
        /// <param name="saveItems"></param>
        /// <returns></returns>
        public static Task SetObject<T>(string name, T inObj, List<string> saveItems = null) where T : new()
        {
            var allProperties = inObj.GetType().GetProperties();
            var allField = allProperties.Select(x => (RedisValue)x.Name).ToArray();
            RedisValue[] values = cache.HashGet(name, allField);
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
            return cache.HashSetAsync(name, allKv.ToArray());
        }

        /// <summary>
        /// 将key中存储的哈希中的字段设置为value。 如果key不存在，则创建一个包含哈希的新密钥。 如果哈希中已存在字段，则会覆盖该字段。
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static Task<bool> HashSetKey(string hash, string key, string value)
        {
            return cache.HashSetAsync(hash, key, value);
        }
        /// <summary>
        /// 设置Hash过期时间
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static Task<bool> HashExpire(string hash, TimeSpan span)
        {
            return cache.KeyExpireAsync(hash, span);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除根据Key删除
        /// </summary>
        /// <param name="hash">keyName</param>
        public static Task<bool> KeyDelete(string keyName)
        {
            return cache.KeyDeleteAsync(keyName);
        }

        /// <summary>
        /// 删除hask的Key
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task<bool> HashDelete(string hash, string key)
        {
            return cache.HashDeleteAsync(hash, key);
        }

        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Task<bool> StringHashSetKey(string hash, string key, string value)
        {
            return cache.HashSetAsync(hash, key, value);
        }
        #endregion

        #region 操作String数据类型
        /// <summary>
        /// set String类型
        /// </summary>
        /// <param name="key">string类型key值</param>
        /// <param name="value">值</param>
        /// <param name="timeSpan">当前key过期时间</param>
        public static Task<bool> SetStringKey(string key, string value, TimeSpan timeSpan)
        {
            return cache.StringSetAsync(key, value, timeSpan);//新建字段返回值1;修改字段返回值0
        }
        #endregion
    }

}