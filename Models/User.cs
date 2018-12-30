using System;
using MongoDB.Bson;

namespace Models
{
    public class User:MongodbEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
