
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_flow")]
    public class FaFlowEntity
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
        /// 名称
        /// </summary>
        [Required]
        [StringLength(100)]
        [Display(Name = "名称")]
        [Column]
        public string NAME { get; set; }

        /// <summary>
        /// 流程类别
        /// </summary>
        [StringLength(20)]
        [Display(Name = "流程类别")]
        [Column]
        public string FLOW_TYPE { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(100)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }
        /// <summary>
        /// 坐标信息
        /// </summary>
        [StringLength(500)]
        [Display(Name = "坐标信息")]
        [Column]
        public string X_Y { get; set; }
    }
}