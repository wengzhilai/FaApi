using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaTaskFlowHandleUser
    {
        public int TaskFlowId { get; set; }
        public int HandleUserId { get; set; }

        public virtual FaTaskFlow TaskFlow { get; set; }
    }
}
