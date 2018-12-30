using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaMessageType
    {
        public FaMessageType()
        {
            FaMessage = new HashSet<FaMessage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public int? IsUse { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<FaMessage> FaMessage { get; set; }
    }
}
