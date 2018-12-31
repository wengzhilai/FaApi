using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_file
    {
        public int USER_ID { get; set; }
        public int FILE_ID { get; set; }

        public virtual fa_files FILE_ { get; set; }
        public virtual fa_user USER_ { get; set; }
    }
}
