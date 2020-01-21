
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 用户
    /// </summary>
    [Table("w_wechat_info")]
    public class WWechatInfoEntity {

    
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "")]
        [Column("id")]
        public String id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required]
        [Display(Name = "用户ID")]
        [Column("user_id")]
        public String userId { get; set; }

        /// <summary>
        /// 微信帐号
        /// </summary>
        [Required]
        [Display(Name = "微信帐号")]
        [Column("account")]
        public String account { get; set; }

        /// <summary>
        /// 呢称
        /// </summary>
        [Required]
        [Display(Name = "呢称")]
        [Column("nick_name")]
        public String nickName { get; set; }

        /// <summary>
        /// 性别(1:男,2:女)
        /// </summary>
        [Required]
        [Display(Name = "性别(1:男,2:女)")]
        [Column("sex")]
        public int sex { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Required]
        [Display(Name = "头像")]
        [Column("img_url")]
        public String imgUrl { get; set; }

        /// <summary>
        /// 登录计算机名称
        /// </summary>
        [Required]
        [Display(Name = "登录计算机名称")]
        [Column("pc_name")]
        public String pcName { get; set; }

        /// <summary>
        /// 状态(1:在线,2:离线)
        /// </summary>
        [Required]
        [Display(Name = "状态(1:在线,2:离线)")]
        [Column("state")]
        public int state { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Required]
        [Display(Name = "最后登录时间")]
        [Column("last_login_time")]
        public String lastLoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("update_time")]
        public String updateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        [Display(Name = "")]
        [Column("create_time")]
        public String createTime { get; set; }


    }

}
/*
select 
  id id,
  user_id userId,
  account account,
  nick_name nickName,
  sex sex,
  img_url imgUrl,
  pc_name pcName,
  state state,
  last_login_time lastLoginTime,
  update_time updateTime,
  create_time createTime 
from w_wechat_info


{
  "id": {
    "title": "",
    "type": "bigint(20)",
    "editable": true
  },
  "userId": {
    "title": "用户ID",
    "type": "bigint(20)",
    "editable": true
  },
  "account": {
    "title": "微信帐号",
    "type": "varchar(32)",
    "editable": true
  },
  "nickName": {
    "title": "呢称",
    "type": "varchar(32)",
    "editable": true
  },
  "sex": {
    "title": "性别(1:男,2:女)",
    "type": "int(11)",
    "editable": true
  },
  "imgUrl": {
    "title": "头像",
    "type": "varchar(256)",
    "editable": true
  },
  "pcName": {
    "title": "登录计算机名称",
    "type": "varchar(64)",
    "editable": true
  },
  "state": {
    "title": "状态(1:在线,2:离线)",
    "type": "int(11)",
    "editable": true
  },
  "lastLoginTime": {
    "title": "最后登录时间",
    "type": "bigint(14)",
    "editable": true
  },
  "updateTime": {
    "title": "",
    "type": "bigint(14)",
    "editable": true
  },
  "createTime": {
    "title": "",
    "type": "bigint(14)",
    "editable": true
  }
}
*/

