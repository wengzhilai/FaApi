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
using AutoMapper;
using Models.EntityView;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FamilyController : ControllerBase
    {
        IConfiguration config;
        IUserInfoRepository userInfo;
        private IMapper mapper;
        IUserRepository user;
        IFamilyRepository family;
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="_config"></param>
        /// <param name="_userInfo"></param>
        /// <param name="_mapper"></param>
        /// <param name="_family"></param>
        /// <param name="_user"></param>
        public FamilyController(
            IConfiguration _config,
            IUserInfoRepository _userInfo,
            IMapper _mapper,
        IFamilyRepository _family,

             IUserRepository _user)
        {
            config = _config;
            userInfo = _userInfo;
            mapper = _mapper;
            user = _user;
            family = _family;

            family.SetMapper(mapper);
        }
        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<Relative>> Relative(DtoDo<int> inObj)
        {
            Result<Relative> reObj = new Result<Relative>();
            try
            {
                reObj = await family.Relative(inObj);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取关系图失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取关系图
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaUserBookEntityView>> GetUserBooks(DtoDo<int> inObj)
        {
            Result<FaUserBookEntityView> reObj = new Result<FaUserBookEntityView>();
            try
            {
                reObj.DataList = await family.GetUserBooksAsync(inObj.Key);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户获取前谱失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }
    }
}