using System.Threading.Tasks;

namespace IRepository
{
    public interface IRedisRepository
    {
        /// <summary>
        /// 保存，用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool UserTokenSet(int userId, string key);

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UserTokenExists(int userId);

        /// <summary>
        /// 删除用户是的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool UserTokenDelete(int userId);

        /// <summary>
        /// 获取用户的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        string UserTokenGet(int userId);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="md5Str">主键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool CacheSet(string md5Str, string value);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        bool CasheExists(string md5Str);

      
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        bool CasheDelete(string md5Str);

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        string CasheGet(string md5Str);


    }
}