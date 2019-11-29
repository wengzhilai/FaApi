
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{

    /// <summary>
    /// 账号
    /// </summary>
    [Table("fa_module")]
    public class FaModuleEntity {

    
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [Display(Name = "id")]
        [Column("ID")]
        public int id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        [Display(Name = "父ID")]
        [Column("PARENT_ID")]
        public int parentId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [Column("NAME")]
        public String name { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>
        [Display(Name = "连接地址")]
        [Column("LOCATION")]
        public String location { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        [Column("CODE")]
        public String code { get; set; }

        /// <summary>
        /// 调试
        /// </summary>
        [Display(Name = "调试")]
        [Column("IS_DEBUG")]
        public Decimal isDebug { get; set; }

        /// <summary>
        /// 隐藏
        /// </summary>
        [Display(Name = "隐藏")]
        [Column("IS_HIDE")]
        public Decimal isHide { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Column("SHOW_ORDER")]
        public Decimal showOrder { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Column("DESCRIPTION")]
        public String description { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        [Display(Name = "图片地址")]
        [Column("IMAGE_URL")]
        public String imageUrl { get; set; }

        /// <summary>
        /// 桌面
        /// </summary>
        [Display(Name = "桌面")]
        [Column("DESKTOP_ROLE")]
        public String desktopRole { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        [Display(Name = "宽")]
        [Column("W")]
        public int w { get; set; }

        /// <summary>
        /// 高
        /// </summary>
        [Display(Name = "高")]
        [Column("H")]
        public int h { get; set; }


    }

}
/*
select 
  ID id,
  PARENT_ID parentId,
  NAME name,
  LOCATION location,
  CODE code,
  IS_DEBUG isDebug,
  IS_HIDE isHide,
  SHOW_ORDER showOrder,
  DESCRIPTION description,
  IMAGE_URL imageUrl,
  DESKTOP_ROLE desktopRole,
  W w,
  H h 
from fa_module


{
  "id": {
    "title": "id",
    "type": "int(11)",
    "editable": true
  },
  "parentId": {
    "title": "父ID",
    "type": "int(11)",
    "editable": true
  },
  "name": {
    "title": "模块名称",
    "type": "varchar(60)",
    "editable": true
  },
  "location": {
    "title": "连接地址",
    "type": "varchar(2000)",
    "editable": true
  },
  "code": {
    "title": "代码",
    "type": "varchar(20)",
    "editable": true
  },
  "isDebug": {
    "title": "调试",
    "type": "decimal(1,0)",
    "editable": true
  },
  "isHide": {
    "title": "隐藏",
    "type": "decimal(1,0)",
    "editable": true
  },
  "showOrder": {
    "title": "排序",
    "type": "decimal(2,0)",
    "editable": true
  },
  "description": {
    "title": "描述",
    "type": "varchar(2000)",
    "editable": true
  },
  "imageUrl": {
    "title": "图片地址",
    "type": "varchar(2000)",
    "editable": true
  },
  "desktopRole": {
    "title": "桌面",
    "type": "varchar(200)",
    "editable": true
  },
  "w": {
    "title": "宽",
    "type": "int(11)",
    "editable": true
  },
  "h": {
    "title": "高",
    "type": "int(11)",
    "editable": true
  }
}
*/

