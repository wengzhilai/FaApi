
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    /// <summary>
    /// 家谱
    /// </summary>
    [Table("fa_family_books")]
    public class FaFamilyBooksEntity : BaseModel
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
            [StringLength(20)]
            [Display(Name = "NAME")]
            [Column]
            public string NAME { get; set; }
            /// <summary>
            /// 类型
            /// </summary>
            [Required]
            [Display(Name = "类型")]
            [Column]
            public int TYPE_ID { get; set; }
            /// <summary>
            /// 排序
            /// </summary>
            [Display(Name = "排序")]
            [Column]
            public Nullable<int> SORT { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            [Display(Name = "用户ID")]
            [Column]
            public Nullable<int> UserID { get; set; }
            /// <summary>
            /// 文件ID
            /// </summary>
            [Display(Name = "文件ID")]
            [Column]
            public Nullable<int> FileID { get; set; }
            
       
    }
}
