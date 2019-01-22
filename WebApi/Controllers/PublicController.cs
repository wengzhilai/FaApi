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
    public class PublicController : ControllerBase
    {

        IPublicRepository _public;
        /// <summary>
        /// 
        /// </summary>
        public PublicController(IPublicRepository pub)
        {
            _public=pub;
        }
  
        /// <summary>
        /// 发送验证码到手机
        /// <para>发送时会在用户的Login表里修改VERIFY_CODE，并在fa_sms_send增加记录</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> SendCode(DtoKey inEnt)
        {
            Result reEnt = new Result();
            var t=await _public.SmsSendCode("18180770313","AAAA");
            // dynamic reEnt = await Task.Run(() => Fun<ErrorInfo>.Func(api.PublicApi.SendCode, ref err, inEnt));
            // if (err.IsError) return err;
            return reEnt;
        }
    }
}