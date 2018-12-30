using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaTask
    {
        public FaTask()
        {
            FaTaskFlow = new HashSet<FaTaskFlow>();
        }

        public int Id { get; set; }
        public int? FlowId { get; set; }
        public string TaskName { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? CreateUser { get; set; }
        public string CreateUserName { get; set; }
        public string Status { get; set; }
        public DateTime? StatusTime { get; set; }
        public string Remark { get; set; }
        public string Region { get; set; }
        public string KeyId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? DealTime { get; set; }
        public string RoleIdStr { get; set; }

        public virtual FaFlow Flow { get; set; }
        public virtual ICollection<FaTaskFlow> FaTaskFlow { get; set; }
    }
}
