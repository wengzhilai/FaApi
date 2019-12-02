
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 客户
    /// </summary>
    [Table("etc_clent")]
    public class EtcClentEntity {

    
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
        /// 员工ID
        /// </summary>
        [Display(Name = "员工ID")]
        [Column("StaffId")]
        public int staffid { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [Display(Name = "客户姓名")]
        [Column("ClientName")]
        public String clientname { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        [Display(Name = "客户电话")]
        [Column("ClientPhone")]
        public String clientphone { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        [Display(Name = "绑定时间")]
        [Column("BindTime")]
        public DateTime bindtime { get; set; }

        /// <summary>
        /// 状态:已绑定已提交资料推广成功待结算已结算
        /// </summary>
        [Display(Name = "状态:已绑定已提交资料推广成功待结算已结算")]
        [Column("Status")]
        public String status { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        [Display(Name = "结算金额")]
        [Column("Money")]
        public Decimal money { get; set; }

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

        /// <summary>
        /// 结算Id
        /// </summary>
        [Display(Name = "结算Id")]
        [Column("WalletId")]
        public int walletid { get; set; }


    }

}
/*
select 
  Id id,
  StaffId staffid,
  ClientName clientname,
  ClientPhone clientphone,
  BindTime bindtime,
  Status status,
  Money money,
  OpUserName opusername,
  ReMark remark,
  WalletId walletid 
from etc_clent


{
  "id": {
    "title": "ID",
    "type": "int(11)",
    "editable": true
  },
  "staffid": {
    "title": "员工ID",
    "type": "int(11)",
    "editable": true
  },
  "clientname": {
    "title": "客户姓名",
    "type": "varchar(50)",
    "editable": true
  },
  "clientphone": {
    "title": "客户电话",
    "type": "varchar(50)",
    "editable": true
  },
  "bindtime": {
    "title": "绑定时间",
    "type": "datetime",
    "editable": true
  },
  "status": {
    "title": "状态:已绑定已提交资料推广成功待结算已结算",
    "type": "varchar(10)",
    "editable": true
  },
  "money": {
    "title": "结算金额",
    "type": "decimal(8,2)",
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
  },
  "walletid": {
    "title": "结算Id",
    "type": "int(11)",
    "editable": true
  }
}
*/

