
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 用户扩展
    /// </summary>
    [Table("fa_user_info")]
    public class FaUserInfoEntity : BaseModel
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
        /// LEVEL_ID
        /// </summary>
        [Display(Name = "LEVEL_ID")]
        [Column]
        public Nullable<int> LEVEL_ID { get; set; }
        /// <summary>
        /// FAMILY_ID
        /// </summary>
        [Display(Name = "FAMILY_ID")]
        [Column]
        public Nullable<int> FAMILY_ID { get; set; }
        /// <summary>
        /// ELDER_ID
        /// </summary>
        [Display(Name = "ELDER_ID")]
        [Column]
        public Nullable<int> ELDER_ID { get; set; }

        /// <summary>
        /// 父亲ID
        /// </summary>
        [Display(Name = "父亲ID")]
        [Column]
        public Nullable<int> FATHER_ID { get; set; }

        /// <summary>
        /// 配偶ID
        /// </summary>
        [Display(Name = "配偶ID")]
        [Column]
        public Nullable<int> COUPLE_ID { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [Column]
        public Nullable<DateTime> BIRTHDAY_TIME { get; set; }
        /// <summary>
        /// 出生地点
        /// </summary>
        [StringLength(500)]
        [Display(Name = "出生地点")]
        [Column]
        public string BIRTHDAY_PLACE { get; set; }
        /// <summary>
        /// 是否健在
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "是否健在")]
        [Column]
        public Nullable<decimal> IS_LIVE { get; set; }
        /// <summary>
        /// 过逝日期
        /// </summary>
        [Display(Name = "过逝日期")]
        [Column]
        public Nullable<DateTime> DIED_TIME { get; set; }
        /// <summary>
        /// 过逝地点
        /// </summary>
        [StringLength(500)]
        [Display(Name = "过逝地点")]
        [Column]
        public string DIED_PLACE { get; set; }
        /// <summary>
        /// 日期类型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "日期类型")]
        [Column]
        public string YEARS_TYPE { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(2)]
        [Display(Name = "性别")]
        [Column]
        public string SEX { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [StringLength(10)]
        [Display(Name = "状态")]
        [Column]
        public string STATUS { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        [Display(Name = "创建时间")]
        [Column]
        public DateTime CREATE_TIME { get; set; }
        /// <summary>
        /// 创建用户的姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "创建用户的姓名")]
        [Column]
        public string CREATE_USER_NAME { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Required]
        [Display(Name = "创建用户ID")]
        [Column]
        public int CREATE_USER_ID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Required]
        [Display(Name = "修改时间")]
        [Column]
        public DateTime UPDATE_TIME { get; set; }
        /// <summary>
        /// 修改用户的姓名
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "修改用户的姓名")]
        [Column]
        public string UPDATE_USER_NAME { get; set; }
        /// <summary>
        /// 修改用户的ID
        /// </summary>
        [Required]
        [Display(Name = "修改用户的ID")]
        [Column]
        public int UPDATE_USER_ID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        /// <value></value>
        [Display(Name = "别名")]
        [Column]
        public string ALIAS { get; set; }

        /// <summary>
        /// 权限
        ///  * 获取权限列表
        ///  * 权限字符串，第一位表示创建者，第二位管理员，第三位表示超级管理员
        ///  * 判断的权限，1添加，2修改，4查看
        /// </summary>
        [Display(Name = "权限")]
        [Column]
        public int AUTHORITY { get; set; }

        /// <summary>
        /// 教育背影
        /// </summary>
        /// <value></value>
        [StringLength(20)]
        [Display(Name = "教育背影")]
        [Column]
        public string EDUCATION { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        /// <value></value>
        [StringLength(100)]
        [Display(Name = "行业")]
        [Column]
        public string INDUSTRY { get; set; }


        /// <summary>
        /// 出生国号
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        [Display(Name = "出生国号")]
        [Column]
        public string BIRTHDAY_CHINA_YEAR { get; set; }

        /// <summary>
        /// 过逝国号
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        [Display(Name = "过逝国号")]
        [Column]
        public string DIED_CHINA_YEAR { get; set; }
    }
}
