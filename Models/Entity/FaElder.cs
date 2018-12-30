using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaElder
    {
        public int Id { get; set; }
        public int? FamilyId { get; set; }
        public string Name { get; set; }
        public int? Sort { get; set; }

        public virtual FaFamily Family { get; set; }
    }
}
