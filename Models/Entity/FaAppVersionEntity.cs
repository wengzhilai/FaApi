
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_app_version")]
    public class FaAppVersionEntity : BaseModel
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
        /// 是否是新的
        /// </summary>
        [Display(Name = "IS_NEW")]
        [Column]
        public int IS_NEW { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "版本号")]
        [Column]
        public int TYPE { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(1000)]
        [Display(Name = "说明")]
        [Column]
        public string REMARK { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Column]
        public DateTime? UPDATE_TIME { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        [StringLength(200)]
        [Display(Name = "下载地址")]
        [Column]
        public string UPDATE_URL { get; set; }
    }
}
