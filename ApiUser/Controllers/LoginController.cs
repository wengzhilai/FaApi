using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Helper;
using IdentityModel.Client;
using IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace ApiUser.Controllers
{
    [Route("[controller]/[action]")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase, ILoginController
    {
        private readonly IHttpClientFactory clientFactory;
        ILoginRepository _login;

        public LoginController(IHttpClientFactory clientFactory, ILoginRepository login)
        {
            this.clientFactory = clientFactory;
            _login = login;
        }

        /// <summary>
        /// 密码登录
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<String>> userLogin(UserLoginDto inEnt)
        {
            ResultObj<String> reobj = new ResultObj<String>();

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(AppSettingsManager.self.Idsvr4Url);
            if (disco.IsError)
                return new ResultObj<String>(false, disco.Error);
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
        [AllowAnonymous]
        public async Task<ResultObj<String>> UserCodeLogin(UserCodeLoginDto inEnt)
        {
            ResultObj<String> reobj = new ResultObj<String>();

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:9001/");
            if (disco.IsError)
                return new ResultObj<String>(false, disco.Error);
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

        public async Task<ResultObj<int>> loginReg(LogingDto inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                return await _login.LoginReg(inEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<Result> resetPassword(ResetPasswordDto inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._login.ResetPassword(inEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public Task<Result> deleteUser(DtoKey userName)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultObj<bool>> userEditPwd(EditPwdDto inEnt)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                inEnt.LoginName = User.Identity.Name;
                reObj = await this._login.UserEditPwd(inEnt);
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }

    
}