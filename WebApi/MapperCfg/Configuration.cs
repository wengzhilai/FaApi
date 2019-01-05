

using AutoMapper;

namespace WebApi.MapperCfg
{
    /// <summary>
    /// 
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public static MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new Profiles.UserInfoProfile());
        });
    }
}