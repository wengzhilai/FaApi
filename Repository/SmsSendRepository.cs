
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
    public class SmsSendRepository : ISmsSendRepository
    {
        DapperHelper<FaSmsSendEntity> dbHelper = new DapperHelper<FaSmsSendEntity>();
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public FaSmsSendEntity SingleByKey<t>(t key)
        {
            return dbHelper.SingleByKey(key);
        }

        /// <summary>
        /// 查找所有
        /// </summary>
        /// <param name="inParm"></param>
        /// <returns></returns>
        public List<FaSmsSendEntity> FindAll(Expression<Func<FaSmsSendEntity, bool>> inParm = null)
        {
            return dbHelper.FindAll(inParm);
        }
        /// <summary>
        /// 获取发送短信记录数
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public int Count(string phone,string code)
        {
            var nowDate = DateTime.Now.AddMinutes(-AppSettingsManager.Config.VerifyExpireMinute);
            //    x.ADD_TIME > nowDate
            //             && x.PHONE_NO == inEnt.loginName
            //             && x.CONTENT == inEnt.code
            var searchObj=new {ADD_TIME=nowDate};
            
            return dbHelper.Count(searchObj);
        }
    }
}
