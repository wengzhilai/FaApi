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
            List<ApiResource> resource = new List<ApiResource>();

            resource.Add(new ApiResource("wzl", "123456"));

            return resource.ToArray();
        }
        /// <summary>
        /// 定义受信任的客户端 Client
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            List<Client> clients = new List<Client>();


            Client client = new Client();
            client.ClientId = "js";
            List<Secret> clientSecrets = new List<Secret>();
            clientSecrets.Add(new Secret("secret".Sha256()));

            client.ClientSecrets = clientSecrets.ToArray();

            client.AllowedGrantTypes = GrantTypes.ClientCredentials;

            client.AllowedScopes = new[] { "api" };

            clients.Add(client);
            return clients.ToArray();
        }
    }
}
