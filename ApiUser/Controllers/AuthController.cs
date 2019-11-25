using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace ApiUser.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;

        public AuthController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<String>> UserLogin(UserLoginDto inEnt)
        {
            Result<String> reobj = new Result<String>();

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:9001/");
            if (disco.IsError)
                return new Result<String>(false, disco.Error);
            var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                //获取Token的地址
                Address = disco.TokenEndpoint,
                //客户端Id
                ClientId = "pwd",
                //客户端密码
                ClientSecret = "clientsecret",
                GrantType= "password",
                UserName= inEnt.LoginName,
                Password= inEnt.Password,
            });

            if (token.IsError)
            {
                reobj.IsSuccess = false;
            }
            else
            {
                reobj.IsSuccess = true;

                reobj.Data = token.Json.TryGetString("access_token");
            }
            return reobj;
        }

        [HttpPost]
        public async Task<Result<String>> UserCodeLogin(UserCodeLoginDto inEnt)
        {
            Result<String> reobj = new Result<String>();

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:9001/");
            if (disco.IsError)
                return new Result<String>(false, disco.Error);
            var token = await client.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                //获取Token的地址
                Address = disco.TokenEndpoint,
                //客户端Id
                ClientId = "pwd",
                //客户端密码
                ClientSecret = "clientsecret",
                GrantType = "password",
                UserName = inEnt.LoginName,
                Password = inEnt.Password,
            });

            if (token.IsError)
            {
                reobj.IsSuccess = false;
            }
            else
            {
                reobj.IsSuccess = true;

                reobj.Data = token.Json.TryGetString("access_token");
            }
            return reobj;
        }


    }

    public class UserLoginDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    public class UserCodeLoginDto
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}