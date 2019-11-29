
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "ID")]
        [Column("ID")]
        public int id { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名")]
        [Column("LOGIN_NAME")]
        public String loginName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Column("PASSWORD")]
        public String password { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [Column("PHONE_NO")]
        public String phoneNo { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Display(Name = "电子邮件")]
        [Column("EMAIL_ADDR")]
        public String emailAddr { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码")]
        [Column("VERIFY_CODE")]
        public String verifyCode { get; set; }

        /// <summary>
        /// 验证码时间
        /// </summary>
        [Display(Name = "验证码时间")]
        [Column("VERIFY_TIME")]
        public DateTime verifyTime { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        [Display(Name = "是否禁用")]
        [Column("IS_LOCKED")]
        public int isLocked { get; set; }

        /// <summary>
        /// 密码修改时间
        /// </summary>
        [Display(Name = "密码修改时间")]
        [Column("PASS_UPDATE_DATE")]
        public DateTime passUpdateDate { get; set; }

        /// <summary>
        /// 禁用原因
        /// </summary>
        [Display(Name = "禁用原因")]
        [Column("LOCKED_REASON")]
        public String lockedReason { get; set; }

        /// <summary>
        /// 失败次数
        /// </summary>
        [Display(Name = "失败次数")]
        [Column("FAIL_COUNT")]
        public int failCount { get; set; }


    }

}
