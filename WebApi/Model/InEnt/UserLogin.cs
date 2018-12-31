namespace WebApi.Model.InEnt
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// 登录名
        /// </summary>
        /// <value></value>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <value></value>
        public string Password { get; set; }
    }
}