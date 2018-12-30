using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaBulletinFile
    {
        public int BulletinId { get; set; }
        public int FileId { get; set; }

        public virtual FaBulletin Bulletin { get; set; }
        public virtual FaFiles File { get; set; }
    }
}
