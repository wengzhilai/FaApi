using System;
using System.Linq;
using System.Threading.Tasks;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entity;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using ApiQuartz.Controllers.Interface;

namespace ApiQuartz.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ScriptController : ControllerBase, IScriptController
    {
        private IScritpRepository _respoitory;

        /// <summary>
        /// 脚本管理
        /// </summary>
        /// <param name="script"></param>
        public ScriptController(
            IScritpRepository script
            )
        {
            this._respoitory = script;
        }
        /// <summary>
        /// 保存Query
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<int>> save(DtoSave<FaScriptEntity> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            try
            {
                reObj = await _respoitory.Save(inEnt);

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(this.GetType(), ex.ToString());
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
        public async Task<ResultObj<FaScriptEntity>> singleByKey(DtoDo<int> inEnt)
        {
            ResultObj<FaScriptEntity> reObj = new ResultObj<FaScriptEntity>();
            try
            {
                reObj.data = await _respoitory.SingleByKey(inEnt.Key);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(this.GetType(), ex.ToString());
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
                LogHelper.WriteErrorLog(this.GetType(), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
    }
}