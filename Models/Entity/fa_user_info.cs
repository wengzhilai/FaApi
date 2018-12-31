using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_user_info
    {
        public fa_user_info()
        {
            fa_user_event = new HashSet<fa_user_event>();
            fa_user_friend = new HashSet<fa_user_friend>();
        }

        public int ID { get; set; }
        public int? LEVEL_ID { get; set; }
        public int? FAMILY_ID { get; set; }
        public int? ELDER_ID { get; set; }
        public string LEVEL_NAME { get; set; }
        public int? FATHER_ID { get; set; }
        public int? MOTHER_ID { get; set; }
        public DateTime? BIRTHDAY_TIME { get; set; }
        public string BIRTHDAY_PLACE { get; set; }
        public decimal? IS_LIVE { get; set; }
        public DateTime? DIED_TIME { get; set; }
        public string DIED_PLACE { get; set; }
        public string REMARK { get; set; }
        public string SEX { get; set; }
        public string YEARS_TYPE { get; set; }
        public int? CONSORT_ID { get; set; }
        public string STATUS { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public string CREATE_USER_NAME { get; set; }
        public int CREATE_USER_ID { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string UPDATE_USER_NAME { get; set; }
        public int UPDATE_USER_ID { get; set; }
        public int? COUPLE_ID { get; set; }
        public string ALIAS { get; set; }
        public int? AUTHORITY { get; set; }

        public virtual ICollection<fa_user_event> fa_user_event { get; set; }
        public virtual ICollection<fa_user_friend> fa_user_friend { get; set; }
    }
}
