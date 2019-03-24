
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 自定义表的类型
    /// </summary>
    [Table("fa_equipment")]
    public class FaEquipmentEntity : BaseModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }

        /// <summary>
        /// 设备名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "设备名")]
        [Column]
        public string NAME { get; set; }


        /// <summary>
        /// 上级ID
        /// </summary>
        /// <value></value>
        [Display(Name = "上级ID")]
        [Column]
        public int? PARENT_ID { get; set; }


        /// <summary>
        /// 自定义表ID
        /// </summary>
        /// <value></value>
        [Display(Name = "自定义表ID")]
        [Column]
        public int TABLE_TYPE_ID { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "介绍")]
        [Column]
        public string INTRODUCE { get; set; }

        /// <summary>
        /// 状态,禁用，启用
        /// </summary>
        [StringLength(15)]
        [Display(Name = "状态")]
        [Column]
        public string STAUTS { get; set; }

    }
}