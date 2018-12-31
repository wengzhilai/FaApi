using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_query
    {
        public fa_query()
        {
            fa_role_query_authority = new HashSet<fa_role_query_authority>();
        }

        public int ID { get; set; }
        public string NAME { get; set; }
        public string CODE { get; set; }
        public decimal AUTO_LOAD { get; set; }
        public int PAGE_SIZE { get; set; }
        public decimal SHOW_CHECKBOX { get; set; }
        public decimal IS_DEBUG { get; set; }
        public decimal? FILTR_LEVEL { get; set; }
        public int? DB_SERVER_ID { get; set; }
        public string QUERY_CONF { get; set; }
        public string QUERY_CFG_JSON { get; set; }
        public string IN_PARA_JSON { get; set; }
        public string JS_STR { get; set; }
        public string ROWS_BTN { get; set; }
        public string HEARD_BTN { get; set; }
        public string REPORT_SCRIPT { get; set; }
        public string CHARTS_CFG { get; set; }
        public string CHARTS_TYPE { get; set; }
        public string FILTR_STR { get; set; }
        public string REMARK { get; set; }
        public string NEW_DATA { get; set; }

        public virtual ICollection<fa_role_query_authority> fa_role_query_authority { get; set; }
    }
}
