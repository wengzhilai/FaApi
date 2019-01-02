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

    /// <summary>
    /// 主页
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="configuration"></param>
        public HomeController(IConfiguration configuration)
        {
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