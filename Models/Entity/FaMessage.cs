using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaMessage
    {
        public int Id { get; set; }
        public int? MessageTypeId { get; set; }
        public int? KeyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateUsername { get; set; }
        public int? CreateUserid { get; set; }
        public string Status { get; set; }
        public string PushType { get; set; }
        public int? DistrictId { get; set; }
        public string AllRoleId { get; set; }

        public virtual FaMessageType MessageType { get; set; }
    }
}
