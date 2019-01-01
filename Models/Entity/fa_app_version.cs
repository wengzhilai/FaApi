using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_app_version : MongodbEntity
    {
        public int ID { get; set; }
        public decimal IS_NEW { get; set; }
        public string TYPE { get; set; }
        public string REMARK { get; set; }
        public DateTime? UPDATE_TIME { get; set; }
        public string UPDATE_URL { get; set; }
    }
}
