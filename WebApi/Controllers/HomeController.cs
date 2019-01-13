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
using Models.Entity;
using AutoMapper;
using Models;

namespace WebApi.Controllers
{

    /// <summary>
    /// 主页
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        public HomeController(IConfiguration configuration,IMapper mapper)
        {
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// 不验证权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string Index()
        {
            return "home-index";
        }

        /// <summary>
        /// 不验证权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string TestAutoMap()
        {
            var testEnt=new FaUserInfoEntity();
            testEnt.ELDER_ID=1;
            var outEnt=_mapper.Map<RelativeItem>(testEnt);
            return outEnt.ElderName;
        }

        /// <summary>
        /// 测试Redis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string TestRedis()
        {
            RedisWriteHelper.HashSetKey("user","user_1","翁志来测试1");
            RedisWriteHelper.HashSetKey("user","user_2","翁志来测试2");
            RedisWriteHelper.HashDelete("user","user_2");
            var t= RedisReadHelper.HashGetKey("user","user_1");
            return t;
        }

        /// <summary>
        /// 不验证权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string TestLambda()
        {
            // FaUserInfoEntity, RelativeItem
            List<KeyValuePair<string,object>> listSqlParaModel = new List<KeyValuePair<string,object>>();
            List<int> tt=new List<int>(){1,2,4};
            var testEnt=Helper.LambdaToSqlHelper.GetWhereSql<FaElderEntity>(x=>x.NAME.IndexOf("34")>1 || x.ID<10,listSqlParaModel);
            return testEnt+"\n\r"+TypeChange.ObjectToStr(listSqlParaModel);
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [HttpPost]
        public string[] AuthPage()
        {
            return new string[] { User.Identity.Name, User.Identity.AuthenticationType };
        }
    }
}