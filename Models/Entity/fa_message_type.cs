using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_message_type
    {
        public fa_message_type()
        {
            fa_message = new HashSet<fa_message>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string TABLE_NAME { get; set; }
        public int? IS_USE { get; set; }
        public string REMARK { get; set; }

        public virtual ICollection<fa_message> fa_message { get; set; }
    }
}
