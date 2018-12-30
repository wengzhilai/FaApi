using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserEvent
    {
        public FaUserEvent()
        {
            FaEventFiles = new HashSet<FaEventFiles>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public DateTime? HappenTime { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }

        public virtual FaUserInfo User { get; set; }
        public virtual ICollection<FaEventFiles> FaEventFiles { get; set; }
    }
}
