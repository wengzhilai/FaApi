using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_role_config
    {
        public int ID { get; set; }
        public int ROLE_ID { get; set; }
        public string TYPE { get; set; }
        public string NAME { get; set; }
        public string VALUE { get; set; }
        public string REMARK { get; set; }

        public virtual fa_role ROLE_ { get; set; }
    }
}
