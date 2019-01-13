
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 账号登录日志
    /// </summary>
    [Table("fa_login_history")]
    public class FaLoginHistoryEntity : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <value></value>
        public int ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        /// <value></value>
        public Nullable<int> USER_ID { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        /// <value></value>
        public Nullable<System.DateTime> LOGIN_TIME { get; set; }
        /// <summary>
        /// 操作IP
        /// </summary>
        /// <value></value>
        public string LOGIN_HOST { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <value></value>
        public Nullable<System.DateTime> LOGOUT_TIME { get; set; }
        /// <summary>
        /// 登录操作类型，1表示登录，2表示退出，3操作
        /// </summary>
        /// <value></value>
        public Nullable<int> LOGIN_HISTORY_TYPE { get; set; }
        public string MESSAGE { get; set; }
    }
}
