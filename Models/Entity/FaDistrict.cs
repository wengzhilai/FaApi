using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaDistrict
    {
        public FaDistrict()
        {
            FaUser = new HashSet<FaUser>();
            FaUserDistrict = new HashSet<FaUserDistrict>();
            InverseParent = new HashSet<FaDistrict>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal InUse { get; set; }
        public int LevelId { get; set; }
        public string IdPath { get; set; }
        public string Region { get; set; }

        public virtual FaDistrict Parent { get; set; }
        public virtual ICollection<FaUser> FaUser { get; set; }
        public virtual ICollection<FaUserDistrict> FaUserDistrict { get; set; }
        public virtual ICollection<FaDistrict> InverseParent { get; set; }
    }
}
