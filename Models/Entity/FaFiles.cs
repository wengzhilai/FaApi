using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFiles
    {
        public FaFiles()
        {
            FaBulletinFile = new HashSet<FaBulletinFile>();
            FaEventFiles = new HashSet<FaEventFiles>();
            FaTaskFlowHandleFiles = new HashSet<FaTaskFlowHandleFiles>();
            FaUserFile = new HashSet<FaUserFile>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
        public int Length { get; set; }
        public DateTime? UploadTime { get; set; }
        public string Remark { get; set; }
        public string Url { get; set; }
        public string FileType { get; set; }

        public virtual ICollection<FaBulletinFile> FaBulletinFile { get; set; }
        public virtual ICollection<FaEventFiles> FaEventFiles { get; set; }
        public virtual ICollection<FaTaskFlowHandleFiles> FaTaskFlowHandleFiles { get; set; }
        public virtual ICollection<FaUserFile> FaUserFile { get; set; }
    }
}
