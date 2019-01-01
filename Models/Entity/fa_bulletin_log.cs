using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin_log : MongodbEntity
    {
        public int ID { get; set; }
        public int BULLETIN_ID { get; set; }
        public int USER_ID { get; set; }
        public DateTime LOOK_TIME { get; set; }

        public virtual fa_bulletin BULLETIN_ { get; set; }
    }
}
