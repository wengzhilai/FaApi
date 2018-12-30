using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUpdataLog
    {
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUserName { get; set; }
        public int? CreateUserId { get; set; }
        public string OldContent { get; set; }
        public string NewContent { get; set; }
        public string TableName { get; set; }
    }
}
