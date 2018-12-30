using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUser
    {
        public FaUser()
        {
            FaUserDistrict = new HashSet<FaUserDistrict>();
            FaUserFile = new HashSet<FaUserFile>();
            FaUserFriend = new HashSet<FaUserFriend>();
            FaUserRole = new HashSet<FaUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LoginName { get; set; }
        public int? IconFilesId { get; set; }
        public int DistrictId { get; set; }
        public decimal? IsLocked { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? LoginCount { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public DateTime? LastActiveTime { get; set; }
        public string Remark { get; set; }

        public virtual FaDistrict District { get; set; }
        public virtual ICollection<FaUserDistrict> FaUserDistrict { get; set; }
        public virtual ICollection<FaUserFile> FaUserFile { get; set; }
        public virtual ICollection<FaUserFriend> FaUserFriend { get; set; }
        public virtual ICollection<FaUserRole> FaUserRole { get; set; }
    }
}
