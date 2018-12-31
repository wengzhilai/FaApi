using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_friend
    {
        public int USER_ID { get; set; }
        public int FRIEND_ID { get; set; }

        public virtual fa_user FRIEND_ { get; set; }
        public virtual fa_user_info USER_ { get; set; }
    }
}
