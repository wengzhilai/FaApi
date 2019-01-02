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
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using WebApi.Model.InEnt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FamilyController : ControllerBase
    {
        IConfiguration config;
        IUserRepository user;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_user"></param>
        public FamilyController(IConfiguration _config, IUserRepository _user)
        {
            config = _config;
            user = _user;
        }
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Result<Relative> Relative(DtoKey inObj)
        {
            Result<Relative> reobj = new Result<Relative>();
            return reobj;
        }
    }
}