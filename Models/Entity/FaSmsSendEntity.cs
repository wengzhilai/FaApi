
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{
    /// <summary>
    /// 短信发送
    /// </summary>
    [Table("fa_sms_send")]
    public class FaSmsSendEntity : BaseModel
    {

        /// <summary>
        /// GUID
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(32)]
        [Display(Name = "GUID")]
        [Column]
        public string GUID { get; set; }
        /// <summary>
        /// MESSAGE_ID
        /// </summary>
        [Display(Name = "MESSAGE_ID")]
        [Column]
        public Nullable<int> MESSAGE_ID { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Display(Name = "电话号码")]
        [Column]
        public string PHONE_NO { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        [Column]
        public Nullable<DateTime> ADD_TIME { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        [Display(Name = "发送时间")]
        [Column]
        public Nullable<DateTime> SEND_TIME { get; set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        [Required]
        [StringLength(500)]
        [Display(Name = "发送内容")]
        [Column]
        public string CONTENT { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [StringLength(15)]
        [Display(Name = "状态")]
        [Column]
        public string STAUTS { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        [Required]
        [Display(Name = "重试次数")]
        [Column]
        public int TRY_NUM { get; set; }


    }
}
