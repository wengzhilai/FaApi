using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_flow
    {
        public fa_flow()
        {
            fa_flow_flownode_flow = new HashSet<fa_flow_flownode_flow>();
            fa_task = new HashSet<fa_task>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string FLOW_TYPE { get; set; }
        public string REMARK { get; set; }
        public string X_Y { get; set; }
        public string REGION { get; set; }

        public virtual ICollection<fa_flow_flownode_flow> fa_flow_flownode_flow { get; set; }
        public virtual ICollection<fa_task> fa_task { get; set; }
    }
}
