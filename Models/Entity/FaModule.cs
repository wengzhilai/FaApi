using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaModule
    {
        public FaModule()
        {
            FaRoleModule = new HashSet<FaRoleModule>();
            InverseParent = new HashSet<FaModule>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Code { get; set; }
        public decimal IsDebug { get; set; }
        public decimal IsHide { get; set; }
        public decimal ShowOrder { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string DesktopRole { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }

        public virtual FaModule Parent { get; set; }
        public virtual ICollection<FaRoleModule> FaRoleModule { get; set; }
        public virtual ICollection<FaModule> InverseParent { get; set; }
    }
}
