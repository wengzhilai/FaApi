
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public string UserLogin(){
            MongoContext.Initialize(AppSettingsManager.MongoSettings.Connection);
            MongoContext.Insert<User>(new User(){UserName="1111",Password="2222"});
            var tmp= MongoContext.All<User>().ToList();
            return tmp.Count().ToString();
        }
    }
}
