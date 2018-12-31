using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_district
    {
        public int USER_ID { get; set; }
        public int DISTRICT_ID { get; set; }

        public virtual fa_district DISTRICT_ { get; set; }
        public virtual fa_user USER_ { get; set; }
    }
}
