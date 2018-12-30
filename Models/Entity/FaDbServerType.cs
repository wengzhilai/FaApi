using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaDbServerType
    {
        public FaDbServerType()
        {
            FaDbServer = new HashSet<FaDbServer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<FaDbServer> FaDbServer { get; set; }
    }
}
