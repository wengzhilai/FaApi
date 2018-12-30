using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaEventFiles
    {
        public int EventId { get; set; }
        public int FilesId { get; set; }

        public virtual FaUserEvent Event { get; set; }
        public virtual FaFiles Files { get; set; }
    }
}
