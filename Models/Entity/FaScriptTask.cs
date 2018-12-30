using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaScriptTask
    {
        public FaScriptTask()
        {
            FaScriptTaskLog = new HashSet<FaScriptTaskLog>();
        }

        public int Id { get; set; }
        public int ScriptId { get; set; }
        public string BodyText { get; set; }
        public string BodyHash { get; set; }
        public string RunState { get; set; }
        public string RunWhen { get; set; }
        public string RunArgs { get; set; }
        public string RunData { get; set; }
        public decimal? LogType { get; set; }
        public string DslType { get; set; }
        public string ReturnCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DisableDate { get; set; }
        public string DisableReason { get; set; }
        public string ServiceFlag { get; set; }
        public string Region { get; set; }
        public int? GroupId { get; set; }

        public virtual FaScript Script { get; set; }
        public virtual ICollection<FaScriptTaskLog> FaScriptTaskLog { get; set; }
    }
}
