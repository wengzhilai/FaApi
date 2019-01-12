
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

        /// <summary>
        /// 验证配置
        /// </summary>
        /// <returns></returns>
        public static BaseConfig Config=new BaseConfig();

        public static RedisConfig RedisConfig=new RedisConfig();
        

    }

    /// <summary>
    /// 日志配置
    /// </summary>
    public class Logging{
        /// <summary>
        /// 日志文件路径
        /// </summary>
        /// <value></value>
        public string Log4netConfigPath { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        /// <value></value>
        public LogLevel LogLevel { get; set; }

    }

    /// <summary>
    ///  系统验证
    /// </summary>
    public class BaseConfig{
        /// <summary>
        /// 是否需要验证码
        /// </summary>
        /// <value></value>
        public bool VerifyCode { get; set; }
        /// <summary>
        /// 短信验证码有效时间
        /// </summary>
        /// <value></value>
        public int  VerifyExpireMinute{ get; set; }=30;

    }

    /// <summary>
    ///  Redis配置
    /// </summary>
    public class RedisConfig{
        /// <summary>
        /// 写的连接地址
        /// </summary>
        /// <value></value>
        public string writeRedisstr { get; set; }="45.32.134.176:6379,password=wengzhilai";
        /// <summary>
        /// 读取的连接地址
        /// </summary>
        /// <value></value>
        public string  readRedisstr{ get; set; }="45.32.134.176:6379,password=wengzhilai";

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