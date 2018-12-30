using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFlowFlownode
    {
        public FaFlowFlownode()
        {
            FaFlowFlownodeFlow = new HashSet<FaFlowFlownodeFlow>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string HandleUrl { get; set; }
        public string ShowUrl { get; set; }

        public virtual ICollection<FaFlowFlownodeFlow> FaFlowFlownodeFlow { get; set; }
    }
}
