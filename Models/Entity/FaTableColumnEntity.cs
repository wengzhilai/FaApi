
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 自定义表的列
    /// </summary>
    [Table("fa_table_column")]
    public class FaTableColumnEntity : BaseModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }

        /// <summary>
        /// 自定义表ID
        /// </summary>
        /// <value></value>
        [Display(Name = "自定义表ID")]
        [Column]
        public int TABLE_TYPE_ID { get; set; }

        /// <summary>
        /// 列别名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "列别名")]
        [Column]
        public string NAME { get; set; }

        /// <summary>
        /// 数据库中列名
        /// </summary>
        [StringLength(50)]
        [Display(Name = "数据库中列名")]
        [Column]
        public string COLUMN_NAME { get; set; }

        /// <summary>
        /// 介绍
        /// </summary>
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

        /// <summary>
        /// 排序号
        /// </summary>
        [Display(Name = "排序号")]
        [Column]
        public int ORDER_INDEX { get; set; }

        /// <summary>
        /// 列类型，text,int,datatime,pic,textarea,Checkbox,Radio,auto
        /// </summary>
        [StringLength(15)]
        [Display(Name = "列类型")]
        [Column]
        public string COLUMN_TYPE { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        /// <value></value>
        [Display(Name = "字段长度")]
        [Column]
        public int? COLUMN_LONG { get; set; }

        /// <summary>
        /// 必填
        /// </summary>
        [Display(Name = "必填")]
        [Column]
        public int IS_REQUIRED { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        [StringLength(10)]
        [Display(Name = "默认值")]
        [Column]
        public string DEFAULT_VALUE { get; set; }

        /// <summary>
        /// 列配置内容
        /// </summary>
        /// <value></value>
        [StringLength(15)]
        [Display(Name = "列配置内容")]
        [Column]
        public string COLUMN_TYPE_CFG { get; set; }
        /// <summary>
        /// 权限
        ///  * 获取权限列表
        ///  * 判断的权限，1添加，2修改，4查看
        /// </summary>
        /// <value></value>
        [Display(Name = "权限")]
        [Column]
        public int AUTHORITY { get; set; }
    }

}
