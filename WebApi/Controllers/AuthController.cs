using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using WebApi.Model;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using WebApi.Model.InEnt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [Route("api/auth/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        IConfiguration config;
        ILoginRepository user;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_user"></param>
        public AuthController(IConfiguration _config, ILoginRepository _user)
        {
            config = _config;
            user = _user;
        }
        /// <summary>
        /// 登录登录 
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result<FaUserEntity> UserLogin(LogingDto inEnt)
        {
            //Bearer 
            Result<FaUserEntity> reobj = new Result<FaUserEntity>();
            reobj = user.UserLogin(inEnt);
            if (reobj.IsSuccess)
            {
                var claims = new Claim[]
                        {
                        new Claim(ClaimTypes.Name, reobj.Data.LOGIN_NAME),
                        new Claim(ClaimTypes.Role, "admin, Manage")
                        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.JwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    AppSettingsManager.JwtSettings.Issuer,
                    AppSettingsManager.JwtSettings.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(1),
                    creds);
                reobj.Code = new JwtSecurityTokenHandler().WriteToken(token);
            }
            return reobj;
        }
    }
}