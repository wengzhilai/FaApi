
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
    public class FaLoginHistoryRepository
    {
        DapperHelper<FaLoginHistoryEntity> dbHelper = new DapperHelper<FaLoginHistoryEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaLoginHistoryEntity SingleByKey(int key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaLoginHistoryEntity> FindAll(Expression<Func<FaLoginHistoryEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }
        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public int Save(DtoSave<FaLoginHistoryEntity> inObj)
        {
            dbHelper.SaveAsync(inObj);
            return 1;
        }
    }
}
