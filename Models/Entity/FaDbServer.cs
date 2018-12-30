using System;
using System.Collections.Generic;

namespace Models.Entity
{
    public partial class FaDbServer
    {
        public int Id { get; set; }
        public int DbTypeId { get; set; }
        public string Type { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Dbname { get; set; }
        public string Dbuid { get; set; }
        public string Password { get; set; }
        public string Remark { get; set; }
        public string DbLink { get; set; }
        public string Nickname { get; set; }
        public string ToPathM { get; set; }
        public string ToPathD { get; set; }

        public virtual FaDbServerType DbType { get; set; }
    }
}
