
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 自定义表的类型
    /// </summary>
    [Table("fa_table_type")]
    public class FaTableTypeEntity : BaseModel
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
        /// 表别名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "表别名")]
        [Column]
        public string NAME { get; set; }

        /// <summary>
        /// 数据库中表名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "数据库中表名")]
        [Column]
        public string TABLE_NAME { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "介绍")]
        [Column]
        public string INTRODUCE { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        [Column]
        public DateTime ADD_TIME { get; set; }
 
        /// <summary>
        /// 状态,禁用，启用
        /// </summary>
        [StringLength(15)]
        [Display(Name = "状态")]
        [Column]
        public string STAUTS { get; set; }


    }
}
