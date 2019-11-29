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
    public class TableController : ControllerBase
    {
        private ITableRepository _dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public TableController(
            ITableRepository dal
            )
        {
            _dal = dal;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<FaTableTypeEntity>> Single(DtoDo<int> inEnt)
        {
            ResultObj<FaTableTypeEntity> reObj = new ResultObj<FaTableTypeEntity>();
            try
            {
                var ent = await _dal.SingleByKey(inEnt.Key);
                reObj.data = ent;
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;

        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> Save(DtoSave<FaTableTypeEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                reObj = await this._dal.Save(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "修改角色信息失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Delete(DtoDo<int> inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._dal.Delete(inEnt.Key);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取用户详情失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取表的选择框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<KTV>> GetTableSelect()
        {
            var reObj = new ResultObj<KTV>();
            try
            {
                reObj = await this._dal.GetTableSelect();
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取表的选择框", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}