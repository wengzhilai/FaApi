using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entity;

namespace Models
{
    /// <summary>
    /// 
    /// </summary>
    public class FaUserInfo:FaUserInfoEntity
    {
        /// <summary>
        /// 家族
        /// </summary>
        public FaFamilyEntity Family { get; set; }
        /// <summary>
        /// 辈分
        /// </summary>
        public FaElderEntity Elder { get; set; }

        /// <summary>
        /// 所有朋友
        /// </summary>
        public IList<FaUserEntity> FriendList{ get; set; }

        /// <summary>
        /// 所有事件
        /// </summary>
        public IList<FaUserEventEntity> EventList { get; set; }

        /// <summary>
        /// 父亲
        /// </summary>
        public FaUserInfoEntity Father { get; set; }
        /// <summary>
        /// 父亲姓名
        /// </summary>
        public string FatherName { get; set; }
        /// <summary>
        /// 母亲
        /// </summary>
        public FaUserInfoEntity Mather { get; set; }
        /// <summary>
        /// 母亲姓名
        /// </summary>
        public string MatherName { get; set; }
        /// <summary>
        ///  配偶
        /// </summary>
        public FaUserInfoEntity Consort { get; set; }

        /// <summary>
        /// 出生日期中国历
        /// </summary>
        public Nullable<DateTime> BirthdayTimeChinese { get; set; }

        /// <summary>
        /// 过逝日期中国历
        /// </summary>
        public Nullable<DateTime> DiedTimeChinese { get; set; }
    }
}
