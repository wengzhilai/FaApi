using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin_file
    {
        public int BULLETIN_ID { get; set; }
        public int FILE_ID { get; set; }

        public virtual fa_bulletin BULLETIN_ { get; set; }
        public virtual fa_files FILE_ { get; set; }
    }
}
