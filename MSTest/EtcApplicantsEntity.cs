
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 用户
    /// </summary>
    [Table("etc_applicants")]
    public class EtcApplicantsEntity {

    
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "id")]
        [Column("id")]
        public int id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Column("name")]
        public String name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [Column("phone")]
        public String phone { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [Required]
        [Display(Name = "城市")]
        [Column("city")]
        public String city { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("create_time")]
        public DateTime createTime { get; set; }

        /// <summary>
        /// 状态:未处理已联系
        /// </summary>
        [Display(Name = "状态:未处理已联系")]
        [Column("status")]
        public String status { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Required]
        [Display(Name = "操作人")]
        [Column("op_user")]
        public String opUser { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Required]
        [Display(Name = "操作时间")]
        [Column("op_time")]
        public DateTime opTime { get; set; }


    }

}
/*
select 
  id id,
  name name,
  phone phone,
  city city,
  create_time createTime,
  status status,
  op_user opUser,
  op_time opTime 
from etc_applicants


{
  "id": {
    "title": "id",
    "type": "int(11)",
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
  "city": {
    "title": "城市",
    "type": "varchar(20)",
    "editable": true
  },
  "createTime": {
    "title": "创建时间",
    "type": "datetime",
    "editable": true
  },
  "status": {
    "title": "状态:未处理已联系",
    "type": "varchar(10)",
    "editable": true
  },
  "opUser": {
    "title": "操作人",
    "type": "varchar(50)",
    "editable": true
  },
  "opTime": {
    "title": "操作时间",
    "type": "datetime",
    "editable": true
  }
}
*/

