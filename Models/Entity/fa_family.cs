using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_family
    {
        public fa_family()
        {
            fa_elder = new HashSet<fa_elder>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<fa_elder> fa_elder { get; set; }
    }
}
