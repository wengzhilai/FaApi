using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_role
    {
        public int ROLE_ID { get; set; }
        public int USER_ID { get; set; }

        public virtual fa_role ROLE_ { get; set; }
        public virtual fa_user USER_ { get; set; }
    }
}
