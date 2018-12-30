using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaOauth
    {
        public FaOauth()
        {
            FaOauthLogin = new HashSet<FaOauthLogin>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string RegUrl { get; set; }
        public string LoginUrl { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<FaOauthLogin> FaOauthLogin { get; set; }
    }
}
