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
using Microsoft.AspNetCore.Http;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        IConfiguration _config;
        ILoginRepository _login;
        IHttpContextAccessor _accessor;
        IUserRepository _user;

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="config"></param>
        /// <param name="login"></param>
        /// <param name="accessor"></param>
        /// <param name="user"></param>
        public AuthController(IConfiguration config, ILoginRepository login, IHttpContextAccessor accessor, IUserRepository user)
        {
            _config = config;
            _login = login;
            _user = user;
            _accessor = accessor;
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
            reobj = _login.UserLogin(inEnt);
            if (reobj.IsSuccess)
            {
                var claims = new Claim[]
                        {
                        new Claim(ClaimTypes.Name, reobj.Data.LOGIN_NAME),
                        new Claim(ClaimTypes.NameIdentifier, reobj.Data.ID.ToString()),
                        new Claim(ClaimTypes.Role, "admin, Manage")
                        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.JwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    AppSettingsManager.JwtSettings.Issuer,
                    AppSettingsManager.JwtSettings.Audience,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddDays(7),
                    creds);
                reobj.Code = new JwtSecurityTokenHandler().WriteToken(token);
                
                RedisRepository.UserTokenSet(reobj.Data.ID, reobj.Code);
            }
            return reobj;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result LoginReg(LogingDto inEnt)
        {
            var reObj = new Result();
            try
            {
                return _login.LoginReg(inEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.IsSuccess = false;
                reObj.Code = e.RealCode;
                reObj.Msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public Result LoginOut()
        {
            var reObj = new Result();
            try
            {
                var outEnt = new DtoSave<FaLoginHistoryEntity>();
                outEnt.Data = new FaLoginHistoryEntity();
                outEnt.Data.LOGIN_HISTORY_TYPE = 2;
                outEnt.Data.LOGIN_HOST = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                outEnt.Data.LOGOUT_TIME = DateTime.Now;
                outEnt.Data.MESSAGE = "正常退出";
                var userEntList = _user.FindAll(x => x.LOGIN_NAME == User.Identity.Name);
                if (userEntList.Count() > 0)
                {
                    outEnt.Data.USER_ID = userEntList[0].ID;
                }
                reObj = _login.LoginOut(outEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.IsSuccess = false;
                reObj.Code = e.RealCode;
                reObj.Msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }
    }
}