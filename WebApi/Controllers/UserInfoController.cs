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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Models.EntityView;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserInfoController : ControllerBase
    {
        private IUserInfoRepository userInfo;
        private IMapper mapper;
        private IUserRepository user;
        private ILoginRepository login;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userInfo"></param>
        /// <param name="_mapper"></param>
        /// <param name="_login"></param>
        /// <param name="_user"></param>
        public UserInfoController(
            IUserInfoRepository _userInfo,
            IMapper _mapper,
            ILoginRepository _login,
            IUserRepository _user
            )
        {
            userInfo = _userInfo;
            mapper = _mapper;
            user = _user;
            login = _login;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<FaUserInfoEntityView>> Single(DtoKey inEnt)
        {
            Result<FaUserInfoEntityView> reObj = new Result<FaUserInfoEntityView>();
            int key = Convert.ToInt32(inEnt.Key);
            FaUserInfoEntityView user = await userInfo.SingleByKey(key);
            user.BirthdaylunlarDate=user.BIRTHDAY_TIME.ToString();
            user.BirthdaysolarDate=user.BIRTHDAY_TIME.ToString();
            reObj.Data = user;
            reObj.IsSuccess = true;
            return reObj;
        }

        /// <summary>
        /// 根据姓名查用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaUserInfoEntityView>> SingleByName(DtoKey inEnt)
        {
            Result<FaUserInfoEntityView> reObj = new Result<FaUserInfoEntityView>();
            var user = await userInfo.FindAll(x=>x.NAME==inEnt.Key);
            reObj.DataList = user.ToList();
            reObj.IsSuccess = true;
            return reObj;
        }
        

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt)
        {
            Result<FaUserInfoEntityView> reObj = new Result<FaUserInfoEntityView>();
            inEnt.FilterList=x=>x.ID<100;
            inEnt.OrderType="id asc";
            var user = await userInfo.List(inEnt);
            reObj.DataList = user.ToList();
            reObj.IsSuccess = true;
            return reObj;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result> UserInfoReg(RegUserInfo inEnt)
        {
            var reObj = new Result();
            try
            {
                return await userInfo.RegUserInfo(inEnt);
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