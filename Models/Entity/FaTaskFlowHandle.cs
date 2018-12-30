using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaTaskFlowHandle
    {
        public FaTaskFlowHandle()
        {
            FaTaskFlowHandleFiles = new HashSet<FaTaskFlowHandleFiles>();
        }

        public int Id { get; set; }
        public int TaskFlowId { get; set; }
        public int DealUserId { get; set; }
        public string DealUserName { get; set; }
        public DateTime DealTime { get; set; }
        public string Content { get; set; }

        public virtual FaTaskFlow TaskFlow { get; set; }
        public virtual ICollection<FaTaskFlowHandleFiles> FaTaskFlowHandleFiles { get; set; }
    }
}
