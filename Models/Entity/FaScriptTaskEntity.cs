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
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// 口径脚本ID
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "口径脚本ID")]
        [Column]
        public Int64 SCRIPT_ID { get; set; }
        /// <summary>
        /// 任务脚本
        /// </summary>
        [Required]
        [Display(Name = "任务脚本")]
        [Column]
        public string BODY_TEXT { get; set; }
        /// <summary>
        /// 脚本哈希值
        /// </summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "脚本哈希值")]
        public string BODY_HASH { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "运行状态")]
        [Column]
        public string RUN_STATE { get; set; }
        /// <summary>
        /// 时间表达式
        /// </summary>
        [StringLength(30)]
        [Display(Name = "时间表达式")]
        [Column]
        public string RUN_WHEN { get; set; }
        /// <summary>
        /// 脚本参数
        /// </summary>
        [StringLength(255)]
        [Display(Name = "脚本参数")]
        [Column]
        public string RUN_ARGS { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        [StringLength(20)]
        [Display(Name = "运行时间")]
        [Column]
        public string RUN_DATA { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "日志类型")]
        [Column]
        public Nullable<Int16> LOG_TYPE { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [StringLength(255)]
        [Display(Name = "任务类型")]
        [Column]
        public string DSL_TYPE { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        [StringLength(10)]
        [Display(Name = "任务状态")]
        [Column]
        public string RETURN_CODE { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Column]
        public Nullable<DateTime> START_TIME { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        [Column]
        public Nullable<DateTime> END_TIME { get; set; }
        /// <summary>
        /// 禁用时间
        /// </summary>
        [Display(Name = "禁用时间")]
        [Column]
        public Nullable<DateTime> DISABLE_DATE { get; set; }
        /// <summary>
        /// 禁用原因
        /// </summary>
        [StringLength(50)]
        [Display(Name = "禁用原因")]
        [Column]
        public string DISABLE_REASON { get; set; }
        /// <summary>
        /// 服务标识
        /// </summary>
        [StringLength(50)]
        [Display(Name = "服务标识")]
        [Column]
        public string SERVICE_FLAG { get; set; }

    }
}