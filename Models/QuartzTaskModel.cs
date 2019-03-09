namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QuartzTaskModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        /// <value></value>
        public string KeyName { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        /// <value></value>
        public string KeyGroup { get; set; }
        /// <summary>
        /// 传入的参数
        /// </summary>
        /// <value></value>
        public string JobDataListStr { get; set; }
        /// <summary>
        /// 日历名称
        /// </summary>
        /// <value></value>
        public string CalendarName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <value></value>
        public string EndTime { get; set; }

        /// <summary>
        /// 最后一次执行时间
        /// </summary>
        /// <value></value>
        public string FinalFireTimeUtc { get; set; }
        /// <summary>
        /// 下次执行时间
        /// </summary>
        /// <value></value>
        public string NextFireTime { get; set; }
        /// <summary>
        /// 执行级别
        /// </summary>
        /// <value></value>
        public int Priority { get; set; }
        /// <summary>
        /// 开始执行时间
        /// </summary>
        /// <value></value>
        public string StartTimeUtc { get; set; }
    }
}