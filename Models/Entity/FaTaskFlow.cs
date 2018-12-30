using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaTaskFlow
    {
        public FaTaskFlow()
        {
            FaTaskFlowHandle = new HashSet<FaTaskFlowHandle>();
            FaTaskFlowHandleUser = new HashSet<FaTaskFlowHandleUser>();
            InverseParent = new HashSet<FaTaskFlow>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int TaskId { get; set; }
        public int? LevelId { get; set; }
        public int? FlownodeId { get; set; }
        public int? EqualId { get; set; }
        public int IsHandle { get; set; }
        public string Name { get; set; }
        public string HandleUrl { get; set; }
        public string ShowUrl { get; set; }
        public DateTime? ExpireTime { get; set; }
        public DateTime StartTime { get; set; }
        public string DealStatus { get; set; }
        public string RoleIdStr { get; set; }
        public int? HandleUserId { get; set; }
        public DateTime? DealTime { get; set; }
        public DateTime? AcceptTime { get; set; }

        public virtual FaTaskFlow Parent { get; set; }
        public virtual FaTask Task { get; set; }
        public virtual ICollection<FaTaskFlowHandle> FaTaskFlowHandle { get; set; }
        public virtual ICollection<FaTaskFlowHandleUser> FaTaskFlowHandleUser { get; set; }
        public virtual ICollection<FaTaskFlow> InverseParent { get; set; }
    }
}
