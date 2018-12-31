using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_oauth
    {
        public fa_oauth()
        {
            fa_oauth_login = new HashSet<fa_oauth_login>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string REG_URL { get; set; }
        public string LOGIN_URL { get; set; }
        public string REMARK { get; set; }

        public virtual ICollection<fa_oauth_login> fa_oauth_login { get; set; }
    }
}
