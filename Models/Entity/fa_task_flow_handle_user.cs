using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_task_flow_handle_user
    {
        public int TASK_FLOW_ID { get; set; }
        public int HANDLE_USER_ID { get; set; }

        public virtual fa_task_flow TASK_FLOW_ { get; set; }
    }
}
