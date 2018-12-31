using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_config
    {
        public int ID { get; set; }
        public string TYPE { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string VALUE { get; set; }
        public string REMARK { get; set; }
        public string REGION { get; set; }
        public int? ADD_USER_ID { get; set; }
        public DateTime? ADD_TIEM { get; set; }
    }
}
