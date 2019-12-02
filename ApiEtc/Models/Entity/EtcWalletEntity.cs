using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEtc.Models.Entity
{

    /// <summary>
    /// 钱包
    /// </summary>
    [Table("etc_wallet")]
    public class EtcWalletEntity
    {


        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "ID")]
        [Column("Id")]
        public int id { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [Display(Name = "金额")]
        [Column("Money")]
        public Decimal money { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CreateTime")]
        public DateTime createtime { get; set; }

        /// <summary>
        /// 客户数
        /// </summary>
        [Display(Name = "客户数")]
        [Column("ClientNum")]
        public int clientnum { get; set; }

        /// <summary>
        /// 账号类型:WeiChatAliPay
        /// </summary>
        [Display(Name = "账号类型:WeiChatAliPay")]
        [Column("WalletAccountType")]
        public int walletaccounttype { get; set; }

        /// <summary>
        /// 钱包账号
        /// </summary>
        [Display(Name = "钱包账号")]
        [Column("WalletAccount")]
        public String walletaccount { get; set; }

        /// <summary>
        /// 钱包账号名
        /// </summary>
        [Display(Name = "钱包账号名")]
        [Column("WalletAccountName")]
        public String walletaccountname { get; set; }

        /// <summary>
        /// 状态:未发发已发放
        /// </summary>
        [Display(Name = "状态:未发发已发放")]
        [Column("Status")]
        public String status { get; set; }

        /// <summary>
        /// 操作人员姓名
        /// </summary>
        [Display(Name = "操作人员姓名")]
        [Column("OpUserName")]
        public String opusername { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [Column("ReMark")]
        public String remark { get; set; }


    }
}
