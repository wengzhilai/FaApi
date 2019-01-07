
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
    public class ElderRepository : IElderRepository
    {
        DapperHelper<FaElderEntity> dbHelper = new DapperHelper<FaElderEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaElderEntity SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaElderEntity> FindAll(Expression<Func<FaElderEntity, bool>> where)
        {
            return dbHelper.FindAll(where);
        }
    }
}
