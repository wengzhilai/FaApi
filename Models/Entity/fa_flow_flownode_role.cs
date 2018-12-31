using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_flow_flownode_role
    {
        public int FLOW_ID { get; set; }
        public int ROLE_ID { get; set; }

        public virtual fa_flow_flownode_flow FLOW_ { get; set; }
        public virtual fa_role ROLE_ { get; set; }
    }
}
