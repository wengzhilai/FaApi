using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_script_group_list
    {
        public int SCRIPT_ID { get; set; }
        public int GROUP_ID { get; set; }
        public int ORDER_INDEX { get; set; }

        public virtual fa_script GROUP_ { get; set; }
    }
}
