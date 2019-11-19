using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUser.Configuration
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            List<ApiResource> resource = new List<ApiResource>()
            {

            };

            resource.Add(new ApiResource("wzl", "123456"));

            return resource.ToArray();
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        /// <summary>
        /// 定义受信任的客户端 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();


            Client client = new Client()
            {
                RequireConsent = false,//如果不需要显示否同意授权 页面 这里就设置为false
                RedirectUris = { "http://localhost:5002/signin-oidc" },//登录成功后返回的客户端地址
                PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },//注销登录后返回的客户端地址
            };
            client.ClientId = "js";
            List<Secret> clientSecrets = new List<Secret>();
            clientSecrets.Add(new Secret("secret".Sha256()));

            client.ClientSecrets = clientSecrets.ToArray();

            client.AllowedGrantTypes = GrantTypes.ClientCredentials;

            client.AllowedScopes = new[] {
                "api",
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
             };

            clients.Add(client);
            return clients.ToArray();
        }
    }
}
