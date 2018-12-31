using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_script_task
    {
        public fa_script_task()
        {
            fa_script_task_log = new HashSet<fa_script_task_log>();
        }

        public int ID { get; set; }
        public int SCRIPT_ID { get; set; }
        public string BODY_TEXT { get; set; }
        public string BODY_HASH { get; set; }
        public string RUN_STATE { get; set; }
        public string RUN_WHEN { get; set; }
        public string RUN_ARGS { get; set; }
        public string RUN_DATA { get; set; }
        public decimal? LOG_TYPE { get; set; }
        public string DSL_TYPE { get; set; }
        public string RETURN_CODE { get; set; }
        public DateTime? START_TIME { get; set; }
        public DateTime? END_TIME { get; set; }
        public DateTime? DISABLE_DATE { get; set; }
        public string DISABLE_REASON { get; set; }
        public string SERVICE_FLAG { get; set; }
        public string REGION { get; set; }
        public int? GROUP_ID { get; set; }

        public virtual fa_script SCRIPT_ { get; set; }
        public virtual ICollection<fa_script_task_log> fa_script_task_log { get; set; }
    }
}
