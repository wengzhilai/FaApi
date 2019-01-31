
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 事件
    /// </summary>
    [Table("fa_user_event")]
    public class FaUserEventEntity : BaseModel
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "ID")]
        [Column]
        public int ID { get; set; }
        /// <summary>
        /// USER_ID
        /// </summary>
        [Display(Name = "USER_ID")]
        [Column]
        public Nullable<int> USER_ID { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [StringLength(50)]
        [Display(Name = "标题")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
        [Column]
        public Nullable<DateTime> HAPPEN_TIME { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(500)]
        [Display(Name = "描述")]
        [Column]
        public string CONTENT { get; set; }
        /// <summary>
        /// 地点
        /// </summary>
        [StringLength(500)]
        [Display(Name = "地点")]
        [Column]
        public string ADDRESS { get; set; }

    }
}
