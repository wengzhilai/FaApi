using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }

        public virtual FaRole Role { get; set; }
        public virtual FaUser User { get; set; }
    }
}
