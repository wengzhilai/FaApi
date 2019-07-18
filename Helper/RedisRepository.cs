using System.Threading.Tasks;

namespace Helper
{
    public class RedisRepository
    {
        private static string _userTokenKey = "UserToken_{0}";
        private static string _Data = "Data_{0}";

        /// <summary>
        /// 保存，用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool UserTokenSet<T>(int userId, string key)where T : class, new()
        {
            try
            {
                return Helper.RedisWriteHelper.HashSetKey<T>("UserToken", string.Format(_userTokenKey, userId), key);
            }
            catch
            {
                return true;
            }
        }



        /// <summary>
        /// 删除用户是的Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool UserTokenDelete(int userId)
        {
            try
            {
                return Helper.RedisWriteHelper.KeyDelete(string.Format(_userTokenKey, userId));

            }
            catch
            {
                return true;
            }
        }

    }
}