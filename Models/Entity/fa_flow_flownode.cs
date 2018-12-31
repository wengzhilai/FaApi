using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_flow_flownode
    {
        public fa_flow_flownode()
        {
            fa_flow_flownode_flow = new HashSet<fa_flow_flownode_flow>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string HANDLE_URL { get; set; }
        public string SHOW_URL { get; set; }

        public virtual ICollection<fa_flow_flownode_flow> fa_flow_flownode_flow { get; set; }
    }
}
