using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_task
    {
        public fa_task()
        {
            fa_task_flow = new HashSet<fa_task_flow>();
        }

        public int ID { get; set; }
        public int? FLOW_ID { get; set; }
        public string TASK_NAME { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public int? CREATE_USER { get; set; }
        public string CREATE_USER_NAME { get; set; }
        public string STATUS { get; set; }
        public DateTime? STATUS_TIME { get; set; }
        public string REMARK { get; set; }
        public string REGION { get; set; }
        public string KEY_ID { get; set; }
        public DateTime? START_TIME { get; set; }
        public DateTime? END_TIME { get; set; }
        public DateTime? DEAL_TIME { get; set; }
        public string ROLE_ID_STR { get; set; }

        public virtual fa_flow FLOW_ { get; set; }
        public virtual ICollection<fa_task_flow> fa_task_flow { get; set; }
    }
}
