using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaRoleModule
    {
        public int RoleId { get; set; }
        public int ModuleId { get; set; }

        public virtual FaModule Module { get; set; }
        public virtual FaRole Role { get; set; }
    }
}
