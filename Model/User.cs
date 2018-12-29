using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace FaApi.Model
{
    public class User
    {
        public ObjectId _id {get;set;}
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
