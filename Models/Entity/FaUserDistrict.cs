using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserDistrict
    {
        public int UserId { get; set; }
        public int DistrictId { get; set; }

        public virtual FaDistrict District { get; set; }
        public virtual FaUser User { get; set; }
    }
}
