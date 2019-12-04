using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IRepository;
using Models.Entity;
using Quartz;
using Quartz.Impl.Matchers;
using System.Linq;
using Helper;

namespace ApiQuartz.Jobs
{
    /// <summary>
    /// 用于加载数据库资源，并添加任务
    /// </summary>
    public class LoadTaskJob : IJob
    {
        private  IScritpRepository repository;


        public async Task Execute(IJobExecutionContext context)
        {
            System.Console.WriteLine("扫描数据库任务");
            repository= (IScritpRepository)ServiceLocator.GetClass<IScritpRepository>();
            IScheduler scheduler = context.Scheduler;
            List<FaScriptEntity> allTask = await repository.getNormalScript();
            foreach (var item in allTask)
            {
                GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.GroupEquals("ScriptTriggerGroup");
                var triggerList = await scheduler.GetTriggerKeys(matcherTrigger);

                var triggerKey = triggerList.SingleOrDefault(x => x.Name.Equals("scriptTrigger_" + item.id));
                //表示任务存在
                if (triggerKey != null)
                {
                    ICronTrigger trigger = (ICronTrigger)await scheduler.GetTrigger(triggerKey);
                    IJobDetail job = await scheduler.GetJobDetail(trigger.JobKey);
                    //表示式有变化则重新加载表达式
                    if (!trigger.CronExpressionString.Equals(item.runWhen))
                    {
                        System.Console.WriteLine("修改脚本ID:" + item.id);
                        // logger.InfoFormat("脚本服务 修改触发器【{0}】的时间表达式【{1}】为【{2}】", trigger.Key.Name, trigger.CronExpressionString, t.RUN_WHEN);
                        trigger.CronExpressionString = item.runWhen;
                        await scheduler.DeleteJob(trigger.JobKey);
                        await scheduler.ScheduleJob(job, trigger);

                    }
                }
                else
                {

                    System.Console.WriteLine("添加脚本ID:" + item.id);

                    //3、创建一个触发器
                    var trigger = TriggerBuilder.Create()
                                    .WithCronSchedule(item.runWhen)
                                    .UsingJobData("scriptId", item.id)  //通过在Trigger中添加参数值
                                    .WithIdentity("scriptTrigger_" + item.id, "ScriptTriggerGroup")
                                    .Build();
                    //4、创建任务
                    var jobDetail = JobBuilder.Create<QuartzJobRunScriptTask>()
                                    .WithIdentity("scriptJob_" + item.id, "ScriptJobGroup")
                                    .Build();
                    //5、将触发器和任务器绑定到调度器中
                    await scheduler.ScheduleJob(jobDetail, trigger);
                }
            }
            System.Console.WriteLine("结束扫描");

        }
    }
}
