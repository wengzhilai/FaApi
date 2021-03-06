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
    public class EquipmentController : ControllerBase
    {
        private IEquipmentRepository _dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public EquipmentController(
            IEquipmentRepository dal
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
        public async Task<ResultObj<FaEquipmentEntity>> Single(DtoDo<int> inEnt)
        {
            ResultObj<FaEquipmentEntity> reObj = new ResultObj<FaEquipmentEntity>();
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
        async public Task<Result> Save(DtoSave<FaEquipmentEntity> inEnt)
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
        async public Task<Result> Delete(DtoDo<int> inEnt)
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
        public async Task<ResultObj<KTV>> GetTree()
        {
            var reObj = new ResultObj<KTV>();
            try
            {
                reObj = await this._dal.GetTree(null);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取表的选择框", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }



        /// <summary>
        /// 获取单条设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> SingleEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._dal.SingleEquiment(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取单条设备失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 保存设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        async public Task<Result> SaveEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._dal.SaveEquiment(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "保存设备失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> DeleteEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._dal.DeleteEquiment(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "保存设备失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }


        /// <summary>
        /// 更新设备
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> UpdateEquiment(DtoEquipment inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await this._dal.UpdateEquiment(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "保存设备失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取设备配置失败
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<SmartTableSetting>> GetConfig(DtoDo<int> inEnt)
        {
            var reObj = new ResultObj<SmartTableSetting>();
            try
            {
                reObj = await this._dal.GetConfig(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取设备配置失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }


        /// <summary>
        /// 获取配置信息和数据
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<Dictionary<string, object>>> GetConfigAndData(QuerySearchDto inEnt)
        {
            var reObj = new ResultObj<Dictionary<string, object>>();
            try
            {
                reObj = await this._dal.GetData(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "获取设备数据失败", e);
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}