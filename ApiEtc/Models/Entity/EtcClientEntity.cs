using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiEtc.Models.Entity
{
    /// <summary>
    /// 客户
    /// </summary>
    [Table("etc_client")]
    public class EtcClientEntity
    {

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        [Column("Id")]
        public int id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        [Display(Name = "员工ID")]
        [Column("StaffId")]
        public int staffId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        [Display(Name = "客户姓名")]
        [Column("ClientName")]
        public String clientName { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        [Display(Name = "客户电话")]
        [Column("ClientPhone")]
        public String clientPhone { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        [Display(Name = "绑定时间")]
        [Column("BindTime")]
        public DateTime bindTime { get; set; }

        /// <summary>
        /// 状态:状态:已绑定/已提交资料/已安装激活/已结算
        /// </summary>
        [Display(Name = "状态:状态:已绑定/已提交资料/已安装激活/已结算")]
        [Column("Status")]
        public String status { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        [Display(Name = "结算金额")]
        [Column("Money")]
        public Decimal money { get; set; }

        /// <summary>
        /// 操作人员姓名
        /// </summary>
        [Display(Name = "操作人员姓名")]
        [Column("OpUserName")]
        public String opuserName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [Column("ReMark")]
        public String remark { get; set; }

        /// <summary>
        /// 结算Id
        /// </summary>
        [Display(Name = "结算Id")]
        [Column("WalletId")]
        public int walletId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [Required]
        [Display(Name = "车牌号")]
        [Column("CarNum")]
        public String carNum { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        [Required]
        [Display(Name = "车型")]
        [Column("CarType")]
        public String carType { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        [Required]
        [Display(Name = "提交时间")]
        [Column("SubmitTime")]
        public DateTime submitTime { get; set; }

    }
}
