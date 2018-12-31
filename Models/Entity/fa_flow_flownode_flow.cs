using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_flow_flownode_flow
    {
        public fa_flow_flownode_flow()
        {
            fa_flow_flownode_role = new HashSet<fa_flow_flownode_role>();
        }

        public int ID { get; set; }
        public int FLOW_ID { get; set; }
        public int FROM_FLOWNODE_ID { get; set; }
        public int TO_FLOWNODE_ID { get; set; }
        public decimal HANDLE { get; set; }
        public decimal ASSIGNER { get; set; }
        public string STATUS { get; set; }
        public string REMARK { get; set; }
        public int EXPIRE_HOUR { get; set; }

        public virtual fa_flow FLOW_ { get; set; }
        public virtual fa_flow_flownode FROM_FLOWNODE_ { get; set; }
        public virtual ICollection<fa_flow_flownode_role> fa_flow_flownode_role { get; set; }
    }
}
