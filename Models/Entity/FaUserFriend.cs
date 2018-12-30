using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaUserFriend
    {
        public int UserId { get; set; }
        public int FriendId { get; set; }

        public virtual FaUser Friend { get; set; }
        public virtual FaUserInfo User { get; set; }
    }
}
