using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFlow
    {
        public FaFlow()
        {
            FaFlowFlownodeFlow = new HashSet<FaFlowFlownodeFlow>();
            FaTask = new HashSet<FaTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FlowType { get; set; }
        public string Remark { get; set; }
        public string XY { get; set; }
        public string Region { get; set; }

        public virtual ICollection<FaFlowFlownodeFlow> FaFlowFlownodeFlow { get; set; }
        public virtual ICollection<FaTask> FaTask { get; set; }
    }
}
