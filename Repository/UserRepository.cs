
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        public Result<fa_user> UserLogin(string username, string password)
        {
            Result<fa_user> reObj = new Result<fa_user>();
            // var tmp = MongoContext.Get<fa_login>(x => x.LOGIN_NAME.Equals(username));
            var allLogin=MongoContext.All<fa_login>();
            var tmp = MongoContext.All<fa_login>().SingleOrDefault(x => x.LOGIN_NAME.Equals(username));
            if (tmp != null)
            {
                if (tmp.PASSWORD.Equals(Helper.StringHelp.Get32MD5One(password)))
                {
                    reObj.Data = MongoContext.Get<fa_user>(x => x.LOGIN_NAME == username);
                }
                else
                {
                    reObj.Msg = "密码错误";
                }
            }
            else
            {
                reObj.Msg = "用户名不存在";
            }
            return reObj;
        }
    }
}
