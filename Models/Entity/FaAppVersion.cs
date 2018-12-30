using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaAppVersion
    {
        public int Id { get; set; }
        public decimal IsNew { get; set; }
        public string Type { get; set; }
        public string Remark { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateUrl { get; set; }
    }
}
