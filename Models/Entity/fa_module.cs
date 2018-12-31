using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_module
    {
        public fa_module()
        {
            InversePARENT_ = new HashSet<fa_module>();
            fa_role_module = new HashSet<fa_role_module>();
        }

        public int ID { get; set; }
        public int? PARENT_ID { get; set; }
        public string NAME { get; set; }
        public string LOCATION { get; set; }
        public string CODE { get; set; }
        public decimal IS_DEBUG { get; set; }
        public decimal IS_HIDE { get; set; }
        public decimal SHOW_ORDER { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGE_URL { get; set; }
        public string DESKTOP_ROLE { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }

        public virtual fa_module PARENT_ { get; set; }
        public virtual ICollection<fa_module> InversePARENT_ { get; set; }
        public virtual ICollection<fa_role_module> fa_role_module { get; set; }
    }
}
