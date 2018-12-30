using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaLogin
    {
        public FaLogin()
        {
            FaOauthLogin = new HashSet<FaOauthLogin>();
        }

        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddr { get; set; }
        public string VerifyCode { get; set; }
        public DateTime? VerifyTime { get; set; }
        public int? IsLocked { get; set; }
        public DateTime? PassUpdateDate { get; set; }
        public string LockedReason { get; set; }
        public int? FailCount { get; set; }

        public virtual ICollection<FaOauthLogin> FaOauthLogin { get; set; }
    }
}
