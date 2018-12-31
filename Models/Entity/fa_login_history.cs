using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_login_history
    {
        public int ID { get; set; }
        public int? USER_ID { get; set; }
        public DateTime? LOGIN_TIME { get; set; }
        public string LOGIN_HOST { get; set; }
        public DateTime? LOGOUT_TIME { get; set; }
        public int? LOGIN_HISTORY_TYPE { get; set; }
        public string MESSAGE { get; set; }
    }
}
