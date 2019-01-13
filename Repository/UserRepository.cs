
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
using System.Linq.Expressions;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        DapperHelper<FaUserEntity> dbHelper = new DapperHelper<FaUserEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaUserEntity SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }
        

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaUserEntity> FindAll(Expression<Func<FaUserEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }

        public int Update(DtoSave<FaUserEntity> inObj)
        {
            return dbHelper.Update(inObj);
        }

        public Result<FaUserEntity> UserLogin(string username, string password)
        {
            Result<FaUserEntity> reObj = new Result<FaUserEntity>();
            DapperHelper<FaLoginEntity> dapper=new DapperHelper<FaLoginEntity>();
            var login= dapper.Single(x=>x.LOGIN_NAME==username);
            if (login != null)
            {
                if (login.PASSWORD.ToLower().Equals(Helper.StringHelp.Get32MD5One(password).ToLower()))
                {
                    reObj.Data = new DapperHelper<FaUserEntity>().Single(x=>x.LOGIN_NAME==username);
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
