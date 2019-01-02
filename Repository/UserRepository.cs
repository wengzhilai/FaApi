
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        IDbConnection connection = new MySqlConnection("server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;");
        public Result<fa_user> UserLogin(string username, string password)
        {
            Result<fa_user> reObj = new Result<fa_user>();

            FaLoginEntity user=new FaLoginEntity();
            user.LOGIN_NAME="asdfasdfasgdasdfasdfasdfasdfasgdasdfasdfasdfasdfasgdasdfasdfasdfasdfasgdasdfasdfasdfasdfasgdasdfasdf";

            ModelHelper<FaLoginEntity> helper=new ModelHelper<FaLoginEntity>(user);
            var errList=helper.Validate();
            if (errList.Count() > 0)
            {
                new ExceptionExtend(string.Join(",", errList.Select(x => x.ErrorMessage)));
            }
           
            string slq=helper.GetFindAllSql();
            var query = connection.Query<FaLoginEntity>(slq);

            return reObj;
        }
    }
}
