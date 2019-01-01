using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_login : MongodbEntity
    {
        public fa_login()
        {
            fa_oauth_login = new HashSet<fa_oauth_login>();
        }

        public int ID { get; set; }
        public Int64 LOGIN_NAME { get; set; }
        public string PASSWORD { get; set; }
        public Int64 PHONE_NO { get; set; }
        public string EMAIL_ADDR { get; set; }
        public string VERIFY_CODE { get; set; }
        public DateTime? VERIFY_TIME { get; set; }
        public int? IS_LOCKED { get; set; }
        public DateTime? PASS_UPDATE_DATE { get; set; }
        public string LOCKED_REASON { get; set; }
        public int? FAIL_COUNT { get; set; }

        public virtual ICollection<fa_oauth_login> fa_oauth_login { get; set; }
    }
}
