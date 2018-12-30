using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaBulletinReview
    {
        public FaBulletinReview()
        {
            InverseParent = new HashSet<FaBulletinReview>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int BulletinId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public DateTime AddTime { get; set; }
        public string Status { get; set; }
        public DateTime StatusTime { get; set; }

        public virtual FaBulletin Bulletin { get; set; }
        public virtual FaBulletinReview Parent { get; set; }
        public virtual ICollection<FaBulletinReview> InverseParent { get; set; }
    }
}
