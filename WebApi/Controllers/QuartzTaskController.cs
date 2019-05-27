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
using AutoMapper;
using Models.EntityView;
using Microsoft.AspNetCore.Cors;
using Quartz;
using Microsoft.AspNetCore.Authorization;
using Quartz.Impl.Matchers;

namespace WebApi.Controllers
{
    /// <summary>
    /// 关系
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuartzTaskController : ControllerBase
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
        async public Task<Result<bool>> IsStarted()
        {
            Result<bool> reObj = new Result<bool>();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            reObj.Data = _scheduler.IsStarted;
            return reObj;
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result> Start()
        {
            Result reObj = new Result();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            if (!_scheduler.IsStarted)
            {
                //2、开启调度器
                await _scheduler.Start();
            }

            var AllTask = await _scritp.ScriptList(new DtoSearch<FaScriptEntity>()
            {
                FilterList = x => x.STATUS == "正常",
                PageIndex = 1,
                PageSize = 1000
            });

            foreach (var item in AllTask)
            {
                GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.GroupEquals("ScriptGroup");
                var triggerList = await _scheduler.GetTriggerKeys(matcherTrigger);
                var triggerKey = triggerList.SingleOrDefault(x => x.Name == "triggerScript" + item.ID.ToString());
                if (string.IsNullOrEmpty(item.RUN_WHEN)) continue;
                //表示任务存在
                if (triggerKey != null)
                {
                    ICronTrigger trigger = (ICronTrigger)_scheduler.GetTrigger(triggerKey);
                    IJobDetail job = await _scheduler.GetJobDetail(trigger.JobKey);
                    if (trigger.CronExpressionString != item.RUN_WHEN)
                    {
                        // logger.InfoFormat("脚本服务 修改触发器【{0}】的时间表达式【{1}】为【{2}】", trigger.Key.Name, trigger.CronExpressionString, t.RUN_WHEN);
                        trigger.CronExpressionString = item.RUN_WHEN;
                        await _scheduler.DeleteJob(trigger.JobKey);
                        await _scheduler.ScheduleJob(job, trigger);
                    }
                }
                else
                {
                    //3、创建一个触发器
                    var trigger = TriggerBuilder.Create()
                                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                                    .WithCronSchedule(item.RUN_WHEN)
                                    .UsingJobData("scriptId", item.ID)  //通过在Trigger中添加参数值
                                    .WithIdentity("triggerScript" + item.ID.ToString(), "ScriptGroup")
                                    .Build();
                    //4、创建任务
                    var jobDetail = JobBuilder.Create<QuartzJobRunScriptTask>()
                                    .WithIdentity("jobScript" + item.ID.ToString(), "JobGroup")
                                    .Build();
                    //5、将触发器和任务器绑定到调度器中
                    await _scheduler.ScheduleJob(jobDetail, trigger);
                }
            }
            return reObj;
        }

        /// <summary>
        /// 停止任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result> Stop()
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
        async public Task<Result<bool>> RemoveJob(DtoKey InEnt)
        {
            Result<bool> reObj = new Result<bool>();
            try
            {
                //1、通过调度工厂获得调度器
                _scheduler = await _schedulerFactory.GetScheduler();
                GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.GroupEquals(InEnt.Key);
                var triggerList = await _scheduler.GetTriggerKeys(matcherTrigger);
                foreach (var triggerKey in triggerList)
                {
                    await _scheduler.PauseTrigger(triggerKey);// 停止触发器
                    await _scheduler.UnscheduleJob(triggerKey);// 移除触发器
                    var trigger = await _scheduler.GetTrigger(triggerKey);
                    await _scheduler.DeleteJob(trigger.JobKey);// 删除任务
                    reObj.IsSuccess = true;
                    reObj.Data = true;
                }
            }
            catch (Exception e)
            {
                reObj.IsSuccess = false;
                reObj.Data = false;
                reObj.Msg = e.Message;
            }

            return reObj;
        }


        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        async public Task<Result<QuartzTaskModel>> List()
        {
            Result<QuartzTaskModel> reObj = new Result<QuartzTaskModel>();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            GroupMatcher<TriggerKey> matcherTrigger = GroupMatcher<TriggerKey>.AnyGroup();
            var allTrigger = await _scheduler.GetTriggerKeys(matcherTrigger);

            foreach (var triggerKey in allTrigger)
            {
                var task = new QuartzTaskModel();
                var jobTrigger = await _scheduler.GetTrigger(triggerKey);
                var jobDetail = await _scheduler.GetJobDetail(jobTrigger.JobKey);
                task.KeyName = jobTrigger.Key.Name;
                task.KeyGroup = jobTrigger.Key.Group;
                task.JobDataListStr = TypeChange.ObjectToStr(jobTrigger.JobDataMap);
                task.CalendarName = jobTrigger.CalendarName;
                task.Description = jobTrigger.Description;
                if (jobTrigger.EndTimeUtc != null) task.EndTime = jobTrigger.EndTimeUtc.Value.ToString("yyyy-MM-dd HH-mm-ss");
                if (jobTrigger.FinalFireTimeUtc != null) task.FinalFireTimeUtc = jobTrigger.FinalFireTimeUtc.Value.ToString("yyyy-MM-dd HH-mm-ss");
                //返回下一次计划触发Quartz.ITrigger的时间
                if (jobTrigger.GetNextFireTimeUtc() != null) task.NextFireTime = jobTrigger.GetNextFireTimeUtc().Value.ToString("yyyy-MM-dd HH-mm-ss");
                //优先级
                task.Priority = jobTrigger.Priority;
                //触发器调度应该开始的时间
                task.StartTimeUtc = jobTrigger.StartTimeUtc.ToString("yyyy-MM-dd HH-mm-ss");
                reObj.DataList.Add(task);
            }
            return reObj;
        }
    }
}