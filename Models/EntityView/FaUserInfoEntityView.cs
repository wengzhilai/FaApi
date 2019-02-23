
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.EntityView
{
    /// <summary>
    /// 用户扩展
    /// </summary>
    [Table("fa_user_info a left join fa_user b on a.ID=b.ID LEFT JOIN fa_user father on father.ID=a.FATHER_ID LEFT JOIN fa_user Couple on Couple.ID=a.COUPLE_ID LEFT JOIN fa_elder elder on elder.ID=a.ELDER_ID")]
    public class FaUserInfoEntityView
    {



        /// <summary>
        /// 父亲姓名
        /// </summary>
        [Column("father.`NAME` FatherName")]
        public string FatherName { get; set; }

        /// <summary>
        /// 辈分
        /// </summary>
        [Column("elder.`NAME` ElderName")]
        public string ElderName { get; set; }

        /// <summary>
        /// 辈分排号
        /// </summary>
        [Column("elder.SORT ElderSort")]
        public string ElderSort { get; set; }

        /// <summary>
        /// 配偶姓名
        /// </summary>
        [Column("Couple.`NAME` CoupleName")]
        public string CoupleName { get; set; }


        /// <summary>
        /// 子女数
        /// </summary>
        [Column("(SELECT count(1) from fa_user_info x where x.FATHER_ID=a.ID) ChildNum")]
        public int ChildNum { get; set; }

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
        /// 权限
        /// </summary>
        [Display(Name = "权限")]
        [Column("a.AUTHORITY")]
        public int AUTHORITY { get; set; }




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
        /// 归属地
        /// </summary>
        [Required]
        [Display(Name = "归属地")]
        [Column("b.`DISTRICT_ID`")]
        public int DISTRICT_ID { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "锁定")]
        [Column("b.`IS_LOCKED`")]
        public Nullable<decimal> IS_LOCKED { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("b.`CREATE_TIME`")]
        public Nullable<DateTime> CREATE_TIME { get; set; }

        /// <summary>
        /// 出生阴历
        /// </summary>
        /// <value></value>
        public string BirthdaylunlarDate{get;set;}

        /// <summary>
        /// 出生阳历
        /// </summary>
        /// <value></value>
        public string BirthdaysolarDate{get;set;}


        /// <summary>
        /// 逝世阴历
        /// </summary>
        /// <value></value>
        public string DiedlunlarDate{get;set;}

        /// <summary>
        /// 逝世阳历
        /// </summary>
        /// <value></value>
        public string DiedsolarDate{get;set;}
    }
}
