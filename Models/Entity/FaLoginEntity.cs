
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_login")]
    public class FaLoginEntity : BaseModel
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
        /// 登录名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "登录名")]
        [Column]
        public string LOGIN_NAME { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(255)]
        [Display(Name = "密码")]
        [Column]
        public string PASSWORD { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [StringLength(20)]
        [Display(Name = "电话")]
        [Column]
        public string PHONE_NO { get; set; }
        /// <summary>
        /// 邮件
        /// </summary>
        [StringLength(255)]
        [Display(Name = "邮件")]
        [Column]
        public string EMAIL_ADDR { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [StringLength(10)]
        [Display(Name = "验证码")]
        [Column]
        public string VERIFY_CODE { get; set; }
        /// <summary>
        /// 验证时间
        /// </summary>
        [Display(Name = "验证时间")]
        [Column]
        public Nullable<DateTime> VERIFY_TIME { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "锁定")]
        [Column]
        public Nullable<decimal> IS_LOCKED { get; set; }
        /// <summary>
        /// 修改密码时间
        /// </summary>
        [Display(Name = "修改密码时间")]
        [Column]
        public Nullable<DateTime> PASS_UPDATE_DATE { get; set; }
        /// <summary>
        /// 锁定原因
        /// </summary>
        [StringLength(255)]
        [Display(Name = "锁定原因")]
        [Column]
        public string LOCKED_REASON { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        [Display(Name = "失败次数")]
        [Column]
        public Nullable<int> FAIL_COUNT { get; set; }


    }
}
