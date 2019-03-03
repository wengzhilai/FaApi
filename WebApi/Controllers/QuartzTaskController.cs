using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using WebApi.Model;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using WebApi.Model.InEnt;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerFactory"></param>
        public QuartzTaskController(ISchedulerFactory schedulerFactory)
        {
            this._schedulerFactory = schedulerFactory;
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
            //2、开启调度器
            if (!_scheduler.IsStarted)
            {
                await _scheduler.Start();
                //3、创建一个触发器
                var trigger = TriggerBuilder.Create()
                                .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())//每两秒执行一次
                                .UsingJobData("key1", 321)  //通过在Trigger中添加参数值
                                .UsingJobData("key2", "123")
                                .WithIdentity("trigger2", "group1")
                                .Build();
                //4、创建任务
                var jobDetail = JobBuilder.Create<QuartzJob>()
                                .UsingJobData("key1", 123)//通过Job添加参数值
                                .UsingJobData("key2", "123")
                                .WithIdentity("job", "group")
                                .Build();
                //5、将触发器和任务器绑定到调度器中
                await _scheduler.ScheduleJob(jobDetail, trigger);
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

        [HttpPost]
        [AllowAnonymous]
        async public Task<Result<string>> List()
        {
            Result<string> reObj = new Result<string>();
            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            GroupMatcher<JobKey> matcher=GroupMatcher<JobKey>.AnyGroup();
            var jobList = await _scheduler.GetJobKeys(matcher);
            reObj.DataList = jobList.Select(x => x.Name).ToList();
            return reObj;
        }
    }
}