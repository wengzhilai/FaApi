using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_task_flow_handle
    {
        public fa_task_flow_handle()
        {
            fa_task_flow_handle_files = new HashSet<fa_task_flow_handle_files>();
        }

        public int ID { get; set; }
        public int TASK_FLOW_ID { get; set; }
        public int DEAL_USER_ID { get; set; }
        public string DEAL_USER_NAME { get; set; }
        public DateTime DEAL_TIME { get; set; }
        public string CONTENT { get; set; }

        public virtual fa_task_flow TASK_FLOW_ { get; set; }
        public virtual ICollection<fa_task_flow_handle_files> fa_task_flow_handle_files { get; set; }
    }
}
