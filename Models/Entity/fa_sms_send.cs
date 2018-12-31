using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_sms_send
    {
        public string GUID { get; set; }
        public int? MESSAGE_ID { get; set; }
        public string PHONE_NO { get; set; }
        public DateTime? ADD_TIME { get; set; }
        public DateTime? SEND_TIME { get; set; }
        public string CONTENT { get; set; }
        public string STAUTS { get; set; }
        public int TRY_NUM { get; set; }
    }
}
