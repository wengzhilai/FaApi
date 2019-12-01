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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column("ID")]
        public int id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "代码")]
        [Column("CODE")]
        public string code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(255)]
        [Display(Name = "名称")]
        [Column("NAME")]
        public string name { get; set; }
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
        [Required]
        [StringLength(20)]
        [Display(Name = "运行时间")]
        [Column("RUN_DATA")]
        public string runData { get; set; }

        /// <summary>
        /// 状态（正常，停用）
        /// </summary>
        [Required]
        [StringLength(10)]
        [Display(Name = "状态")]
        [Column("STATUS")]
        public string status { get; set; }
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
        /// 归属组ID
        /// </summary>
        /// <value></value>
        [Range(0, 2147483647)]
        [Display(Name = "GROUP_ID")]
        [Column("GROUP_ID")]
        public int groupId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        /// <value></value>
        [Range(0, 2147483647)]
        [Display(Name = "排序")]
        [Column("ORDER_INDEX")]
        public int orderIndex { get; set; }

    }
}