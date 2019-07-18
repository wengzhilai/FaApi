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

using Helper;
using Models.Entity;
using AutoMapper;
using Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebApi.Controllers
{

    /// <summary>
    /// 主页
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private IMapper _mapper { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        /// <param name="env"></param>
        public HomeController(IConfiguration configuration, IMapper mapper, IHostingEnvironment env)
        {
            _env = env;
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
            try
            {
                var i = Convert.ToInt32("a");
            }
            catch
            (Exception e)
            {
                LogHelper.WriteErrorLog<HomeController>("测试错误日志", e);
            }
            LogHelper.WriteLog<HomeController>("测试正常日志");
            return "home-index";
        }

        /// <summary>
        /// 测试word文档
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string TestDocx()
        {
            try
            {
                var allPath = Path.Combine(_env.ContentRootPath, "..\\Doc\\Family.docx");
                var t = new WordHelper();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog<HomeController>("测试错误日志", e);
            }
            return "normal";
        }

        /// <summary>
        /// 不验证权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string TestAutoMap()
        {
            var testEnt = new FaUserInfoEntity();
            testEnt.ELDER_ID = 1;
            var outEnt = _mapper.Map<RelativeItem>(testEnt);
            return outEnt.ElderName;
        }

        /// <summary>
        /// 测试Redis
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public FaUserEntity TestRedis()
        {
            RedisWriteHelper.HashSetKey<FaUserEntity>("User_1", "NAME", "翁志来测试1");
            RedisWriteHelper.HashSetKey<FaUserEntity>("User_1", "NAME", "翁志来测试2");
            // RedisWriteHelper.HashDelete<FaUserEntity>("User_1", "NAME");
            //RedisWriteHelper.KeyDelete("User_1");
            var t = RedisReadHelper.GetObject<FaUserEntity>("User_1");
            return t.Item1;
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
            List<KeyValuePair<string, object>> listSqlParaModel = new List<KeyValuePair<string, object>>();
            List<int> tt = new List<int>() { 1, 2, 4 };
            var testEnt = Helper.LambdaToSqlHelper.GetWhereSql<FaElderEntity>(x => x.NAME.IndexOf("34") > 1 || x.ID < 10, listSqlParaModel);
            return testEnt + "\n\r" + TypeChange.ObjectToStr(listSqlParaModel);
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
            return new string[] { User.Identity.Name, User.Claims.Single(a => a.Type == ClaimTypes.NameIdentifier).Value };
        }
    }
}