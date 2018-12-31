using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_updata_log
    {
        public int ID { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public string CREATE_USER_NAME { get; set; }
        public int? CREATE_USER_ID { get; set; }
        public string OLD_CONTENT { get; set; }
        public string NEW_CONTENT { get; set; }
        public string TABLE_NAME { get; set; }
    }
}
