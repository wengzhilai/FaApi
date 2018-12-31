using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_oauth_login
    {
        public int OAUTH_ID { get; set; }
        public int LOGIN_ID { get; set; }

        public virtual fa_login LOGIN_ { get; set; }
        public virtual fa_oauth OAUTH_ { get; set; }
    }
}
