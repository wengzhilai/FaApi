using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaScriptGroupList
    {
        public int ScriptId { get; set; }
        public int GroupId { get; set; }
        public int OrderIndex { get; set; }

        public virtual FaScript Group { get; set; }
    }
}
