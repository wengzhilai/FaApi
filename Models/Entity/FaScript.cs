using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaScript
    {
        public FaScript()
        {
            FaScriptGroupList = new HashSet<FaScriptGroupList>();
            FaScriptTask = new HashSet<FaScriptTask>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string BodyText { get; set; }
        public string BodyHash { get; set; }
        public string RunWhen { get; set; }
        public string RunArgs { get; set; }
        public string RunData { get; set; }
        public string Status { get; set; }
        public string DisableReason { get; set; }
        public string ServiceFlag { get; set; }
        public string Region { get; set; }
        public decimal IsGroup { get; set; }

        public virtual ICollection<FaScriptGroupList> FaScriptGroupList { get; set; }
        public virtual ICollection<FaScriptTask> FaScriptTask { get; set; }
    }
}
