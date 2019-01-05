
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table("fa_user")]
    public class FaUserEntity : BaseModel
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
        /// 姓名
        /// </summary>
        [StringLength(80)]
        [Display(Name = "姓名")]
        [Column]
        public string NAME { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        [StringLength(20)]
        [Display(Name = "登录名")]
        [Column]
        public string LOGIN_NAME { get; set; }
        /// <summary>
        /// 头像图片
        /// </summary>
        [Display(Name = "头像图片")]
        [Column]
        public Nullable<int> ICON_FILES_ID { get; set; }
        /// <summary>
        /// 归属地
        /// </summary>
        [Required]
        [Display(Name = "归属地")]
        [Column]
        public int DISTRICT_ID { get; set; }
        /// <summary>
        /// 锁定
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "锁定")]
        [Column]
        public Nullable<decimal> IS_LOCKED { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column]
        public Nullable<DateTime> CREATE_TIME { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        [Display(Name = "登录次数")]
        [Column]
        public Nullable<int> LOGIN_COUNT { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        [Column]
        public Nullable<DateTime> LAST_LOGIN_TIME { get; set; }
        /// <summary>
        /// 最后离开时间
        /// </summary>
        [Display(Name = "最后离开时间")]
        [Column]
        public Nullable<DateTime> LAST_LOGOUT_TIME { get; set; }
        /// <summary>
        /// 最后活动时间
        /// </summary>
        [Display(Name = "最后活动时间")]
        [Column]
        public Nullable<DateTime> LAST_ACTIVE_TIME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }

    }
}
