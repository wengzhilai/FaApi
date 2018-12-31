using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_function
    {
        public fa_function()
        {
            fa_role_function = new HashSet<fa_role_function>();
        }

        public int ID { get; set; }
        public string REMARK { get; set; }
        public string FULL_NAME { get; set; }
        public string NAMESPACE { get; set; }
        public string CLASS_NAME { get; set; }
        public string METHOD_NAME { get; set; }
        public string DLL_NAME { get; set; }
        public string XML_NOTE { get; set; }

        public virtual ICollection<fa_role_function> fa_role_function { get; set; }
    }
}
