using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserFile
    {
        public int UserId { get; set; }
        public int FileId { get; set; }

        public virtual FaFiles File { get; set; }
        public virtual FaUser User { get; set; }
    }
}
