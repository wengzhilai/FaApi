
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
        [Range(0, 2147483647)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [Key]
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
        /// 电话号码
        /// </summary>
        [StringLength(20)]
        [Display(Name = "电话号码")]
        [Column]
        public string PHONE_NO { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        [StringLength(255)]
        [Display(Name = "电子邮件")]
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
        /// 验证码时间
        /// </summary>
        [Display(Name = "验证码时间")]
        [Column]
        public DateTime VERIFY_TIME { get; set; }
        /// <summary>
        /// 是否禁用
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "是否禁用")]
        [Column]
        public int IS_LOCKED { get; set; }
        /// <summary>
        /// 更新密码时间
        /// </summary>
        [Display(Name = "更新密码时间")]
        [Column]
        public DateTime PASS_UPDATE_DATE { get; set; }
        /// <summary>
        /// 禁用原因
        /// </summary>
        [StringLength(255)]
        [Display(Name = "禁用原因")]
        [Column]
        public string LOCKED_REASON { get; set; }
        /// <summary>
        /// 登录失败次数
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "登录失败次数")]
        [Column]
        public int FAIL_COUNT { get; set; }


    }

}
