using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaConfig
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
        public string Region { get; set; }
        public int? AddUserId { get; set; }
        public DateTime? AddTiem { get; set; }
    }
}
