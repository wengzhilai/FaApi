using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaTaskFlowHandleFiles
    {
        public int FlowHandleId { get; set; }
        public int FilesId { get; set; }

        public virtual FaFiles Files { get; set; }
        public virtual FaTaskFlowHandle FlowHandle { get; set; }
    }
}
