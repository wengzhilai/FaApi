using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaRole
    {
        public FaRole()
        {
            FaBulletinRole = new HashSet<FaBulletinRole>();
            FaFlowFlownodeRole = new HashSet<FaFlowFlownodeRole>();
            FaRoleConfig = new HashSet<FaRoleConfig>();
            FaRoleFunction = new HashSet<FaRoleFunction>();
            FaRoleModule = new HashSet<FaRoleModule>();
            FaRoleQueryAuthority = new HashSet<FaRoleQueryAuthority>();
            FaUserRole = new HashSet<FaUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<FaBulletinRole> FaBulletinRole { get; set; }
        public virtual ICollection<FaFlowFlownodeRole> FaFlowFlownodeRole { get; set; }
        public virtual ICollection<FaRoleConfig> FaRoleConfig { get; set; }
        public virtual ICollection<FaRoleFunction> FaRoleFunction { get; set; }
        public virtual ICollection<FaRoleModule> FaRoleModule { get; set; }
        public virtual ICollection<FaRoleQueryAuthority> FaRoleQueryAuthority { get; set; }
        public virtual ICollection<FaUserRole> FaUserRole { get; set; }
    }
}
