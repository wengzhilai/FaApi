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
    public class ScriptController : ControllerBase
    {
        private IScritpRepository _script;

        /// <summary>
        /// 脚本管理
        /// </summary>
        /// <param name="script"></param>
        public ScriptController(
            IScritpRepository script
            )
        {
            this._script = script;
        }
        /// <summary>
        /// 获取脚本列表
        /// </summary>
        /// <param name="inEnt"></param>
        [HttpPost]
        [Authorize]
        public async Task<ResultObj<FaScriptEntity>> ScriptList(DtoSearch inEnt)
        {
            var reObj = new ResultObj<FaScriptEntity>();
            try
            {
                DtoSearch<FaScriptEntity> postEnt=new DtoSearch<FaScriptEntity>();
                postEnt.PageIndex=inEnt.PageIndex;
                postEnt.PageSize=inEnt.PageSize;
                reObj.dataList = (await _script.ScriptList(postEnt)).ToList();
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
        /// 获取任务列表
        /// </summary>
        /// <param name="inEnt"></param>
        [HttpPost]
        [Authorize]
        public async Task<ResultObj<FaScriptTaskEntity>> ScriptTaskList(DtoSearch inEnt)
        {
            var reObj = new ResultObj<FaScriptTaskEntity>();
            try
            {
                var SCRIPT_ID=Convert.ToInt32( inEnt.FilterList.SingleOrDefault(x=>x.Key=="SCRIPT_ID"));
                DtoSearch<FaScriptTaskEntity> postEnt=new DtoSearch<FaScriptTaskEntity>();
                postEnt.PageIndex=inEnt.PageIndex;
                postEnt.PageSize=inEnt.PageSize;
                postEnt.FilterList=x=>x.SCRIPT_ID==SCRIPT_ID;
                reObj.dataList = (await _script.ScriptTaskList(postEnt)).ToList();
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
        /// 获取任务列表
        /// </summary>
        /// <param name="inEnt"></param>
        [HttpPost]
        [Authorize]
        public async Task<ResultObj<FaScriptTaskLogEntity>> ScriptTaskLogList(DtoSearch inEnt)
        {
            var reObj = new ResultObj<FaScriptTaskLogEntity>();
            try
            {
                var SCRIPT_TASK_ID=Convert.ToInt32(inEnt.FilterList.SingleOrDefault(x=>x.Key=="SCRIPT_TASK_ID"));
                var postEnt=new DtoSearch<FaScriptTaskLogEntity>();
                postEnt.PageIndex=inEnt.PageIndex;
                postEnt.PageSize=inEnt.PageSize;
                postEnt.FilterList=x=>x.SCRIPT_TASK_ID==SCRIPT_TASK_ID;
                reObj.dataList = (await _script.ScriptTaskLogList(postEnt)).ToList();
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
        /// 添加脚本
        /// </summary>
        /// <param name="inEnt"></param>
        [HttpPost]
        [Authorize]
        public async Task<ResultObj<bool>> ScriptSave(DtoSave<FaScriptEntity> inEnt)
        {
            var reObj = new ResultObj<bool>();
            try
            {
                reObj = await _script.ScriptSave(inEnt);
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
        /// 删除脚本
        /// </summary>
        /// <param name="inEnt"></param>
        [HttpPost]
        [Authorize]
        public async Task<Result> ScriptDelete(DtoDo<int> inEnt)
        {
            var reObj = new Result();
            try
            {
                reObj = await _script.ScriptDelete(inEnt.Key);
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