
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 登录
    /// </summary>
    [Table("fa_sms_send")]
    public class FaSmsSendEntity : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// <value></value>
        public string GUID { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        /// <value></value>
        public Nullable<int> MESSAGE_ID { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        /// <value></value>
        public string PHONE_NO { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <value></value>
        public Nullable<System.DateTime> ADD_TIME { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        /// <value></value>
        public Nullable<System.DateTime> SEND_TIME { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        /// <value></value>
        public string CONTENT { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <value></value>
        public string STAUTS { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        /// <value></value>
        public int TRY_NUM { get; set; }
    }
}
