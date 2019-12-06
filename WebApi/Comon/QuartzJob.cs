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
                var script = await dal.SingleByKey(scriptId);
                if (script != null)
                {
                    var addEnt = new FaScriptTaskEntity();
                    addEnt.bodyHash = script.bodyText.Md5();
                    addEnt.bodyText = script.bodyText;
                    addEnt.logType = 0;
                    addEnt.returnCode = "";
                    addEnt.runArgs = script.runArgs;
                    addEnt.runData = script.runData;
                    addEnt.runState = "等待";
                    addEnt.scriptId = scriptId;
                    addEnt.startTime = DateTime.Now;
                    var taskId = await dal.ScriptTaskSave(new DtoSave<Models.Entity.FaScriptTaskEntity>
                    {
                        data = addEnt
                    });
                    if (!taskId.success)
                    {
                        LogHelper.WriteErrorLog(this.GetType(), "添加任务出错：" + taskId.msg);
                        Fun.LockRunScript = null;
                        return;
                    }

                    var sqlList = script.bodyText.Split(';');
                    var dapper = new DapperHelper();
                    dapper.TranscationBegin();

                    foreach (var item in sqlList)
                    {
                        int opNum = 0;
                        var log = new FaScriptTaskLogEntity()
                        {
                            scriptTaskId = taskId.data,
                            logTime = DateTime.Now,
                            sqlText = item,
                            message = opNum.ToString()
                        };
                        try
                        {
                            opNum = await dapper.Exec(item);
                            log.logType = 1;
                            log.message = opNum.ToString();
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteErrorLog(this.GetType(), string.Format("执行{0}任务出错：{1}", item, e.ToString()));
                            dapper.TranscationRollback();
                            log.logType = 2;
                            log.message = e.Message;
                        }
                        var op = await dal.ScriptTaskLogSave(new DtoSave<FaScriptTaskLogEntity>
                        {
                            data = log
                        });
                        dapper.TranscationCommit();
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