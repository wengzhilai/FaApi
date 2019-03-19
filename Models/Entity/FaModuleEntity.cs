
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录历史
    /// </summary>
    [Table("fa_module")]
    public class FaModuleEntity : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "上级")]
        [Column]
        public Nullable<int> PARENT_ID { get; set; }
        /// <summary>
        /// 模块名
        /// </summary>
        [StringLength(60)]
        [Display(Name = "模块名")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "地址")]
        [Column]
        public string LOCATION { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        [StringLength(20)]
        [Display(Name = "代码")]
        [Column]
        public string CODE { get; set; }
        /// <summary>
        /// 调试
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "调试")]
        [Column]
        public Int16 IS_DEBUG { get; set; }
        /// <summary>
        /// 隐藏
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "隐藏")]
        [Column]
        public Int16 IS_HIDE { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [Range(0, 2147483647)]
        [Display(Name = "排序")]
        [Column]
        public Int16 SHOW_ORDER { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "描述")]
        [Column]
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "图片")]
        [Column]
        public string IMAGE_URL { get; set; }
        /// <summary>
        /// 桌面角色
        /// </summary>
        [StringLength(200)]
        [Display(Name = "桌面角色")]
        [Column]
        public string DESKTOP_ROLE { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "宽")]
        [Column]
        public Nullable<int> W { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "高")]
        [Column]
        public Nullable<int> H { get; set; }


        /// <summary>
        /// 所有子项
        /// </summary>
        /// <value></value>
        public List<FaModuleEntity> Children { get; set; }
    }
}
