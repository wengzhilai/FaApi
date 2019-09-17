
using System;
using System.Collections.Generic;
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
        [Range(0, 2147483647)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }

        /// <summary>
        /// 表别名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "表别名")]
        [Column]
        public string NAME { get; set; }

        /// <summary>
        /// 数据库中表名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "数据库中表名")]
        [Column]
        public string TABLE_NAME { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
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


        /// <summary>
        /// 表的所有列
        /// </summary>
        /// <value></value>
        public List<FaTableColumnEntity> AllColumns{get;set;}=new List<FaTableColumnEntity>();


    }

}
