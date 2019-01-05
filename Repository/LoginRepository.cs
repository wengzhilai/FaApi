
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
    public class LoginRepository : ILoginRepository
    {
        DapperHelper<FaLoginEntity> dbHelper = new DapperHelper<FaLoginEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaLoginEntity SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaLoginEntity> FindAll(object inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }
    }
}
