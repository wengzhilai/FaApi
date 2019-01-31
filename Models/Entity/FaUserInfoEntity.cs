
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
        /// 辈字
        /// </summary>
        [StringLength(2)]
        [Display(Name = "辈字")]
        [Column]
        public string LEVEL_NAME { get; set; }
        /// <summary>
        /// 父亲ID
        /// </summary>
        [Display(Name = "父亲ID")]
        [Column]
        public Nullable<int> FATHER_ID { get; set; }
        /// <summary>
        /// 妈妈ID
        /// </summary>
        [Display(Name = "妈妈ID")]
        [Column]
        public Nullable<int> MOTHER_ID { get; set; }
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
        /// 配偶ID
        /// </summary>
        [Display(Name = "配偶ID")]
        [Column]
        public Nullable<int> CONSORT_ID { get; set; }

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
    }
}
