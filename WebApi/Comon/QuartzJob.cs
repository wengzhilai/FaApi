using System;
using System.Threading.Tasks;
using System.Linq;
using Quartz;
using Quartz.Impl.Matchers;
using Repository;
using Models;
using Models.Entity;
using Helper;

namespace WebApi.Comon
{
    /// <summary>
    /// 执行任务
    /// </summary>
    public class QuartzJobRunScriptTask : IJob
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        async public Task Execute(IJobExecutionContext context)
        {
            if (Fun.LockRunScript != null) return;
            try
            {
                Fun.LockRunScript = true;
                // var jobData = context.JobDetail.JobDataMap;//获取Job中的参数
                var triggerData = context.Trigger.JobDataMap;//获取Trigger中的参数
                                                             // 当Job中的参数和Trigger中的参数名称一样时，用 context.MergedJobDataMap获取参数时，Trigger中的值会覆盖Job中的值。
                                                             // var data = context.MergedJobDataMap;//获取Job和Trigger中合并的参数
                var scriptId = triggerData.GetInt("scriptId");
                var dal = new ScritpRepository();
                var script = await dal.ScriptSingleByKey(scriptId);
                if (script != null)
                {
                    var addEnt = new FaScriptTaskEntity();
                    addEnt.BODY_HASH = script.BODY_TEXT.Md5();
                    addEnt.BODY_TEXT = script.BODY_TEXT;
                    addEnt.LOG_TYPE = 0;
                    addEnt.RETURN_CODE = "";
                    addEnt.RUN_ARGS = script.RUN_ARGS;
                    addEnt.RUN_DATA = script.RUN_DATA;
                    addEnt.RUN_STATE = "等待";
                    addEnt.SCRIPT_ID = scriptId;
                    addEnt.START_TIME = DateTime.Now;
                    var taskId = await dal.ScriptTaskSave(new DtoSave<Models.Entity.FaScriptTaskEntity>
                    {
                        Data = addEnt
                    });
                    if (!taskId.IsSuccess)
                    {
                        LogHelper.WriteErrorLog(this.GetType(), "添加任务出错：" + taskId.Msg);
                        Fun.LockRunScript = null;
                        return;
                    }

                    var sqlList = script.BODY_TEXT.Split(';');
                    DapperHelper.TranscationBegin();

                    foreach (var item in sqlList)
                    {
                        int opNum = 0;
                        var log = new FaScriptTaskLogEntity()
                        {
                            SCRIPT_TASK_ID = taskId.Data,
                            LOG_TIME = DateTime.Now,
                            SQL_TEXT = item,
                            MESSAGE = opNum.ToString()
                        };
                        try
                        {
                            opNum = await DapperHelper.Exec(item);
                            log.LOG_TYPE = 1;
                            log.MESSAGE = opNum.ToString();
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), string.Format("执行{0}任务出错：{1}", item, e.ToString()));
                            DapperHelper.TranscationRollback();
                            log.LOG_TYPE = 2;
                            log.MESSAGE = e.Message;
                        }
                        var op = await dal.ScriptTaskLogSave(new DtoSave<FaScriptTaskLogEntity>
                        {
                            Data = log
                        });
                        DapperHelper.TranscationCommit();
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), string.Format("执行任务出错：{0}", e.ToString()));
            }
            finally
            {
                Fun.LockRunScript = null;

            }
            return;
        }
    }
}