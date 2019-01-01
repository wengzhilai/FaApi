using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user : MongodbEntity
    {
        public fa_user()
        {
            fa_user_district = new HashSet<fa_user_district>();
            fa_user_file = new HashSet<fa_user_file>();
            fa_user_friend = new HashSet<fa_user_friend>();
            fa_user_role = new HashSet<fa_user_role>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string LOGIN_NAME { get; set; }
        public int? ICON_FILES_ID { get; set; }
        public int DISTRICT_ID { get; set; }
        public decimal? IS_LOCKED { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public int? LOGIN_COUNT { get; set; }
        public DateTime? LAST_LOGIN_TIME { get; set; }
        public DateTime? LAST_LOGOUT_TIME { get; set; }
        public DateTime? LAST_ACTIVE_TIME { get; set; }
        public string REMARK { get; set; }

        public virtual fa_district DISTRICT_ { get; set; }
        public virtual ICollection<fa_user_district> fa_user_district { get; set; }
        public virtual ICollection<fa_user_file> fa_user_file { get; set; }
        public virtual ICollection<fa_user_friend> fa_user_friend { get; set; }
        public virtual ICollection<fa_user_role> fa_user_role { get; set; }
    }
}
