
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq.Expressions;
namespace Repository
{
    public class ScritpRepository : IScritpRepository
    {
        /// <summary>
        /// 取消任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns></returns>
        async public Task<Result> CancelTask(int taskId)
        {
            Result reObj = new Result();
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            var single = await dapper.SingleByKey(taskId);
            if (single == null)
            {
                reObj.success = false;
                reObj.msg = "任务不存在";
            }
            if (single.RUN_STATE == "停止")
            {
                reObj.success = true;
            }
            else
            {
                reObj.success = (await dapper.Update("RUN_STATE='停止'", "ID=" + taskId)) > 0;
            }
            return reObj;
        }

        async public Task<Result> ScriptDelete(int scriptId)
        {
            Result reObj = new Result();
            DapperHelper<FaScriptEntity> dapper = new DapperHelper<FaScriptEntity>();

            dapper.TranscationBegin();
            try
            {
                DapperHelper<FaScriptTaskEntity> dapperTask = new DapperHelper<FaScriptTaskEntity>(dapper.GetConnection(), dapper.GetTransaction());
                DapperHelper<FaScriptTaskLogEntity> dapperTaskLog = new DapperHelper<FaScriptTaskLogEntity>(dapper.GetConnection(), dapper.GetTransaction());
                var opNum = await dapperTaskLog.Delete(string.Format("SCRIPT_TASK_ID IN ( SELECT a.ID from fa_script_task a where a.SCRIPT_ID={0})", scriptId));
                opNum = await dapperTask.Delete(i => i.SCRIPT_ID == scriptId);
                opNum = await dapper.Delete(i => i.ID == scriptId);
                reObj.success = opNum > 0;
                dapper.TranscationCommit();
            }
            catch (Exception e)
            {
                dapper.TranscationRollback();
                reObj.success = false;
                reObj.msg = e.Message;
            }

            return reObj;
        }

        public Task<IEnumerable<FaScriptEntity>> ScriptList(DtoSearch<FaScriptEntity> inEnt)
        {
            DapperHelper<FaScriptEntity> dapper = new DapperHelper<FaScriptEntity>();
            if (inEnt.IgnoreFieldList == null) inEnt.IgnoreFieldList = new List<string>();
            inEnt.IgnoreFieldList.Add("BODY_TEXT");
            return dapper.FindAll(inEnt);
        }

        async public Task<Result<bool>> ScriptSave(DtoSave<FaScriptEntity> inEnt)
        {
            Result<bool> reObj = new Result<bool>();
            DapperHelper<FaScriptEntity> dapper = new DapperHelper<FaScriptEntity>();
            if (inEnt.Data.ID == 0)
            {
                inEnt.Data.ID = await new SequenceRepository().GetNextID<FaScriptEntity>();
                var opNum = await dapper.Save(inEnt);
                reObj.success = opNum > 0;
                reObj.msg = "添加成功";
            }
            else
            {
                var opNum = await dapper.Update(inEnt);
                reObj.success = opNum > 0;
                reObj.msg = "修改成功";
            }
            return reObj;
        }

        async public Task<FaScriptEntity> ScriptSingleByKey(int key)
        {
            DapperHelper<FaScriptEntity> dapper = new DapperHelper<FaScriptEntity>();
            return await dapper.SingleByKey(key);
        }

        public Task<IEnumerable<FaScriptTaskEntity>> ScriptTaskList(DtoSearch<FaScriptTaskEntity> inEnt)
        {
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            if (inEnt.IgnoreFieldList == null) inEnt.IgnoreFieldList = new List<string>();
            inEnt.IgnoreFieldList.Add("BODY_TEXT");
            return dapper.FindAll(inEnt);
        }

        public Task<IEnumerable<FaScriptTaskLogEntity>> ScriptTaskLogList(DtoSearch<FaScriptTaskLogEntity> inEnt)
        {
            DapperHelper<FaScriptTaskLogEntity> dapper = new DapperHelper<FaScriptTaskLogEntity>();
            if (inEnt.IgnoreFieldList == null) inEnt.IgnoreFieldList = new List<string>();
            inEnt.IgnoreFieldList.Add("SQL_TEXT");
            return dapper.FindAll(inEnt);
        }

        async public Task<FaScriptTaskLogEntity> ScriptTaskLogoSingleByKey(int key)
        {
            DapperHelper<FaScriptTaskLogEntity> dapper = new DapperHelper<FaScriptTaskLogEntity>();
            return await dapper.SingleByKey(key);
        }

        async public Task<Result<bool>> ScriptTaskLogSave(DtoSave<FaScriptTaskLogEntity> inEnt)
        {
            Result<bool> reObj = new Result<bool>();
            DapperHelper<FaScriptTaskLogEntity> dapper = new DapperHelper<FaScriptTaskLogEntity>();
            if (inEnt.Data.ID == 0)
            {
                inEnt.Data.ID = await new SequenceRepository().GetNextID<FaScriptTaskLogEntity>();
                var opNum = await dapper.Save(inEnt);
                reObj.success = opNum > 0;
                reObj.msg = "添加成功";
            }
            else
            {
                var opNum = await dapper.Update(inEnt);
                reObj.success = opNum > 0;
                reObj.msg = "修改成功";
            }
            return reObj;
        }

        async public Task<Result<int>> ScriptTaskSave(DtoSave<FaScriptTaskEntity> inEnt)
        {
            Result<int> reObj = new Result<int>();
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            if (inEnt.Data.ID == 0)
            {
                inEnt.Data.ID = await new SequenceRepository().GetNextID<FaScriptTaskEntity>();
            }
            var opNum = await dapper.Save(inEnt);
            reObj.success = opNum > 0;
            reObj.msg = "添加成功";
            reObj.data=inEnt.Data.ID;
            return reObj;
        }

        async public Task<FaScriptTaskEntity> ScriptTaskSingleByKey(int key)
        {
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            return await dapper.SingleByKey(key);
        }
    }
}
