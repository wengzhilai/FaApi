using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_message
    {
        public int ID { get; set; }
        public int? MESSAGE_TYPE_ID { get; set; }
        public int? KEY_ID { get; set; }
        public string TITLE { get; set; }
        public string CONTENT { get; set; }
        public DateTime? CREATE_TIME { get; set; }
        public string CREATE_USERNAME { get; set; }
        public int? CREATE_USERID { get; set; }
        public string STATUS { get; set; }
        public string PUSH_TYPE { get; set; }
        public int? DISTRICT_ID { get; set; }
        public string ALL_ROLE_ID { get; set; }

        public virtual fa_message_type MESSAGE_TYPE_ { get; set; }
    }
}
