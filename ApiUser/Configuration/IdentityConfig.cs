using ApiUser.Helper;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace ApiUser.Configuration
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
            resources.Add(new ApiResource("UsersService", "用户服务API"));
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
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "UsersService" }
                },
                //Implicit模式Client配置，适用于SPA
                new Client
                {
                    ClientId = "Test",
                    ClientName = "Test",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    AccessTokenLifetime = 60 * 60,
                    AccessTokenType = AccessTokenType.Jwt,
                    RedirectUris =
                        {
                            "http://localhost:5003/callback.html",
                            "http://localhost:5003/silent.html"
                        },
                    PostLogoutRedirectUris = { "http://localhost:5003" },
                    AllowedCorsOrigins = { "http://localhost:5003" },
                    RequireConsent = false,
                    AllowedScopes =
                        {
                            "UsersService"//对应webapi里面的scope配置
                        }
                },
                //ResourceOwnerPassword模式Client配置，适用于App、Winform
                new Client
                {
                    ClientId = "Wpf",
                    ClientName = "App",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60 * 60,//单位s
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600 * 24,//以秒为单位滑动刷新令牌的生命周期。 
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "UsersService"
                    }
                },
                //自定义短信验证模式
                new Client
                {
                    ClientId = "sms",
                    ClientName = "sms",
                    ClientSecrets = { new Secret("123456".Sha256()) },
                    AccessTokenLifetime = 60 * 60,//单位s
                    AllowedGrantTypes = new[] { ExtensionGrantTypes.SMSGrantType }, //一个 Client 可以配置多个 GrantType
                    AbsoluteRefreshTokenLifetime = 2592000,//RefreshToken的最长生命周期,默认30天
                    RefreshTokenExpiration = TokenExpiration.Sliding,//刷新令牌时，将刷新RefreshToken的生命周期。RefreshToken的总生命周期不会超过AbsoluteRefreshTokenLifetime。
                    SlidingRefreshTokenLifetime = 3600 * 24,//以秒为单位滑动刷新令牌的生命周期。
                    AllowOfflineAccess = true,
                    AllowedScopes = new List<string>
                    {
                        "UsersService"
                    }
                },
                new Client
                {
                    ClientId = "SwaggerClientId",
                    ClientName = "Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris =
                    {
                        "http://localhost:5001/swagger/oauth2-redirect.html"//swagger回调地址
                    },
                    AllowedScopes = new List<string>
                    {
                        "UsersService"
                    }
                }

            };

        }

    }
}
