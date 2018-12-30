using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaRoleQueryAuthority
    {
        public int RoleId { get; set; }
        public int QueryId { get; set; }
        public string NoAuthority { get; set; }

        public virtual FaQuery Query { get; set; }
        public virtual FaRole Role { get; set; }
    }
}
