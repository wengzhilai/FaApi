using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin_review
    {
        public fa_bulletin_review()
        {
            InversePARENT_ = new HashSet<fa_bulletin_review>();
        }

        public int ID { get; set; }
        public int? PARENT_ID { get; set; }
        public int BULLETIN_ID { get; set; }
        public string NAME { get; set; }
        public string CONTENT { get; set; }
        public int USER_ID { get; set; }
        public DateTime ADD_TIME { get; set; }
        public string STATUS { get; set; }
        public DateTime STATUS_TIME { get; set; }

        public virtual fa_bulletin BULLETIN_ { get; set; }
        public virtual fa_bulletin_review PARENT_ { get; set; }
        public virtual ICollection<fa_bulletin_review> InversePARENT_ { get; set; }
    }
}
