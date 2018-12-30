using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaFunction
    {
        public FaFunction()
        {
            FaRoleFunction = new HashSet<FaRoleFunction>();
        }

        public int Id { get; set; }
        public string Remark { get; set; }
        public string FullName { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public string DllName { get; set; }
        public string XmlNote { get; set; }

        public virtual ICollection<FaRoleFunction> FaRoleFunction { get; set; }
    }
}
