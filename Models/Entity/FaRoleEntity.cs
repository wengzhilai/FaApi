
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("fa_role")]
    public class FaRoleEntity : BaseModel
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
        /// 角色名
        /// </summary>
        [StringLength(80)]
        [Display(Name = "角色名")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(255)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Column]
        public Nullable<int> TYPE { get; set; }


    }
}
