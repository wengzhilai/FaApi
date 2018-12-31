using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_task_flow
    {
        public fa_task_flow()
        {
            InversePARENT_ = new HashSet<fa_task_flow>();
            fa_task_flow_handle = new HashSet<fa_task_flow_handle>();
            fa_task_flow_handle_user = new HashSet<fa_task_flow_handle_user>();
        }

        public int ID { get; set; }
        public int? PARENT_ID { get; set; }
        public int TASK_ID { get; set; }
        public int? LEVEL_ID { get; set; }
        public int? FLOWNODE_ID { get; set; }
        public int? EQUAL_ID { get; set; }
        public int IS_HANDLE { get; set; }
        public string NAME { get; set; }
        public string HANDLE_URL { get; set; }
        public string SHOW_URL { get; set; }
        public DateTime? EXPIRE_TIME { get; set; }
        public DateTime START_TIME { get; set; }
        public string DEAL_STATUS { get; set; }
        public string ROLE_ID_STR { get; set; }
        public int? HANDLE_USER_ID { get; set; }
        public DateTime? DEAL_TIME { get; set; }
        public DateTime? ACCEPT_TIME { get; set; }

        public virtual fa_task_flow PARENT_ { get; set; }
        public virtual fa_task TASK_ { get; set; }
        public virtual ICollection<fa_task_flow> InversePARENT_ { get; set; }
        public virtual ICollection<fa_task_flow_handle> fa_task_flow_handle { get; set; }
        public virtual ICollection<fa_task_flow_handle_user> fa_task_flow_handle_user { get; set; }
    }
}
