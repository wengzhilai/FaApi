using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 流程节点步骤
    /// </summary>
    [Table("fa_flow_node_flow")]
    public class FaFlowFlownodeFlowEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// FLOW_ID
        /// </summary>
        [Display(Name = "FLOW_ID")]
        [Column]
        public int FLOW_ID { get; set; }
        /// <summary>
        /// 开始节点
        /// </summary>
        [Display(Name = "开始节点")]
        [Column]
        public int FROM_NODE_ID { get; set; }
        /// <summary>
        /// 结束节点
        /// </summary>
        [Display(Name = "结束节点")]
        [Column]
        public int TO_NODE_ID { get; set; }
        /// <summary>
        /// 处理方式 0一人处理即可,1所有人必须处理
        /// </summary>
        [Required]
        [Display(Name = "处理方式")]
        [Column]
        public short HANDLE { get; set; }

        /// <summary>
        /// 选择人 0,指定角色,1操作时选择人,2返回上级,3发起人,4已处理人
        /// </summary>
        [Required]
        [Display(Name = "选择人")]
        [Column]
        public short ASSIGNER { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [StringLength(20)]
        [Display(Name = "状态名称")]
        [Column]
        public string STATUS { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(20)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }


        /// <summary>
        /// 处理时长(小时)
        /// </summary>
        [Display(Name = "处理时长(小时)")]
        [Column]
        public int? EXPIRE_HOUR { get; set; }
    }
}