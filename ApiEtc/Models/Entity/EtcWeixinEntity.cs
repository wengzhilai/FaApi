
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEtc.Models.Entity
{
    /// <summary>
    /// 微信用户
    /// </summary>
    [Table("etc_weixin")]
    public class EtcWeixinEntity
    {


        /// <summary>
        /// OpenId
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Display(Name = "OpenId")]
        [Column("OpenId")]
        public String openid { get; set; }

        /// <summary>
        /// ParentTicket
        /// </summary>
        [Required]
        [Display(Name = "ParentTicket")]
        [Column("ParentTicket")]
        public String parentTicket { get; set; }

        /// <summary>
        /// Ticket
        /// </summary>
        [Required]
        [Display(Name = "Ticket")]
        [Column("Ticket")]
        public String ticket { get; set; }


        /// <summary>
        /// EventKey
        /// </summary>
        [Required]
        [Display(Name = "EventKey")]
        [Column("EventKey")]
        public String eventKey { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [Column("CreateTime")]
        public DateTime createTime { get; set; }


    }
}
