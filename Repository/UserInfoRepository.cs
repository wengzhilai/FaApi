
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
    public class UserInfoRepository : IUserInfoRepository
    {
        DapperHelper<FaUserInfoEntity> dbHelper = new DapperHelper<FaUserInfoEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<FaUserInfoEntity> SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public Task<IEnumerable<FaUserInfoEntity>> FindAll(Expression<Func<FaUserInfoEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }
    }
}
