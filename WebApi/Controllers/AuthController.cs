using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        IConfiguration _config;
        ILoginRepository _login;
        IRoleRepository _role;
        IHttpContextAccessor _accessor;
        IUserRepository _user;
        IUserInfoRepository _userInfo;

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="config"></param>
        /// <param name="login"></param>
        /// <param name="role"></param>
        /// <param name="userInfo"></param>
        /// <param name="accessor"></param>
        /// <param name="user"></param>
        public AuthController(
            IConfiguration config,
            ILoginRepository login,
            IUserInfoRepository userInfo,
            IRoleRepository role,
            IHttpContextAccessor accessor,
            IUserRepository user
            )
        {
            _config = config;
            _login = login;
            _role = role;
            _user = user;
            _accessor = accessor;
            _userInfo = userInfo;
        }
        /// <summary>
        /// 登录登录 
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaUserEntity>> UserLogin(LogingDto inEnt)
        {
            //Bearer 
            Result<FaUserEntity> reobj = new Result<FaUserEntity>();
            reobj = await _login.UserLogin(inEnt);
            if (reobj.IsSuccess)
            {
                reobj.Data.CanEditIdList=await _userInfo.GetCanEditUserIdListAsync(reobj.Data.ID);
                var claims = new Claim[]
                        {
                        new Claim(ClaimTypes.Name, reobj.Data.LOGIN_NAME),
                        new Claim(ClaimTypes.NameIdentifier, reobj.Data.ID.ToString()),
                        new Claim(ClaimTypes.Role, "admin, Manage")
                        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsManager.self.JwtSettings.SecretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    AppSettingsManager.self.JwtSettings.Issuer,
                    AppSettingsManager.self.JwtSettings.Audience,
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
        public async Task<Result> LoginReg(LogingDto inEnt)
        {
            var reObj = new Result();
            try
            {
                return await _login.LoginReg(inEnt);
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
        public async Task<Result> LoginOut()
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
                var userEntList = await _user.FindAll(x => x.LOGIN_NAME == User.Identity.Name);
                if (userEntList.Count() > 0)
                {
                    outEnt.Data.USER_ID = userEntList.ToList()[0].ID;
                }
                reObj = await _login.LoginOut(outEnt);
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
        /// 重设置密码
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> ResetPassword(ResetPasswordDto inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._login.ResetPassword(inEnt);
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
        /// 修改密码
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> EditPwd(EditPwdDto inEnt)
        {
            var reObj = new Result();
            try
            {
                inEnt.LoginName=User.Identity.Name;
                reObj = await this._login.UserEditPwd(inEnt);
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
        /// 检测用户是否有权限
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<bool>> CheckAuth(CheckAuthDto inEnt)
        {
            var reObj = new Result<bool>();
            try
            {
                reObj.Data = await this._role.CheckAuth(inEnt);
                reObj.IsSuccess=true;
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