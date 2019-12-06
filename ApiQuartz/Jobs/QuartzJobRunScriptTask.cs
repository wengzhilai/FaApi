using System;
using System.Threading.Tasks;
using Helper;
using Models;
using Models.Entity;
using Quartz;
using Repository;

namespace ApiQuartz.Jobs
{
    /// <summary>
    /// 执行具体任务
    /// </summary>
    public class QuartzJobRunScriptTask : IJob
    {
        public QuartzJobRunScriptTask()
        {
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                // var jobData = context.JobDetail.JobDataMap;//获取Job中的参数
                var triggerData = context.Trigger.JobDataMap;//获取Trigger中的参数
                                                             // 当Job中的参数和Trigger中的参数名称一样时，用 context.MergedJobDataMap获取参数时，Trigger中的值会覆盖Job中的值。
                                                             // var data = context.MergedJobDataMap;//获取Job和Trigger中合并的参数
                var scriptId = triggerData.GetInt("scriptId");

                if (config.RunStatus.isRun("QuartzJobRunScriptTask_"+ scriptId)) return;
                config.RunStatus.setRun("QuartzJobRunScriptTask_" + scriptId);
                System.Console.WriteLine("开始执行任务"+ scriptId);

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
                    var taskId = await dal.ScriptTaskSave(new DtoSave<FaScriptTaskEntity>
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
                config.RunStatus.remove("QuartzJobRunScriptTask_" + scriptId);
                System.Console.WriteLine("结束执行任务" + scriptId);

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), string.Format("执行任务出错：{0}", e.ToString()));
            }
            return;
        }
    }
}

