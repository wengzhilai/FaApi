using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("fa_script_task_log")]
    public class FaScriptTaskLogEntity : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// 口径任务ID
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "口径任务ID")]
        [Column]
        public int SCRIPT_TASK_ID { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [Required]
        [Display(Name = "记录时间")]
        [Column]
        public DateTime LOG_TIME { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "日志级别")]
        [Column]
        public int LOG_TYPE { get; set; }
        /// <summary>
        /// 日志说明
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "日志说明")]
        [Column]
        public string MESSAGE { get; set; }
        /// <summary>
        /// SQL内容
        /// </summary>
        [Display(Name = "SQL内容")]
        [Column]
        public string SQL_TEXT { get; set; }


    }
}