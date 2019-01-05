
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
        public List<FaUserEntity> FindAll(object inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }

        public Result<FaUserEntity> UserLogin(string username, string password)
        {
            Result<FaUserEntity> reObj = new Result<FaUserEntity>();
            DapperHelper<FaLoginEntity> dapper=new DapperHelper<FaLoginEntity>();
            var dict=new {LOGIN_NAME=username};
            var login= dapper.Single(dict);
            if (login != null)
            {
                if (login.PASSWORD.ToLower().Equals(Helper.StringHelp.Get32MD5One(password).ToLower()))
                {
                    reObj.Data = new DapperHelper<FaUserEntity>().Single(dict);
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
