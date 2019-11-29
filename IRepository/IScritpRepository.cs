

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface IScritpRepository
    {
        /// <summary>
        /// 获取所有脚本列表，需指定字段，因为有字段内容很多
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        Task<IEnumerable<FaScriptEntity>> ScriptList(DtoSearch<FaScriptEntity> inEnt);

        /// <summary>
        /// 获取单个脚本
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaScriptEntity> ScriptSingleByKey(int key);

        /// <summary>
        /// 保存脚本基本信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<bool>> ScriptSave(DtoSave<FaScriptEntity> inEnt);

        /// <summary>
        /// 删除脚本
        /// </summary>
        /// <param name="scriptId">脚本 ID</param>
        /// <returns></returns>
        Task<Result> ScriptDelete(int scriptId);

        /// <summary>
        /// 查询脚本任务列表，需指定字段，因为有字段内容很多
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<IEnumerable<FaScriptTaskEntity>> ScriptTaskList(DtoSearch<FaScriptTaskEntity> inEnt);

        /// <summary>
        /// 获取单个脚本任务，需指定字段，因为有字段内容很多
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaScriptTaskEntity> ScriptTaskSingleByKey(int key);

        /// <summary>
        /// 保存脚本任务基本信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> ScriptTaskSave(DtoSave<FaScriptTaskEntity> inEnt);

        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns></returns>
        Task<Result> CancelTask(int taskId);


        /// <summary>
        /// 查询任务日志,需指定字段，因为有字段内容很多
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<IEnumerable<FaScriptTaskLogEntity>> ScriptTaskLogList(DtoSearch<FaScriptTaskLogEntity> inEnt);

        /// <summary>
        /// 获取单个脚本任务日志
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FaScriptTaskLogEntity> ScriptTaskLogoSingleByKey(int key);

        /// <summary>
        /// 保存脚本任务日志
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<bool>> ScriptTaskLogSave(DtoSave<FaScriptTaskLogEntity> inEnt);

    }
}
