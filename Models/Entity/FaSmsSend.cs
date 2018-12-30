using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaSmsSend
    {
        public string Guid { get; set; }
        public int? MessageId { get; set; }
        public string PhoneNo { get; set; }
        public DateTime? AddTime { get; set; }
        public DateTime? SendTime { get; set; }
        public string Content { get; set; }
        public string Stauts { get; set; }
        public int TryNum { get; set; }
    }
}
