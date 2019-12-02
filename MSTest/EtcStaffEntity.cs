
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 员工
    /// </summary>
    [Table("etc_staff")]
    public class EtcStaffEntity {

    
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
        public String qrcode { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CreateTime")]
        public DateTime createtime { get; set; }

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
        public String accountweichat { get; set; }

        /// <summary>
        /// 支付宝账号
        /// </summary>
        [Required]
        [Display(Name = "支付宝账号")]
        [Column("AccountAliPay")]
        public String accountalipay { get; set; }


    }

}
/*
select 
  Id id,
  OpenId openid,
  Name name,
  Phone phone,
  QrCode qrcode,
  CreateTime createtime,
  Status status,
  AccountWeichat accountweichat,
  AccountAliPay accountalipay 
from etc_staff


{
  "id": {
    "title": "ID",
    "type": "int(11)",
    "editable": true
  },
  "openid": {
    "title": "微信OpenId",
    "type": "varchar(50)",
    "editable": true
  },
  "name": {
    "title": "姓名",
    "type": "varchar(50)",
    "editable": true
  },
  "phone": {
    "title": "手机号",
    "type": "varchar(50)",
    "editable": true
  },
  "qrcode": {
    "title": "二维码地址",
    "type": "varchar(50)",
    "editable": true
  },
  "createtime": {
    "title": "创建时间",
    "type": "datetime",
    "editable": true
  },
  "status": {
    "title": "状态:正常冻结",
    "type": "varchar(10)",
    "editable": true
  },
  "accountweichat": {
    "title": "微信账号",
    "type": "varchar(40)",
    "editable": true
  },
  "accountalipay": {
    "title": "支付宝账号",
    "type": "varchar(40)",
    "editable": true
  }
}
*/

