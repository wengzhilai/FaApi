using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaBulletinRole
    {
        public int BulletinId { get; set; }
        public int RoleId { get; set; }

        public virtual FaBulletin Bulletin { get; set; }
        public virtual FaRole Role { get; set; }
    }
}
