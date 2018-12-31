using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_elder
    {
        public int ID { get; set; }
        public int? FAMILY_ID { get; set; }
        public string NAME { get; set; }
        public int? SORT { get; set; }

        public virtual fa_family FAMILY_ { get; set; }
    }
}
