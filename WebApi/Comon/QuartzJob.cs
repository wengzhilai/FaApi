using System;
using System.Threading.Tasks;
using Quartz;

namespace WebApi.Comon
{
    /// <summary>
    /// 默认任务
    /// </summary>
    public class QuartzJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobData = context.JobDetail.JobDataMap;//获取Job中的参数

            var triggerData = context.Trigger.JobDataMap;//获取Trigger中的参数
                                                         // 当Job中的参数和Trigger中的参数名称一样时，用 context.MergedJobDataMap获取参数时，Trigger中的值会覆盖Job中的值。
            var data = context.MergedJobDataMap;//获取Job和Trigger中合并的参数

            var value1 = jobData.GetInt("key1");
            var value2 = jobData.GetString("key2");
            return Task.Run(() =>
                        {
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                        });
        }
    }
}