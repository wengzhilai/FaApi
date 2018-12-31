using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_event
    {
        public fa_user_event()
        {
            fa_event_files = new HashSet<fa_event_files>();
        }

        public int ID { get; set; }
        public int? USER_ID { get; set; }
        public string NAME { get; set; }
        public DateTime? HAPPEN_TIME { get; set; }
        public string CONTENT { get; set; }
        public string ADDRESS { get; set; }

        public virtual fa_user_info USER_ { get; set; }
        public virtual ICollection<fa_event_files> fa_event_files { get; set; }
    }
}
