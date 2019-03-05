using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 流程节点
    /// </summary>
    [Table("fa_flow_flownode")]
    public class FaFlowFlownodeEntity
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
        /// 名称
        /// </summary>
        [Required]
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
    }
}