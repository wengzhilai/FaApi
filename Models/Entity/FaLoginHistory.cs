using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaLoginHistory
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string LoginHost { get; set; }
        public DateTime? LogoutTime { get; set; }
        public int? LoginHistoryType { get; set; }
        public string Message { get; set; }
    }
}
