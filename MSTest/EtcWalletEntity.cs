
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 钱包
    /// </summary>
    [Table("etc_wallet")]
    public class EtcWalletEntity {

    
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
/*
select 
  Id id,
  Money money,
  CreateTime createtime,
  ClientNum clientnum,
  WalletAccountType walletaccounttype,
  WalletAccount walletaccount,
  WalletAccountName walletaccountname,
  Status status,
  OpUserName opusername,
  ReMark remark 
from etc_wallet


{
  "id": {
    "title": "ID",
    "type": "int(11)",
    "editable": true
  },
  "money": {
    "title": "金额",
    "type": "decimal(8,2)",
    "editable": true
  },
  "createtime": {
    "title": "创建时间",
    "type": "datetime",
    "editable": true
  },
  "clientnum": {
    "title": "客户数",
    "type": "int(11)",
    "editable": true
  },
  "walletaccounttype": {
    "title": "账号类型:WeiChatAliPay",
    "type": "int(11)",
    "editable": true
  },
  "walletaccount": {
    "title": "钱包账号",
    "type": "varchar(40)",
    "editable": true
  },
  "walletaccountname": {
    "title": "钱包账号名",
    "type": "varchar(40)",
    "editable": true
  },
  "status": {
    "title": "状态:未发发已发放",
    "type": "varchar(10)",
    "editable": true
  },
  "opusername": {
    "title": "操作人员姓名",
    "type": "varchar(50)",
    "editable": true
  },
  "remark": {
    "title": "备注",
    "type": "varchar(50)",
    "editable": true
  }
}
*/

