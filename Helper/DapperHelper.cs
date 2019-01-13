
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using MySql.Data.MySqlClient;
using Dapper;
using System.Linq.Expressions;

namespace Helper
{
    public class DapperHelper<T> where T : class, new()
    {
        public ModelHelper<T> modelHelper;
        IDbConnection connection;
        IDbTransaction transaction=null;

        public DapperHelper()
        {
            modelHelper = new ModelHelper<T>();
            connection = new MySqlConnection("server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;");
        }

        public DapperHelper(IDbConnection _connection,IDbTransaction _transaction)
        {
            transaction=_transaction;
            connection=_connection;
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection(){
            return connection;
        }

        /// <summary>
        /// 获取事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetTransaction(){
            return transaction;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void TranscationBegin(){
            connection.Open();
            transaction=connection.BeginTransaction();
        }
        
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void TranscationRollback(){
            transaction.Rollback();
            connection.Close();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void TranscationCommit(){
            transaction.Commit();
            connection.Close();
        }


        

        public int Save(DtoSave<T> inEnt)
        {
            var mh = new ModelHelper<T>(inEnt.Data);
            string sql = mh.GetSaveSql(null, inEnt.IgnoreFieldList);
            var result = connection.Execute(sql, mh.GetDynamicParameters(),transaction);
            return result;
        }

        public Task<int> SaveAsync(DtoSave<T> inEnt)
        {
            var mh = new ModelHelper<T>(inEnt.Data);
            string sql = mh.GetSaveSql(null, inEnt.IgnoreFieldList);
            var result = connection.ExecuteAsync(sql, mh.GetDynamicParameters(),transaction);
            return result;
        }

        public int Saves(DtoSave<List<T>> inEnt)
        {
            var mh = new ModelHelper<T>();
            string sql = mh.GetSaveSql(inEnt.SaveFieldList, inEnt.IgnoreFieldList);
            var result = connection.Execute(sql, inEnt.Data,transaction);
            return result;
        }


        public List<T> FindAll(T inEnt, DtoSearch inSearch)
        {

            var mh = new ModelHelper<T>(inEnt);
            string sql = mh.GetFindAllSql(inSearch);
            var query = connection.Query<T>(sql, mh.GetDynamicParameters(),transaction);
            return query.ToList();
        }


        public List<T> FindAll(Expression<Func<T, bool>> where)
        {
            string sql = "";
            List<T> reList = new List<T>();
            if (where == null)
            {
                sql = modelHelper.GetFindAllSql();
                reList = connection.Query<T>(sql,null,transaction).ToList();
            }
            else
            {
                List<KeyValuePair<string, object>> listSqlParaModel = new List<KeyValuePair<string, object>>();
                var whereStr = Helper.LambdaToSqlHelper.GetWhereSql<T>(where, listSqlParaModel);

                sql = modelHelper.GetFindAllSql(whereStr);
                reList = connection.Query<T>(sql, listSqlParaModel,transaction).ToList();
            }
            return reList;
        }

        /// <summary>
        /// 获取指定,字段的类
        /// </summary>
        /// <param name="whereStr">不带where的字符串</param>
        /// <returns></returns>
        public List<T> FindAll(string whereStr, List<string> allItems = null)
        {
            string sql = modelHelper.GetFindAllSql(whereStr, allItems);
            return connection.Query<T>(sql,null,transaction).ToList();
        }

        /// <summary>
        /// 获取满足条件的个数
        /// </summary>
        /// <param name="inEnt"></param>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public int FindNum(T inEnt, DtoSearch inSearch)
        {

            var mh = new ModelHelper<T>(inEnt);
            string sql = mh.GetFindNumSql(inSearch);
            var ds = connection.ExecuteScalar(sql, mh.GetDynamicParameters(),transaction);
            return Convert.ToInt32(ds);
        }


        public int Count(Expression<Func<T, bool>> where)
        {
            var mh = new ModelHelper<T>();
            List<KeyValuePair<string, object>> listSqlParaModel = new List<KeyValuePair<string, object>>();
            var whereStr = Helper.LambdaToSqlHelper.GetWhereSql<T>(where, listSqlParaModel);

            string sql = mh.GetFindNumSql(whereStr);
            var ds = connection.ExecuteScalar(sql, listSqlParaModel,transaction);
            return Convert.ToInt32(ds);

        }


        public int Count(string whereStr)
        {
            var mh = new ModelHelper<T>();
            string sql = mh.GetFindNumSql(whereStr);
            var ds = connection.ExecuteScalar(sql,null,transaction);
            return Convert.ToInt32(ds);
        }

        /// <summary>
        /// 返回影响的条数
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public int Update(DtoSave<T> inObj)
        {
            var mh = new ModelHelper<T>(inObj.Data);
            string sql = mh.GetUpdateSql(inObj.SaveFieldList, inObj.IgnoreFieldList, inObj.WhereList);
            var result = connection.Execute(sql, mh.GetDynamicParameters(),transaction);
            return result;
        }

        public int Update(string upStr, string whereStr)
        {
            string sql = modelHelper.GetUpdateSql(upStr, whereStr);
            var result = connection.Execute(sql,null,transaction);
            return result;
        }

        public T Single(Expression<Func<T, bool>> where, string order = "")
        {
            List<KeyValuePair<string, object>> listSqlParaModel = new List<KeyValuePair<string, object>>();
            var whereStr = Helper.LambdaToSqlHelper.GetWhereSql<T>(where, listSqlParaModel);
            string sql = modelHelper.GetSingleSql(whereStr, order);
            var query = connection.QueryFirst<T>(sql, listSqlParaModel,transaction);
            return query;
        }

        public T SingleByKey<t>(t key)
        {
            string sql = modelHelper.GetSingleSql();
            DynamicParameters dynamicP = new DynamicParameters();
            dynamicP.Add(modelHelper.GetKeyField(), key);

            var query = connection.QueryFirst<T>(sql, dynamicP,transaction);
            return query;
        }


        public int Delete(Expression<Func<T, bool>> where)
        {
            try
            {
                List<KeyValuePair<string, object>> listSqlParaModel = new List<KeyValuePair<string, object>>();
                var whereStr = Helper.LambdaToSqlHelper.GetWhereSql<T>(where, listSqlParaModel);
                string sql = modelHelper.GetDeleteSql(whereStr);
                var query = connection.Execute(sql,listSqlParaModel,transaction);
                return query;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog<DapperHelper<T>>(ex.ToString());
                throw new ExceptionExtend("请先删除子项");
            }
        }
    }
}
