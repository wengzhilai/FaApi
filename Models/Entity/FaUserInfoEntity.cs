
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 用户
    /// </summary>
    [Table("fa_user_info")]
    public class FaUserInfoEntity
    {


        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "")]
        [Column("ID")]
        public int id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("LEVEL_ID")]
        public int levelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("FAMILY_ID")]
        public int familyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("ELDER_ID")]
        public int elderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("LEVEL_NAME")]
        public String levelName { get; set; }

        /// <summary>
        /// 父亲ID
        /// </summary>
        [Display(Name = "父亲ID")]
        [Column("FATHER_ID")]
        public int fatherId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("MOTHER_ID")]
        public int motherId { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        [Column("BIRTHDAY_TIME")]
        public DateTime birthdayTime { get; set; }

        /// <summary>
        /// 出生地点
        /// </summary>
        [Display(Name = "出生地点")]
        [Column("BIRTHDAY_PLACE")]
        public String birthdayPlace { get; set; }

        /// <summary>
        /// 是否健在
        /// </summary>
        [Display(Name = "是否健在")]
        [Column("IS_LIVE")]
        public Decimal isLive { get; set; }

        /// <summary>
        /// 过逝日期
        /// </summary>
        [Display(Name = "过逝日期")]
        [Column("DIED_TIME")]
        public DateTime diedTime { get; set; }

        /// <summary>
        /// 过逝地点
        /// </summary>
        [Display(Name = "过逝地点")]
        [Column("DIED_PLACE")]
        public String diedPlace { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [Column("REMARK")]
        public String remark { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        [Column("SEX")]
        public String sex { get; set; }

        /// <summary>
        /// 日期类型
        /// </summary>
        [Display(Name = "日期类型")]
        [Column("YEARS_TYPE")]
        public String yearsType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [Column("CONSORT_ID")]
        public int consortId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [Column("STATUS")]
        public String status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CREATE_TIME")]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 创建用户的姓名
        /// </summary>
        [Display(Name = "创建用户的姓名")]
        [Column("CREATE_USER_NAME")]
        public String createUserName { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        [Display(Name = "创建用户ID")]
        [Column("CREATE_USER_ID")]
        public int createUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Display(Name = "修改时间")]
        [Column("UPDATE_TIME")]
        public DateTime updateTime { get; set; }

        /// <summary>
        /// 修改用户的姓名
        /// </summary>
        [Display(Name = "修改用户的姓名")]
        [Column("UPDATE_USER_NAME")]
        public String updateUserName { get; set; }

        /// <summary>
        /// 修改用户的ID
        /// </summary>
        [Display(Name = "修改用户的ID")]
        [Column("UPDATE_USER_ID")]
        public int updateUserId { get; set; }

        /// <summary>
        /// 配偶ID
        /// </summary>
        [Display(Name = "配偶ID")]
        [Column("COUPLE_ID")]
        public int coupleId { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [Display(Name = "别名")]
        [Column("ALIAS")]
        public String alias { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = "权限")]
        [Column("AUTHORITY")]
        public int authority { get; set; }

        /// <summary>
        /// 教育背影
        /// </summary>
        [Display(Name = "教育背影")]
        [Column("EDUCATION")]
        public String education { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        [Display(Name = "行业")]
        [Column("INDUSTRY")]
        public String industry { get; set; }

        /// <summary>
        /// 出生国号
        /// </summary>
        [Display(Name = "出生国号")]
        [Column("BIRTHDAY_CHINA_YEAR")]
        public String birthdayChinaYear { get; set; }

        /// <summary>
        /// 过逝国号
        /// </summary>
        [Display(Name = "过逝国号")]
        [Column("DIED_CHINA_YEAR")]
        public String diedChinaYear { get; set; }


    }

}
/*
select 
  ID id,
  LEVEL_ID levelId,
  FAMILY_ID familyId,
  ELDER_ID elderId,
  LEVEL_NAME levelName,
  FATHER_ID fatherId,
  MOTHER_ID motherId,
  BIRTHDAY_TIME birthdayTime,
  BIRTHDAY_PLACE birthdayPlace,
  IS_LIVE isLive,
  DIED_TIME diedTime,
  DIED_PLACE diedPlace,
  REMARK remark,
  SEX sex,
  YEARS_TYPE yearsType,
  CONSORT_ID consortId,
  STATUS status,
  CREATE_TIME createTime,
  CREATE_USER_NAME createUserName,
  CREATE_USER_ID createUserId,
  UPDATE_TIME updateTime,
  UPDATE_USER_NAME updateUserName,
  UPDATE_USER_ID updateUserId,
  COUPLE_ID coupleId,
  ALIAS alias,
  AUTHORITY authority,
  EDUCATION education,
  INDUSTRY industry,
  BIRTHDAY_CHINA_YEAR birthdayChinaYear,
  DIED_CHINA_YEAR diedChinaYear 
from fa_user_info


{
  "id": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "levelId": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "familyId": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "elderId": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "levelName": {
    "title": "",
    "type": "varchar(2)",
    "editable": true
  },
  "fatherId": {
    "title": "父亲ID",
    "type": "int(11)",
    "editable": true
  },
  "motherId": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "birthdayTime": {
    "title": "出生日期",
    "type": "datetime",
    "editable": true
  },
  "birthdayPlace": {
    "title": "出生地点",
    "type": "varchar(500)",
    "editable": true
  },
  "isLive": {
    "title": "是否健在",
    "type": "decimal(1,0)",
    "editable": true
  },
  "diedTime": {
    "title": "过逝日期",
    "type": "datetime",
    "editable": true
  },
  "diedPlace": {
    "title": "过逝地点",
    "type": "varchar(500)",
    "editable": true
  },
  "remark": {
    "title": "备注",
    "type": "varchar(500)",
    "editable": true
  },
  "sex": {
    "title": "性别",
    "type": "varchar(2)",
    "editable": true
  },
  "yearsType": {
    "title": "日期类型",
    "type": "varchar(10)",
    "editable": true
  },
  "consortId": {
    "title": "",
    "type": "int(11)",
    "editable": true
  },
  "status": {
    "title": "状态",
    "type": "varchar(10)",
    "editable": true
  },
  "createTime": {
    "title": "创建时间",
    "type": "datetime",
    "editable": true
  },
  "createUserName": {
    "title": "创建用户的姓名",
    "type": "varchar(50)",
    "editable": true
  },
  "createUserId": {
    "title": "创建用户ID",
    "type": "int(11)",
    "editable": true
  },
  "updateTime": {
    "title": "修改时间",
    "type": "datetime",
    "editable": true
  },
  "updateUserName": {
    "title": "修改用户的姓名",
    "type": "varchar(50)",
    "editable": true
  },
  "updateUserId": {
    "title": "修改用户的ID",
    "type": "int(11)",
    "editable": true
  },
  "coupleId": {
    "title": "配偶ID",
    "type": "int(11)",
    "editable": true
  },
  "alias": {
    "title": "别名",
    "type": "varchar(10)",
    "editable": true
  },
  "authority": {
    "title": "权限",
    "type": "int(11)",
    "editable": true
  },
  "education": {
    "title": "教育背影",
    "type": "varchar(20)",
    "editable": true
  },
  "industry": {
    "title": "行业",
    "type": "varchar(100)",
    "editable": true
  },
  "birthdayChinaYear": {
    "title": "出生国号",
    "type": "varchar(50)",
    "editable": true
  },
  "diedChinaYear": {
    "title": "过逝国号",
    "type": "varchar(50)",
    "editable": true
  }
}
*/

