
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Entity;

namespace Models.EntityView
{
    /// <summary>
    /// 用户扩展
    /// </summary>
    [Table("fa_user_info a left join fa_user b on a.ID=b.ID LEFT JOIN fa_user Couple on Couple.ID=a.COUPLE_ID")]
    public class FaUserBookEntityView
    {

        /// <summary>
        /// 配偶姓名
        /// </summary>
        [Column("Couple.`NAME` CoupleName")]
        public string CoupleName { get; set; }



        /// <summary>
        /// 所有儿子
        /// </summary>
        [Column("(SELECT GROUP_CONCAT(b1.`NAME`,',') modelName from fa_user_info a1 LEFT JOIN fa_user b1 on a1.ID=b1.ID where a1.FATHER_ID=a.ID and a1.SEX='男' ORDER BY a1.LEVEL_ID) ChildSons")]
        public string ChildSons { get; set; }

        /// <summary>
        /// 所有女儿
        /// </summary>
        [Column("(SELECT GROUP_CONCAT(b1.`NAME`,',') modelName from fa_user_info a1 LEFT JOIN fa_user b1 on a1.ID=b1.ID where a1.FATHER_ID=a.ID and a1.SEX='女' ORDER BY a1.LEVEL_ID) ChildDaughters")]
        public string ChildDaughters { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [Column("a.ID")]
        public int ID { get; set; }
        /// <summary>
        /// LEVEL_ID
        /// </summary>
        [Display(Name = "LEVEL_ID")]
        [Column("a.LEVEL_ID")]
        public Nullable<int> LEVEL_ID { get; set; }

        /// <summary>
        /// ELDER_ID
        /// </summary>
        [Display(Name = "ELDER_ID")]
        [Column("a.ELDER_ID")]
        public Nullable<int> ELDER_ID { get; set; }

        /// <summary>
        /// 父亲ID
        /// </summary>
        [Display(Name = "父亲ID")]
        [Column("a.FATHER_ID")]
        public Nullable<int> FATHER_ID { get; set; }

        /// <summary>
        /// 配偶ID
        /// </summary>
        [Display(Name = "配偶ID")]
        [Column("a.COUPLE_ID")]
        public Nullable<int> COUPLE_ID { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [Column("a.BIRTHDAY_TIME")]
        public Nullable<DateTime> BIRTHDAY_TIME { get; set; }
        /// <summary>
        /// 出生地点
        /// </summary>
        [StringLength(500)]
        [Display(Name = "出生地点")]
        [Column("a.BIRTHDAY_PLACE")]
        public string BIRTHDAY_PLACE { get; set; }
        /// <summary>
        /// 是否健在
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "是否健在")]
        [Column("a.IS_LIVE")]
        public Nullable<decimal> IS_LIVE { get; set; }
        /// <summary>
        /// 过逝日期
        /// </summary>
        [Display(Name = "过逝日期")]
        [Column("a.DIED_TIME")]
        public Nullable<DateTime> DIED_TIME { get; set; }
        /// <summary>
        /// 过逝地点
        /// </summary>
        [StringLength(500)]
        [Display(Name = "过逝地点")]
        [Column("a.DIED_PLACE")]
        public string DIED_PLACE { get; set; }
        /// <summary>
        /// 日期类型
        /// </summary>
        [StringLength(10)]
        [Display(Name = "日期类型")]
        [Column("a.YEARS_TYPE")]
        public string YEARS_TYPE { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        [StringLength(2)]
        [Display(Name = "性别")]
        [Column("a.SEX")]
        public string SEX { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [StringLength(10)]
        [Display(Name = "状态")]
        [Column("a.STATUS")]
        public string STATUS { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        /// <value></value>
        [Display(Name = "别名")]
        [Column("a.ALIAS")]
        public string ALIAS { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = "权限")]
        [Column("a.AUTHORITY")]
        public int AUTHORITY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(500)]
        [Display(Name = "备注")]
        [Column("a.REMARK")]
        public string REMARK { get; set; }

        /// <summary>
        /// 教育背影
        /// </summary>
        /// <value></value>
        [StringLength(20)]
        [Display(Name = "教育背影")]
        [Column("a.EDUCATION")]
        public string EDUCATION { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        /// <value></value>
        [StringLength(100)]
        [Display(Name = "行业")]
        [Column("a.INDUSTRY")]
        public string INDUSTRY { get; set; }


        /// <summary>
        /// 出生国号
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        [Display(Name = "出生国号")]
        [Column("a.BIRTHDAY_CHINA_YEAR")]
        public string BIRTHDAY_CHINA_YEAR { get; set; }

        /// <summary>
        /// 过逝国号
        /// </summary>
        /// <value></value>
        [StringLength(50)]
        [Display(Name = "过逝国号")]
        [Column("a.DIED_CHINA_YEAR")]
        public string DIED_CHINA_YEAR { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Required]
        [Display(Name = "创建用户ID")]
        [Column("a.CREATE_USER_ID")]
        public int CREATE_USER_ID { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [StringLength(80)]
        [Display(Name = "姓名")]
        [Column("b.NAME")]
        public string NAME { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "登录名")]
        [Column("b.`LOGIN_NAME`")]
        public string LOGIN_NAME { get; set; }
        /// <summary>
        /// 头像图片
        /// </summary>
        [Display(Name = "头像图片")]
        [Column("b.`ICON_FILES_ID`")]
        public Nullable<int> ICON_FILES_ID { get; set; }


        /// <summary>
        /// 出生阴历
        /// </summary>
        /// <value></value>
        public string BirthdaylunlarDate { get; set; }

        /// <summary>
        /// 出生阳历
        /// </summary>
        /// <value></value>
        public string BirthdaysolarDate { get; set; }


        /// <summary>
        /// 逝世阴历
        /// </summary>
        /// <value></value>
        public string DiedlunlarDate { get; set; }

        /// <summary>
        /// 逝世阳历
        /// </summary>
        /// <value></value>
        public string DiedsolarDate { get; set; }


    }
}
