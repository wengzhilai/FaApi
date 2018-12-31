using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_role_module
    {
        public int ROLE_ID { get; set; }
        public int MODULE_ID { get; set; }

        public virtual fa_module MODULE_ { get; set; }
        public virtual fa_role ROLE_ { get; set; }
    }
}
