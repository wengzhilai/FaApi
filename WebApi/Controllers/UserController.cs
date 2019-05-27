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
    public class UserController : ControllerBase
    {
        private IUserRepository _user;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="role"></param>
        public UserController(
            IUserRepository role
            )
        {
            _user = role;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaUserEntity>> Single(DtoDo<int> inEnt)
        {
            Result<FaUserEntity> reObj = new Result<FaUserEntity>();
            try
            {
                var ent = await _user.SingleByKey(inEnt.Key);
                reObj.Data = ent;
                reObj.IsSuccess = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;

        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> Save(DtoSave<FaUserEntity> inEnt)
        {
            var reObj = new Result<int>();
            try
            {
                reObj = await this._user.Save(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "修改角色信息失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 删除角色
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
                reObj = await this._user.Delete(inEnt.Key);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.IsSuccess = false;
                reObj.Msg = e.Message;
            }
            return reObj;
        }

    }
}