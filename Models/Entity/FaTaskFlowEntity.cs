
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_task_flow")]
    public class FaTaskFlowEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }

        /// <summary>
        /// PARENT_ID
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "PARENT_ID")]
        [Column]
        public Nullable<int> PARENT_ID { get; set; }

        /// <summary>
        /// 相同节点
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "EQUAL_ID")]
        [Column]
        public Nullable<int> EQUAL_ID { get; set; }

        /// <summary>
        /// TASK_ID
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "TASK_ID")]
        [Column]
        public int TASK_ID { get; set; }
        /// <summary>
        /// LEVEL_ID
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "LEVEL_ID")]
        [Column]
        public Nullable<int> LEVEL_ID { get; set; }
        /// <summary>
        /// 当前节点ID
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "当前节点ID")]
        [Column]
        public Nullable<int> FLOWNODE_ID { get; set; }

        /// <summary>
        /// 是否处理
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "是否处理")]
        [Column]
        public int IS_HANDLE { get; set; }
        /// <summary>
        /// 处理状态类型
        /// </summary>
        [StringLength(50)]
        [Display(Name = "处理状态类型")]
        [Column]
        public string DEAL_STATUS { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(100)]
        [Display(Name = "名称")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 处理地址
        /// </summary>
        [StringLength(200)]
        [Display(Name = "处理地址")]
        [Column]
        public string HANDLE_URL { get; set; }
        /// <summary>
        /// 展示地址
        /// </summary>
        [StringLength(200)]
        [Display(Name = "展示地址")]
        [Column]
        public string SHOW_URL { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [Display(Name = "过期时间")]
        [Column]
        public Nullable<DateTime> EXPIRE_TIME { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [Display(Name = "处理时间")]
        [Column]
        public Nullable<DateTime> DEAL_TIME { get; set; }


        /// <summary>
        /// 受理时间
        /// </summary>
        [Display(Name = "受理时间")]
        [Column]
        public Nullable<DateTime> ACCEPT_TIME { get; set; }

        /// <summary>
        /// 指定人
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "指定人")]
        [Column]
        public Nullable<int> HANDLE_USER_ID { get; set; }
    }
}