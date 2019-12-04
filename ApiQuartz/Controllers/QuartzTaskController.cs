using System;
using System.Linq;
using System.Threading.Tasks;

using IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entity;
using Helper;
using Microsoft.AspNetCore.Cors;
using Quartz;
using Microsoft.AspNetCore.Authorization;
using Quartz.Impl.Matchers;
using ApiQuartz.Controllers.Interface;
using ApiQuartz.Jobs;

namespace ApiQuartz.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class QuartzTaskController : ControllerBase, IQuartzTaskController
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;
        private IScritpRepository _scritp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerFactory"></param>
        /// <param name="scritp"></param>
        public QuartzTaskController(
            ISchedulerFactory schedulerFactory,
            IScritpRepository scritp
            )
        {
            this._schedulerFactory = schedulerFactory;
            this._scritp = scritp;
        }

        /// <summary>
        /// 获取任务是否运行
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ResultObj<bool>> isStarted()
        {
            ResultObj<bool> reObj = new ResultObj<bool>();
            _scheduler = await _schedulerFactory.GetScheduler();
            GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.AnyGroup();
            var allTrigger = await _scheduler.GetTriggerKeys(matcherTrigger);
            reObj.data = allTrigger.Count>0;
            reObj.success = reObj.data;
            return reObj;
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result> start()
        {
            Result reObj = new Result();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            var jobDetail =await _scheduler.GetJobDetail(new JobKey("jobLoadTask", "jobGroup"));
            if (jobDetail == null)
            {
                //3、创建一个触发器
                var trigger = TriggerBuilder.Create()
                                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())//每两秒执行一次
                                //.WithCronSchedule("5 * * * * ?")
                                .WithIdentity("triggerJob", "triggerJobGroup")
                                .Build();

                //创建JobDetail实例，并与HelloWordlJob类绑定
                var jobDetailTask = JobBuilder.Create<LoadTaskJob>()
                                    .WithIdentity("jobLoadTask", "jobGroup")
                                    .Build();

                //开始执行
                await _scheduler.ScheduleJob(jobDetailTask, trigger);
            }
            await _scheduler.Start();
            return reObj;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result> stop()
        {
            Result reObj = new Result();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            await _scheduler.Shutdown();
            return reObj;
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="InEnt">trigger名称</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result> removeJob(DtoKey InEnt)
        {
            ResultObj<bool> reObj = new ResultObj<bool>();
            try
            {
                //1、通过调度工厂获得调度器
                _scheduler = await _schedulerFactory.GetScheduler();

                GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.GroupEquals("ScriptGroup");
                var triggerList = await _scheduler.GetTriggerKeys(matcherTrigger);
                triggerList = triggerList.Where(x => x.Name.Equals(InEnt.Key)).ToList();
                foreach (var triggerKey in triggerList)
                {
                    await _scheduler.PauseTrigger(triggerKey);// 停止触发器
                    await _scheduler.UnscheduleJob(triggerKey);// 移除触发器
                    var trigger = await _scheduler.GetTrigger(triggerKey);
                    await _scheduler.DeleteJob(trigger.JobKey);// 删除任务
                    reObj.success = true;
                    reObj.data = true;
                }
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.data = false;
                reObj.msg = e.Message;
            }

            return reObj;
        }


        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<ResultObj<QuartzTaskModel>> list()
        {
            ResultObj<QuartzTaskModel> reObj = new ResultObj<QuartzTaskModel>();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.AnyGroup();
            var allTrigger = await _scheduler.GetTriggerKeys(matcherTrigger);

            foreach (var triggerKey in allTrigger)
            {
                var task = new QuartzTaskModel();
                var jobTrigger = await _scheduler.GetTrigger(triggerKey);
                var jobDetail = await _scheduler.GetJobDetail(jobTrigger.JobKey);
                task.keyName = jobTrigger.Key.Name;
                task.keyGroup = jobTrigger.Key.Group;
                task.jobDataListStr = TypeChange.ObjectToStr(jobTrigger.JobDataMap);
                task.calendarName = jobTrigger.CalendarName;
                task.description = jobTrigger.Description;
                if (jobTrigger.EndTimeUtc != null) task.endTime = jobTrigger.EndTimeUtc.Value.ToString("yyyy-MM-dd HH-mm-ss");
                if (jobTrigger.FinalFireTimeUtc != null) task.finalFireTimeUtc = jobTrigger.FinalFireTimeUtc.Value.ToString("yyyy-MM-dd HH-mm-ss");
                //返回下一次计划触发Quartz.ITrigger的时间
                if (jobTrigger.GetNextFireTimeUtc() != null) task.nextFireTime = jobTrigger.GetNextFireTimeUtc().Value.ToString("yyyy-MM-dd HH-mm-ss");
                //优先级
                task.priority = jobTrigger.Priority;
                //触发器调度应该开始的时间
                task.startTimeUtc = jobTrigger.StartTimeUtc.ToString("yyyy-MM-dd HH-mm-ss");
                reObj.dataList.Add(task);
            }
            return reObj;
        }
    }
}