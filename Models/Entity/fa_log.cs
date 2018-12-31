using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_log
    {
        public int ID { get; set; }
        public DateTime ADD_TIME { get; set; }
        public string MODULE_NAME { get; set; }
        public int USER_ID { get; set; }
    }
}
