using System;
namespace ApiQuartz.models
{
    public class QuartzTaskModel
    {
        public QuartzTaskModel()
        {
        }
        /**
           * 名称
           * 
           */
        public String keyName;
        /**
        * 组名称
        * 
        */
        public String keyGroup;
        /**
        * 传入的参数
        * 
        */
        public String jobDataListStr;
        /**
        * 日历名称
        * 
        */
        public String calendarName;
        /**
        * 描述
        * 
        */
        public String description;
        /**
        * 结束时间
        * 
        */
        public String endTime;

        /**
        * 最后一次执行时间
        * 
        */
        public String finalFireTimeUtc;
        /**
        * 下次执行时间
        * 
        */
        public String nextFireTime;
        /**
        * 执行级别
        * 
        */
        public int priority;
        /**
        * 开始执行时间
        * 
        */
        public String startTimeUtc;
    }
}
