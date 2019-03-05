
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_task_flow_handle")]
    public class FaTaskFlowHandleEntity
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
        /// TASK_FLOW_ID
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "TASK_FLOW_ID")]
        [Column]
        public Nullable<int> TASK_FLOW_ID { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "处理人")]
        [Column]
        public Nullable<int> DEAL_USER_ID { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "处理人姓名")]
        [Column]
        public string DEAL_USER_NAME { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        [Display(Name = "处理时间")]
        [Column]
        public Nullable<DateTime> DEAL_TIME { get; set; }

        /// <summary>
        /// 处理说明
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "处理说明")]
        [Column]
        public string CONTENT { get; set; }
    }
}