using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Idsvr4.Configuration
{
    public class IdentityConfig
    {
        /// <summary>
        /// 微服务中的API资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resources = new List<ApiResource>();
            resources.Add(new ApiResource("UsersService", "用户服务API", new List<string>() { JwtClaimTypes.Role }));
            resources.Add(new ApiResource("FileUpService", "文件上传服务API"));
            resources.Add(new ApiResource("ProductService", "产品服务API"));
            return resources;
        }

        /// <summary>
        /// 加载API资源和客户端资源
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),//必须要添加，否则报无效的scope错误 
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// 定义受信任的客户端 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //自定义短信验证模式
                new Client
                {
                    ClientId = "sms",
                    ClientName = "sms",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60 * 60,//单位s
                    AllowedGrantTypes = new[] { "SMSGrantType" }, //一个 Client 可以配置多个 GrantType
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600 * 24,//以秒为单位滑动刷新令牌的生命周期。
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "UsersService",
                        "FileUpService"
                    }
                },
                new Client
                {
                    ClientId = "pwd",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = new [] { 
                        "UsersService",
                        "FileUpService" 
                    }
                },
                

            };

        }

    }
}
