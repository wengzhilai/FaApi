using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 配置
        /// </summary>
        public static ConfigWebConfig WebConfig { get; set; } = new ConfigWebConfig();
    }

    /// <summary>
    /// 配置
    /// </summary>
    public class ConfigWebConfig
    {
        /// <summary>
        /// 推广一个客户的费用
        /// </summary>
        public int ClientPrice { get; set; }
    }
}
