using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin_role
    {
        public int BULLETIN_ID { get; set; }
        public int ROLE_ID { get; set; }

        public virtual fa_bulletin BULLETIN_ { get; set; }
        public virtual fa_role ROLE_ { get; set; }
    }
}
