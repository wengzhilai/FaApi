using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaScriptTaskLog
    {
        public int Id { get; set; }
        public int ScriptTaskId { get; set; }
        public DateTime LogTime { get; set; }
        public decimal LogType { get; set; }
        public string Message { get; set; }
        public string SqlText { get; set; }

        public virtual FaScriptTask ScriptTask { get; set; }
    }
}
