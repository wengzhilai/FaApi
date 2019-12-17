
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
        [Column("ID")]
        public int id { get; set; }
        /// <summary>
        /// FAMILY_ID
        /// </summary>
        [Display(Name = "FAMILY_ID")]
        [Column("FAMILY_ID")]
        public int familyId { get; set; }
        /// <summary>
        /// NAME
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "NAME")]
        [Column("NAME")]
        public string name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Required]
        [Display(Name = "类型")]
        [Column("TYPE_ID")]
        public int typeId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Column("SORT")]
        public int sort { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [Column("UserID")]
        public int userID { get; set; }
        /// <summary>
        /// 文件地址
        /// </summary>
        [Display(Name = "文件地址")]
        [Column("FILE_URL")]
        public string fileUrl { get; set; }


    }
}
