
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 角色
    /// </summary>
    [Table("fa_role")]
    public class FaRoleEntity {

    
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
        /// 角色名
        /// </summary>
        [Display(Name = "角色名")]
        [Column("NAME")]
        public String name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [Column("REMARK")]
        public String remark { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Column("TYPE")]
        public int type { get; set; }


    }

}
/*
select 
  ID id,
  NAME name,
  REMARK remark,
  TYPE type 
from fa_role


{
  "id": {
    "title": "ID",
    "type": "int(11)",
    "editable": true
  },
  "name": {
    "title": "角色名",
    "type": "varchar(80)",
    "editable": true
  },
  "remark": {
    "title": "备注",
    "type": "varchar(255)",
    "editable": true
  },
  "type": {
    "title": "类型",
    "type": "int(11)",
    "editable": true
  }
}
*/

