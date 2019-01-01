using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_role : MongodbEntity
    {
        public fa_role()
        {
            fa_bulletin_role = new HashSet<fa_bulletin_role>();
            fa_flow_flownode_role = new HashSet<fa_flow_flownode_role>();
            fa_role_config = new HashSet<fa_role_config>();
            fa_role_function = new HashSet<fa_role_function>();
            fa_role_module = new HashSet<fa_role_module>();
            fa_role_query_authority = new HashSet<fa_role_query_authority>();
            fa_user_role = new HashSet<fa_user_role>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string REMARK { get; set; }
        public int? TYPE { get; set; }

        public virtual ICollection<fa_bulletin_role> fa_bulletin_role { get; set; }
        public virtual ICollection<fa_flow_flownode_role> fa_flow_flownode_role { get; set; }
        public virtual ICollection<fa_role_config> fa_role_config { get; set; }
        public virtual ICollection<fa_role_function> fa_role_function { get; set; }
        public virtual ICollection<fa_role_module> fa_role_module { get; set; }
        public virtual ICollection<fa_role_query_authority> fa_role_query_authority { get; set; }
        public virtual ICollection<fa_user_role> fa_user_role { get; set; }
    }
}
