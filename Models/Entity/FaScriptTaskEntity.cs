using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("fa_script_task")]
    public class FaScriptTaskEntity : BaseModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column("ID")]
        public int id { get; set; }
        /// <summary>
        /// 口径脚本ID
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "口径脚本ID")]
        [Column("SCRIPT_ID")]
        public Int64 scriptId { get; set; }
        /// <summary>
        /// 任务脚本
        /// </summary>
        [Required]
        [Display(Name = "任务脚本")]
        [Column("BODY_TEXT")]
        public string bodyText { get; set; }
        /// <summary>
        /// 脚本哈希值
        /// </summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "脚本哈希值")]
        [Column("BODY_HASH")]
        public string bodyHash { get; set; }
        /// <summary>
        /// 运行状态(等待\运行\停止)
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "运行状态")]
        [Column("RUN_STATE")]
        public string runState { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        [StringLength(30)]
        [Display(Name = "时间表达式")]
        [Column("RUN_WHEN")]
        public string runWhen { get; set; }
        /// <summary>
        /// 脚本参数
        /// </summary>
        [StringLength(255)]
        [Display(Name = "脚本参数")]
        [Column("RUN_ARGS")]
        public string runArgs { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        [StringLength(20)]
        [Display(Name = "运行时间")]
        [Column("RUN_DATA")]
        public string runData { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "日志类型")]
        [Column("LOG_TYPE")]
        public Nullable<Int16> logType { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [StringLength(255)]
        [Display(Name = "任务类型")]
        [Column("DSL_TYPE")]
        public string dslType { get; set; }
        /// <summary>
        /// 任务状态(成功\失败)
        /// </summary>
        [StringLength(10)]
        [Display(Name = "任务状态")]
        [Column("RETURN_CODE")]
        public string returnCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Column("START_TIME")]
        public Nullable<DateTime> startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        [Column("END_TIME")]
        public Nullable<DateTime> endTime { get; set; }
        /// <summary>
        /// 禁用时间
        /// </summary>
        [Display(Name = "禁用时间")]
        [Column("DISABLE_DATE")]
        public Nullable<DateTime> disableDate { get; set; }
        /// <summary>
        /// 禁用原因
        /// </summary>
        [StringLength(50)]
        [Display(Name = "禁用原因")]
        [Column("DISABLE_REASON")]
        public string disableReason { get; set; }
        /// <summary>
        /// 服务标识
        /// </summary>
        [StringLength(50)]
        [Display(Name = "服务标识")]
        [Column("SERVICE_FLAG")]
        public string serviceFlag { get; set; }


        /// <summary>
        /// 最后一条日志
        /// </summary>
        /// <value></value>
        public FaScriptTaskLogEntity laskLog{get;set;}

    }
}