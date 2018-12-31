using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_db_server_type
    {
        public fa_db_server_type()
        {
            fa_db_server = new HashSet<fa_db_server>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string REMARK { get; set; }

        public virtual ICollection<fa_db_server> fa_db_server { get; set; }
    }
}
