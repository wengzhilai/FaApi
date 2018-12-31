using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_task_flow_handle_files
    {
        public int FLOW_HANDLE_ID { get; set; }
        public int FILES_ID { get; set; }

        public virtual fa_files FILES_ { get; set; }
        public virtual fa_task_flow_handle FLOW_HANDLE_ { get; set; }
    }
}
