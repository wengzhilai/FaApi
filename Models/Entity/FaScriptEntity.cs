using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("fa_script")]
    public class FaScriptEntity : BaseModel
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
        /// 代码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "代码")]
        [Column]
        public string CODE { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "名称")]
        [Column]
        public string NAME { get; set; }
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
        [Column]
        public string BODY_HASH { get; set; }
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
        [Required]
        [StringLength(20)]
        [Display(Name = "运行时间")]
        [Column]
        public string RUN_DATA { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [StringLength(10)]
        [Display(Name = "状态")]
        [Column]
        public string STATUS { get; set; }
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

        [Range(0, 2147483647)]
        [Display(Name = "GROUP_ID")]
        [Column]
        public int? GROUP_ID { get; set; }

        [Range(0, 2147483647)]
        [Display(Name = "GROUP_ID")]
        [Column]
        public int ORDER_INDEX { get; set; }

    }
}