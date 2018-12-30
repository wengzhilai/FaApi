using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFamily
    {
        public FaFamily()
        {
            FaElder = new HashSet<FaElder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FaElder> FaElder { get; set; }
    }
}
