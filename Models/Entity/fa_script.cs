using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_script
    {
        public fa_script()
        {
            fa_script_group_list = new HashSet<fa_script_group_list>();
            fa_script_task = new HashSet<fa_script_task>();
        }

        public int ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string BODY_TEXT { get; set; }
        public string BODY_HASH { get; set; }
        public string RUN_WHEN { get; set; }
        public string RUN_ARGS { get; set; }
        public string RUN_DATA { get; set; }
        public string STATUS { get; set; }
        public string DISABLE_REASON { get; set; }
        public string SERVICE_FLAG { get; set; }
        public string REGION { get; set; }
        public decimal IS_GROUP { get; set; }

        public virtual ICollection<fa_script_group_list> fa_script_group_list { get; set; }
        public virtual ICollection<fa_script_task> fa_script_task { get; set; }
    }
}
