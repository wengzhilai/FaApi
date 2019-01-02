
namespace Helper
{
    /// <summary>
    /// 读取AppSetting文件
    /// </summary>
    public class AppSettingsManager
    {
        
        /// <summary>
        /// 获取jwt配置
        /// </summary>
        /// <returns></returns>
        public static JwtSettings JwtSettings = new JwtSettings();

        /// <summary>
        /// log4net配置
        /// </summary>
        /// <returns></returns>
        public static Logging Logging = new Logging();

    }


    public class Logging{
        public string Log4netConfigPath { get; set; }
        public LogLevel LogLevel { get; set; }

    }

    public class LogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Default { get; set; }
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