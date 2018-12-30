using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaBulletinLog
    {
        public int Id { get; set; }
        public int BulletinId { get; set; }
        public int UserId { get; set; }
        public DateTime LookTime { get; set; }

        public virtual FaBulletin Bulletin { get; set; }
    }
}
