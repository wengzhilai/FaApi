using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_event_files
    {
        public int EVENT_ID { get; set; }
        public int FILES_ID { get; set; }

        public virtual fa_user_event EVENT_ { get; set; }
        public virtual fa_files FILES_ { get; set; }
    }
}
