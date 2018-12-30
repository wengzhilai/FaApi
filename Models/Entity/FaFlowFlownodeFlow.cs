using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFlowFlownodeFlow
    {
        public FaFlowFlownodeFlow()
        {
            FaFlowFlownodeRole = new HashSet<FaFlowFlownodeRole>();
        }

        public int Id { get; set; }
        public int FlowId { get; set; }
        public int FromFlownodeId { get; set; }
        public int ToFlownodeId { get; set; }
        public decimal Handle { get; set; }
        public decimal Assigner { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public int ExpireHour { get; set; }

        public virtual FaFlow Flow { get; set; }
        public virtual FaFlowFlownode FromFlownode { get; set; }
        public virtual ICollection<FaFlowFlownodeRole> FaFlowFlownodeRole { get; set; }
    }
}
