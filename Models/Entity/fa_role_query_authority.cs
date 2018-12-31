using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_role_query_authority
    {
        public int ROLE_ID { get; set; }
        public int QUERY_ID { get; set; }
        public string NO_AUTHORITY { get; set; }

        public virtual fa_query QUERY_ { get; set; }
        public virtual fa_role ROLE_ { get; set; }
    }
}
