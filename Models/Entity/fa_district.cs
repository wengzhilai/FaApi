using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_district
    {
        public fa_district()
        {
            InversePARENT_ = new HashSet<fa_district>();
            fa_user = new HashSet<fa_user>();
            fa_user_district = new HashSet<fa_user_district>();
        }

        public int ID { get; set; }
        public int? PARENT_ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public decimal IN_USE { get; set; }
        public int LEVEL_ID { get; set; }
        public string ID_PATH { get; set; }
        public string REGION { get; set; }

        public virtual fa_district PARENT_ { get; set; }
        public virtual ICollection<fa_district> InversePARENT_ { get; set; }
        public virtual ICollection<fa_user> fa_user { get; set; }
        public virtual ICollection<fa_user_district> fa_user_district { get; set; }
    }
}
