
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table("fa_user_role ur LEFT JOIN fa_user u on u.ID=ur.USER_ID LEFT JOIN fa_role r on r.ID=ur.ROLE_ID ")]
    public class FaUserRoleEntityView
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [StringLength(80)]
        [Display(Name = "角色名")]
        [Column("r.`NAME` RoleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Column("r.`TYPE` RoleType")]
        public Nullable<int> TYPE { get; set; }

        /// <summary>
        /// ROLE_ID
        /// </summary>
        [Required]
        [Display(Name = "ROLE_ID")]
        [Column("ur.ROLE_ID")]
        public int ROLE_ID { get; set; }
        /// <summary>
        /// USER_ID
        /// </summary>
        [Required]
        [Key]
        [Display(Name = "USER_ID")]
        [Column("ur.USER_ID")]
        public int USER_ID { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [StringLength(80)]
        [Display(Name = "用户姓名")]
        [Column("u.`NAME` UserName")]
        public string UserName { get; set; }
    }
}
