using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaLog
    {
        public int Id { get; set; }
        public DateTime AddTime { get; set; }
        public string ModuleName { get; set; }
        public int UserId { get; set; }
    }
}
