
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录历史
    /// </summary>
    [Table("fa_login_history")]
    public class FaLoginHistoryEntity : BaseModel
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
        /// USER_ID
        /// </summary>
        [Display(Name = "USER_ID")]
        [Column]
        public Nullable<int> USER_ID { get; set; }
        /// <summary>
        /// LOGIN_TIME
        /// </summary>
        [Display(Name = "LOGIN_TIME")]
        [Column]
        public Nullable<DateTime> LOGIN_TIME { get; set; }
        /// <summary>
        /// LOGIN_HOST
        /// </summary>
        [StringLength(255)]
        [Display(Name = "LOGIN_HOST")]
        [Column]
        public string LOGIN_HOST { get; set; }
        /// <summary>
        /// LOGOUT_TIME
        /// </summary>
        [Display(Name = "LOGOUT_TIME")]
        [Column]
        public Nullable<DateTime> LOGOUT_TIME { get; set; }
        /// <summary>
        /// LOGIN_HISTORY_TYPE
        /// </summary>
        [Display(Name = "LOGIN_HISTORY_TYPE")]
        [Column]
        public Nullable<int> LOGIN_HISTORY_TYPE { get; set; }
        /// <summary>
        /// MESSAGE
        /// </summary>
        [StringLength(255)]
        [Display(Name = "MESSAGE")]
        [Column]
        public string MESSAGE { get; set; }
    }
}
