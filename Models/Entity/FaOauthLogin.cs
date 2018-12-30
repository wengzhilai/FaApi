using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaOauthLogin
    {
        public int OauthId { get; set; }
        public int LoginId { get; set; }

        public virtual FaLogin Login { get; set; }
        public virtual FaOauth Oauth { get; set; }
    }
}
