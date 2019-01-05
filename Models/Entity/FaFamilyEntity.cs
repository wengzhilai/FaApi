
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
            [Required]
            [Display(Name = "ID")]
            public int ID { get; set; }
            /// <summary>
            /// NAME
            /// </summary>
            [Required]
            [StringLength(20)]
            [Display(Name = "NAME")]
            public string NAME { get; set; }
            
       
    }
}