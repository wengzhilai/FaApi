using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin
    {
        public fa_bulletin()
        {
            fa_bulletin_file = new HashSet<fa_bulletin_file>();
            fa_bulletin_log = new HashSet<fa_bulletin_log>();
            fa_bulletin_review = new HashSet<fa_bulletin_review>();
            fa_bulletin_role = new HashSet<fa_bulletin_role>();
        }

        public int ID { get; set; }
        public string TITLE { get; set; }
        public string PIC { get; set; }
        public string TYPE_CODE { get; set; }
        public string CONTENT { get; set; }
        public int? USER_ID { get; set; }
        public string PUBLISHER { get; set; }
        public DateTime ISSUE_DATE { get; set; }
        public decimal IS_SHOW { get; set; }
        public decimal IS_IMPORT { get; set; }
        public decimal IS_URGENT { get; set; }
        public decimal AUTO_PEN { get; set; }
        public DateTime CREATE_TIME { get; set; }
        public DateTime UPDATE_TIME { get; set; }
        public string REGION { get; set; }

        public virtual ICollection<fa_bulletin_file> fa_bulletin_file { get; set; }
        public virtual ICollection<fa_bulletin_log> fa_bulletin_log { get; set; }
        public virtual ICollection<fa_bulletin_review> fa_bulletin_review { get; set; }
        public virtual ICollection<fa_bulletin_role> fa_bulletin_role { get; set; }
    }
}
