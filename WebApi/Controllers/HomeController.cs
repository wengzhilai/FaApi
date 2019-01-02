using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Comon;
using Microsoft.Extensions.Options;
using WebApi.Model.InEnt;
using Helper;

namespace WebApi.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        [AllowAnonymous]
        public string Index()
        {
            return "home-index";
        }
        [HttpGet]
        [AllowAnonymous]
        [HttpPost]
        public string Error()
        {
            return "home-Error";
        }
        [Authorize]
        [HttpGet]
        [HttpPost]
        public string[] AuthPage()
        {
            return new string[] { User.Identity.Name, User.Identity.AuthenticationType };
        }


        [HttpPost]
        public string UserLogin(UserLogin inEnt)
        {
            if (inEnt.UserName == "AngelaDaddy" && inEnt.Password == "123456")
            {
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss: The issuer of the token，token 是给谁的  发送者
                    audience: 接收的
                    sub: The subject of the token，token 主题
                    exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                    iat: Issued At。 token 创建时间， Unix 时间戳格式
                    jti: JWT ID。针对当前 token 的唯一标识
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                try
                {
                    var claims = new Claim[]
                    {
                        new Claim(ClaimTypes.Name, inEnt.UserName),
                        new Claim(ClaimTypes.Role, "admin, Manage")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.JwtSettings.SecretKey));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        AppSettingsManager.JwtSettings.Issuer,
                        AppSettingsManager.JwtSettings.Audience,
                        claims,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        creds);
                    return JsonConvert.SerializeObject(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                catch (Exception ex)
                {
                    return (ex.Message);
                }

            }

            return "用户名密码错误";
        }

    }
}