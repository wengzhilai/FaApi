using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEtc.Models.Entity
{
    /// <summary>
    /// 员工
    /// </summary>
    [Table("etc_staff")]
    public class EtcStaffEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        [Column("Id")]
        public int id { get; set; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        [Display(Name = "微信OpenId")]
        [Column("OpenId")]
        public String openid { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Column("Name")]
        public String name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Column("Phone")]
        public String phone { get; set; }

        /// <summary>
        /// 二维码地址
        /// </summary>
        [Display(Name = "二维码地址")]
        [Column("QrCode")]
        public String qrCode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CreateTime")]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 状态:正常冻结
        /// </summary>
        [Required]
        [Display(Name = "状态:正常冻结")]
        [Column("Status")]
        public String status { get; set; }

        /// <summary>
        /// 微信账号
        /// </summary>
        [Required]
        [Display(Name = "微信账号")]
        [Column("AccountWeichat")]
        public String accountWeichat { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        [Required]
        [Display(Name = "支付宝账号")]
        [Column("AccountAliPay")]
        public String accountalipay { get; set; }


    }

}
