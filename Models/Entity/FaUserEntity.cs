
using System;
using System.Collections.Generic;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        /// 头像
        /// </summary>
        [StringLength(100)]
        [Display(Name = "头像")]
        [Column]
        public string ICON_FILES { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "区域")]
        [Column]
        public int DISTRICT_ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "状态")]
        [Column]
        public decimal IS_LOCKED { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column]
        public DateTime CREATE_TIME { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>
        [Range(0, 2147483647)]
        [Display(Name = "登录次数")]
        [Column]
        public int LOGIN_COUNT { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        [Column]
        public DateTime LAST_LOGIN_TIME { get; set; }
        /// <summary>
        /// 最后离开时间
        /// </summary>
        [Display(Name = "最后离开时间")]
        [Column]
        public DateTime LAST_LOGOUT_TIME { get; set; }
        /// <summary>
        /// 上次活动时间
        /// </summary>
        [Display(Name = "上次活动时间")]
        [Column]
        public DateTime LAST_ACTIVE_TIME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [StringLength(2000)]
        [Display(Name = "备注")]
        [Column]
        public string REMARK { get; set; }


        /// <summary>
        /// 是管理管理员
        /// </summary>
        /// <value></value>
        public bool IsAdmin{ get; set; }
        
        /// <summary>
        /// 是普通管理员
        /// </summary>
        /// <value></value>
        public bool IsLeader{ get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        /// <value></value>
        public List<int> roleIdList { get; set; }
        /// <summary>
        /// 可编辑的用户ID
        /// </summary>
        /// <value></value>
        public List<int> CanEditIdList { get; set; }
    }
}
