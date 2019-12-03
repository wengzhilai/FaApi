
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
        DapperHelper<FaScriptEntity> dbHelper = new DapperHelper<FaScriptEntity>();

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

        async public Task<ResultObj<int>> Delete(int scriptId)
        {
            var reObj = new ResultObj<int>();
            DapperHelper<FaScriptEntity> dapper = new DapperHelper<FaScriptEntity>();

            dapper.TranscationBegin();
            try
            {
                DapperHelper<FaScriptTaskEntity> dapperTask = new DapperHelper<FaScriptTaskEntity>(dapper.GetConnection(), dapper.GetTransaction());
                DapperHelper<FaScriptTaskLogEntity> dapperTaskLog = new DapperHelper<FaScriptTaskLogEntity>(dapper.GetConnection(), dapper.GetTransaction());
                var opNum = await dapperTaskLog.Delete(string.Format("SCRIPT_TASK_ID IN ( SELECT a.ID from fa_script_task a where a.SCRIPT_ID={0})", scriptId));
                opNum = await dapperTask.Delete(i => i.SCRIPT_ID == scriptId);
                opNum = await dapper.Delete(i => i.id == scriptId);
                reObj.data = opNum;
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

        async public Task<ResultObj<int>> Save(DtoSave<FaScriptEntity> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            if (inEnt.data.id == 0)
            {
                inEnt.data.id = await new SequenceRepository().GetNextID<FaScriptEntity>();
                reObj.data = await dbHelper.Save(inEnt);
            }
            else
            {
                reObj.data = await dbHelper.Update(inEnt);
            }

            reObj.success = reObj.data > 0;
            return reObj;
        }

        async public Task<FaScriptEntity> SingleByKey(int key)
        {
            return await dbHelper.SingleByKey(key);
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

        async public Task<ResultObj<bool>> ScriptTaskLogSave(DtoSave<FaScriptTaskLogEntity> inEnt)
        {
            ResultObj<bool> reObj = new ResultObj<bool>();
            DapperHelper<FaScriptTaskLogEntity> dapper = new DapperHelper<FaScriptTaskLogEntity>();
            if (inEnt.data.ID == 0)
            {
                inEnt.data.ID = await new SequenceRepository().GetNextID<FaScriptTaskLogEntity>();
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

        async public Task<ResultObj<int>> ScriptTaskSave(DtoSave<FaScriptTaskEntity> inEnt)
        {
            ResultObj<int> reObj = new ResultObj<int>();
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            if (inEnt.data.ID == 0)
            {
                inEnt.data.ID = await new SequenceRepository().GetNextID<FaScriptTaskEntity>();
            }
            var opNum = await dapper.Save(inEnt);
            reObj.success = opNum > 0;
            reObj.msg = "添加成功";
            reObj.data = inEnt.data.ID;
            return reObj;
        }

        async public Task<FaScriptTaskEntity> ScriptTaskSingleByKey(int key)
        {
            DapperHelper<FaScriptTaskEntity> dapper = new DapperHelper<FaScriptTaskEntity>();
            return await dapper.SingleByKey(key);
        }

        public async Task<List<FaScriptEntity>> getNormalScript()
        {
            var reObj =(await dbHelper.FindAll(x => x.status == "1")).ToList();
            reObj = reObj.Where(x => !string.IsNullOrEmpty(x.runWhen)).ToList();
            return reObj;
        }
    }
}
