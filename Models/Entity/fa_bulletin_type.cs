﻿using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class fa_bulletin_type : MongodbEntity
    {
        public int ID { get; set; }
        public string NAME { get; set; }
    }
}
