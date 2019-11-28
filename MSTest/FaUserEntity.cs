
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 自定义表
    /// </summary>
    [Table("fa_user")]
    public class FaUserEntity {

    
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Display(Name = "ID")]
        [Column("ID")]
        public int id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Column("NAME")]
        public String name { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Display(Name = "登录账号")]
        [Column("LOGIN_NAME")]
        public String loginName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        [Column("ICON_FILES")]
        public String iconFiles { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [Display(Name = "区域")]
        [Column("DISTRICT_ID")]
        public int districtId { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Display(Name = "是否可用")]
        [Column("IS_LOCKED")]
        public Decimal isLocked { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CREATE_TIME")]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Display(Name = "登录次数")]
        [Column("LOGIN_COUNT")]
        public int loginCount { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        [Column("LAST_LOGIN_TIME")]
        public DateTime lastLoginTime { get; set; }

        /// <summary>
        /// 最后登出时间
        /// </summary>
        [Display(Name = "最后登出时间")]
        [Column("LAST_LOGOUT_TIME")]
        public DateTime lastLogoutTime { get; set; }

        /// <summary>
        /// 最后活动时间
        /// </summary>
        [Display(Name = "最后活动时间")]
        [Column("LAST_ACTIVE_TIME")]
        public DateTime lastActiveTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [Column("REMARK")]
        public String remark { get; set; }


    }

}
/*
select 
  ID id,
  NAME name,
  LOGIN_NAME loginName,
  ICON_FILES iconFiles,
  DISTRICT_ID districtId,
  IS_LOCKED isLocked,
  CREATE_TIME createTime,
  LOGIN_COUNT loginCount,
  LAST_LOGIN_TIME lastLoginTime,
  LAST_LOGOUT_TIME lastLogoutTime,
  LAST_ACTIVE_TIME lastActiveTime,
  REMARK remark 
from fa_user


{
  "id": {
    "title": "ID",
    "type": "int(11)",
    "editable": true
  },
  "name": {
    "title": "姓名",
    "type": "varchar(80)",
    "editable": true
  },
  "loginName": {
    "title": "登录账号",
    "type": "varchar(20)",
    "editable": true
  },
  "iconFiles": {
    "title": "头像",
    "type": "varchar(100)",
    "editable": true
  },
  "districtId": {
    "title": "区域",
    "type": "int(11)",
    "editable": true
  },
  "isLocked": {
    "title": "是否可用",
    "type": "decimal(1,0)",
    "editable": true
  },
  "createTime": {
    "title": "创建时间",
    "type": "datetime",
    "editable": true
  },
  "loginCount": {
    "title": "登录次数",
    "type": "int(11)",
    "editable": true
  },
  "lastLoginTime": {
    "title": "最后登录时间",
    "type": "datetime",
    "editable": true
  },
  "lastLogoutTime": {
    "title": "最后登出时间",
    "type": "datetime",
    "editable": true
  },
  "lastActiveTime": {
    "title": "最后活动时间",
    "type": "datetime",
    "editable": true
  },
  "remark": {
    "title": "备注",
    "type": "varchar(2000)",
    "editable": true
  }
}
*/

