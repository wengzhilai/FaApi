using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Helper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace ApiUser.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IHttpClientFactory clientFactory;

        public LoginController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpOptions]
        public async Task<Result<String>> userLogin(UserLoginDto inEnt)
        {
            Result<String> reobj = new Result<String>();

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(AppSettingsManager.self.Idsvr4Url);
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
                reobj.success = false;
                reobj.msg = token.ErrorDescription;
            }
            else
            {
                reobj.success = true;
                
                reobj.code = token.AccessToken;

                var t = new JwtSecurityTokenHandler().ReadJwtToken(token.AccessToken);
                
                reobj.data = TypeChange.ObjectToStr(t.Payload);
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
            Dictionary<string, string> par=new Dictionary<string, string>();
            par.Add("phoneNumber",inEnt.LoginName);
            par.Add("smsCode",inEnt.Code);
            var token = await client.RequestTokenAsync(new TokenRequest()
            {
                //获取Token的地址
                Address = disco.TokenEndpoint,
                //客户端Id
                ClientId = "sms",
                //客户端密码
                ClientSecret = "123456",
                GrantType = "SMSGrantType",
                Parameters=par
            });

            if (token.IsError)
            {
                reobj.success = false;
                reobj.msg = token.ErrorDescription;
            }
            else
            {
                reobj.success = true;

                reobj.data = token.Json.TryGetString("access_token");
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