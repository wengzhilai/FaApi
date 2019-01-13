using System.Threading.Tasks;

namespace Repository
{
    public class RedisRepository
    {
        private string _userTokenKey = "UserToken_{0}";
        private string _Data = "Data_{0}";

        /// <summary>
        /// 保存，用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool UserTokenSet(int userId, string key)
        {
            Helper.RedisWriteHelper.HashSetKey("UserToken", string.Format(_userTokenKey, userId), key);
            return true;
        }

        /// <summary>
        /// 检测用户是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserTokenExists(int userId)
        {
            return Helper.RedisReadHelper.HashExists("UserToken", string.Format(_userTokenKey, userId));
        }

        /// <summary>
        /// 删除用户是的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserTokenDelete(int userId)
        {
            return Helper.RedisWriteHelper.HashDelete("UserToken", string.Format(_userTokenKey, userId));
        }

        /// <summary>
        /// 获取用户的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string UserTokenGet(int userId)
        {
            return Helper.RedisReadHelper.HashGetKey("UserToken", string.Format(_userTokenKey, userId));
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="md5Str">主键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public bool CacheSet(string md5Str, string value)
        {
            Helper.RedisWriteHelper.SetStringKey(string.Format(_Data, md5Str), value,new System.TimeSpan(0,0,0,20));
            return true;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        public bool CasheExists(string md5Str)
        {
            return Helper.RedisReadHelper.HashExists(string.Format(_Data, md5Str), string.Format(_Data, md5Str));
        }

      
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        public bool CasheDelete(string md5Str)
        {
            return Helper.RedisWriteHelper.KeyDelete(string.Format(_Data, md5Str));
        }

        /// <summary>
        /// 设置缓存值
        /// </summary>
        /// <param name="md5Str"></param>
        /// <returns></returns>
        public string CasheGet(string md5Str)
        {
            return Helper.RedisReadHelper.StringGet(string.Format(_Data, md5Str));
        }


    }
}