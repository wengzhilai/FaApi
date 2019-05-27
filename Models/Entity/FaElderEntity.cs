
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.EntityView;

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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        /// <summary>
        /// 所有用户，用于家谱接口
        /// </summary>
        /// <value></value>
        public List<FaUserBookEntityView> AllUser { get; set; }
    }
}
