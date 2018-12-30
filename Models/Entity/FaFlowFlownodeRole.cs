using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFlowFlownodeRole
    {
        public int FlowId { get; set; }
        public int RoleId { get; set; }

        public virtual FaFlowFlownodeFlow Flow { get; set; }
        public virtual FaRole Role { get; set; }
    }
}
