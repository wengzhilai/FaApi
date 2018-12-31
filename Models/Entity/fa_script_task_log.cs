using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_script_task_log
    {
        public int ID { get; set; }
        public int SCRIPT_TASK_ID { get; set; }
        public DateTime LOG_TIME { get; set; }
        public decimal LOG_TYPE { get; set; }
        public string MESSAGE { get; set; }
        public string SQL_TEXT { get; set; }

        public virtual fa_script_task SCRIPT_TASK_ { get; set; }
    }
}
