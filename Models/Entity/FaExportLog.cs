using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaExportLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }
        public string SqlContent { get; set; }
        public DateTime? ExportTime { get; set; }
        public string Remark { get; set; }
    }
}
