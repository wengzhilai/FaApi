
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table("fa_login")]
    public class FaFamilyEntity
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
        /// NAME
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name = "NAME")]
        [Column]
        public string NAME { get; set; }


    }
}