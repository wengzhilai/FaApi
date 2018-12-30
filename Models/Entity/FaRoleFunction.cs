using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaRoleFunction
    {
        public int FunctionId { get; set; }
        public int RoleId { get; set; }

        public virtual FaFunction Function { get; set; }
        public virtual FaRole Role { get; set; }
    }
}
