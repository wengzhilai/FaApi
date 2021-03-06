﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using IRepository;
using System;
using Helper;
using Microsoft.AspNetCore.Cors;
using Models.Entity;

namespace ApiUser.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [EnableCors]
    [Authorize]
    public class ModuleController : ControllerBase, IModuleController
    {
        IModuleRepository _respoitory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="module"></param>
        public ModuleController(IModuleRepository module)
        {
            this._respoitory = module;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        async public Task<ResultObj<FaModuleEntity>> getUserMenu()
        {
            var reObj = new ResultObj<FaModuleEntity>();
            try
            {
                //var allKey = User.Claims.Select(x => new KV { K = x.Type, V = x.Value }).ToList();

                var userId = User.Claims.Single(a => a.Type == "id").Value;

                reObj = await this._respoitory.GetMGetMenuByUserId(Convert.ToInt32(userId));
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
        /// 保存Query
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<int>> save(DtoSave<FaModuleEntity> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            try
            {
                reObj = await _respoitory.Save(inEnt);

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 查找单条
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<FaModuleEntity>> singleByKey(DtoDo<int> inEnt)
        {
            ResultObj<FaModuleEntity> reObj = new ResultObj<FaModuleEntity>();
            try
            {
                reObj.data = await _respoitory.SingleByKey(inEnt.Key);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<int>> delete(DtoDo<int> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            try
            {
                reObj = await _respoitory.Delete(inEnt.Key);

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
    }
}