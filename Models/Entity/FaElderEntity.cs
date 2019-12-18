
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
        [StringLength(2)]
        [Display(Name = "NAME")]
        [Column("NAME")]
        public string name { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [Column("SORT")]
        public int sort { get; set; }

        /// <summary>
        /// 所有用户，用于家谱接口
        /// </summary>
        /// <value></value>
        public List<FaUserBookEntityView> allUser { get; set; }
    }
}
