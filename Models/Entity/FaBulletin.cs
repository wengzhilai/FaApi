using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaBulletin:MongodbEntity
    {
        public FaBulletin()
        {
            FaBulletinFile = new HashSet<FaBulletinFile>();
            FaBulletinLog = new HashSet<FaBulletinLog>();
            FaBulletinReview = new HashSet<FaBulletinReview>();
            FaBulletinRole = new HashSet<FaBulletinRole>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public string TypeCode { get; set; }
        public string Content { get; set; }
        public int? UserId { get; set; }
        public string Publisher { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal IsShow { get; set; }
        public decimal IsImport { get; set; }
        public decimal IsUrgent { get; set; }
        public decimal AutoPen { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Region { get; set; }

        public virtual ICollection<FaBulletinFile> FaBulletinFile { get; set; }
        public virtual ICollection<FaBulletinLog> FaBulletinLog { get; set; }
        public virtual ICollection<FaBulletinReview> FaBulletinReview { get; set; }
        public virtual ICollection<FaBulletinRole> FaBulletinRole { get; set; }
    }
}
