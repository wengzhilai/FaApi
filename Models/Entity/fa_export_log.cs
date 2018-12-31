using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_export_log
    {
        public int ID { get; set; }
        public int? USER_ID { get; set; }
        public string LOGIN_NAME { get; set; }
        public string NAME { get; set; }
        public string SQL_CONTENT { get; set; }
        public DateTime? EXPORT_TIME { get; set; }
        public string REMARK { get; set; }
    }
}
