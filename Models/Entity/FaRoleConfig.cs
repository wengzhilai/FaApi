using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaRoleConfig
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }

        public virtual FaRole Role { get; set; }
    }
}
