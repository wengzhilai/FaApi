using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_role_function
    {
        public int FUNCTION_ID { get; set; }
        public int ROLE_ID { get; set; }

        public virtual fa_function FUNCTION_ { get; set; }
        public virtual fa_role ROLE_ { get; set; }
    }
}
