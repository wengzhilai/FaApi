
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_elder")]
    public class FaElderEntity : BaseModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// FAMILY_ID
        /// </summary>
        [Display(Name = "FAMILY_ID")]
        [Column]
        public Nullable<int> FAMILY_ID { get; set; }
        /// <summary>
        /// NAME
        /// </summary>
        [Required]
        [StringLength(2)]
        [Display(Name = "NAME")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Column]
        public Nullable<int> SORT { get; set; }
    }
}
