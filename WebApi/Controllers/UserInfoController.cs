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
        private IUserInfoRepository _userInfo;
        private IPublicRepository _public;
        private IMapper mapper;
        private IUserRepository user;
        private ILoginRepository login;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="_mapper"></param>
        /// <param name="_login"></param>
        /// <param name="_user"></param>
        /// <param name="pub"></param>
        public UserInfoController(
            IUserInfoRepository userInfo,
            IMapper _mapper,
            ILoginRepository _login,
            IUserRepository _user,
            IPublicRepository pub
            )
        {
            _userInfo = userInfo;
            _public = pub;
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
        [AllowAnonymous]
        public async Task<ResultObj<FaUserInfoEntityView>> Single(DtoKey inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            int key = Convert.ToInt32(inEnt.Key);
            FaUserInfoEntityView user = await _userInfo.SingleByKey(key);
            if (user.BIRTHDAY_TIME != null)
            {
                if (user.YEARS_TYPE==("阴历") || user.YEARS_TYPE==("国号"))
                {
                    user.BirthdaysolarDate = this._public.GetSolarDate(user.BIRTHDAY_TIME.Value).msg;
                    user.BirthdaylunlarDate = user.BIRTHDAY_TIME.Value.ToString("yyyy-MM-dd HH:mm");
                    
                }
                else
                {
                    user.BirthdaylunlarDate = this._public.GetLunarDate(user.BIRTHDAY_TIME.Value).msg;
                    user.BirthdaysolarDate = user.BIRTHDAY_TIME.Value.ToString("yyyy-MM-dd HH:mm");
                }
            }
            if (user.DIED_TIME != null && user.IS_LIVE == 0)
            {
                if (user.YEARS_TYPE==("阴历") || user.YEARS_TYPE==("国号"))
                {
                    user.DiedsolarDate = this._public.GetSolarDate(user.DIED_TIME.Value).msg;
                    user.DiedlunlarDate = user.DIED_TIME.Value.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    user.DiedlunlarDate = this._public.GetLunarDate(user.DIED_TIME.Value).msg;
                    user.DiedsolarDate = user.DIED_TIME.Value.ToString("yyyy-MM-dd HH:mm");
                }
            }

            reObj.data = user;
            reObj.success = true;
            return reObj;
        }

        /// <summary>
        /// 根据姓名查用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<FaUserInfoEntityView>> SingleByName(DtoKey inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            var user = await _userInfo.FindAll(x => x.NAME == inEnt.Key);
            reObj.dataList = user.ToList();
            reObj.success = true;
            return reObj;
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaUserInfoEntityView>> List(DtoSearch<FaUserInfoEntityView> inEnt)
        {
            ResultObj<FaUserInfoEntityView> reObj = new ResultObj<FaUserInfoEntityView>();
            inEnt.FilterList = x => x.LOGIN_NAME.Length == 11;
            inEnt.OrderType = "id asc";
            var user = await _userInfo.List(inEnt);
            reObj.dataList = user.ToList();
            reObj.success = true;
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
                return await _userInfo.RegUserInfo(inEnt);
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

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> Save(DtoSave<FaUserInfoEntityView> inEnt)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                reObj = await this._userInfo.Save(inEnt, User.Identity.Name, 1);
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        async public Task<Result> Delete(DtoDo<int> inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._userInfo.Delete(inEnt.Key);
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