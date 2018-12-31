using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_db_server
    {
        public int ID { get; set; }
        public int DB_TYPE_ID { get; set; }
        public string TYPE { get; set; }
        public string IP { get; set; }
        public int PORT { get; set; }
        public string DBNAME { get; set; }
        public string DBUID { get; set; }
        public string PASSWORD { get; set; }
        public string REMARK { get; set; }
        public string DB_LINK { get; set; }
        public string NICKNAME { get; set; }
        public string TO_PATH_M { get; set; }
        public string TO_PATH_D { get; set; }

        public virtual fa_db_server_type DB_TYPE_ { get; set; }
    }
}
