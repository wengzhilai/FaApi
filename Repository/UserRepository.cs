
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
        public List<User> UserLogin(){
            var tmp= MongoContext.All<User>().ToList();
            return tmp;
        }
    }
}
