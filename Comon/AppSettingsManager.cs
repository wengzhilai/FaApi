using Microsoft.Extensions.Configuration;

namespace FaApi.Comon
{
    /// <summary>
    /// 读取AppSetting文件
    /// </summary>
    public class AppSettingsManager
    {
        /// <summary>
        /// 获取mongodb设置
        /// </summary>
        public static MongodbHost MongoSettings = new MongodbHost();
        
        /// <summary>
        /// 获取jwt配置
        /// </summary>
        /// <returns></returns>
        public static JwtSettings JwtSettings = new JwtSettings();

    }




    /// <summary>
    /// jwt配置
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 证书颁发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 允许使用的角色
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 加密字符串
        /// </summary>
        public string SecretKey { get; set; }
    }
}