using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserInfo
    {
        public FaUserInfo()
        {
            FaUserEvent = new HashSet<FaUserEvent>();
            FaUserFriend = new HashSet<FaUserFriend>();
        }

        public int Id { get; set; }
        public int? LevelId { get; set; }
        public int? FamilyId { get; set; }
        public int? ElderId { get; set; }
        public string LevelName { get; set; }
        public int? FatherId { get; set; }
        public int? MotherId { get; set; }
        public DateTime? BirthdayTime { get; set; }
        public string BirthdayPlace { get; set; }
        public decimal? IsLive { get; set; }
        public DateTime? DiedTime { get; set; }
        public string DiedPlace { get; set; }
        public string Remark { get; set; }
        public string Sex { get; set; }
        public string YearsType { get; set; }
        public int? ConsortId { get; set; }
        public string Status { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int CreateUserId { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUserName { get; set; }
        public int UpdateUserId { get; set; }
        public int? CoupleId { get; set; }
        public string Alias { get; set; }
        public int? Authority { get; set; }

        public virtual ICollection<FaUserEvent> FaUserEvent { get; set; }
        public virtual ICollection<FaUserFriend> FaUserFriend { get; set; }
    }
}
