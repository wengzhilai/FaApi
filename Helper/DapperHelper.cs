
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


namespace Helper
{
    public class DapperHelper<T> where T : new()
    {
        public ModelHelper<T> modelHelper;
        IDbConnection connection;

        public DapperHelper(T indEnt)
        {
            modelHelper = new ModelHelper<T>(indEnt);
            connection = new MySqlConnection("server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;");
        }
        public DapperHelper()
        {
            modelHelper = new ModelHelper<T>();
            connection = new MySqlConnection("server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;");
        }

        public int Save(DtoSave<T> inEnt)
        {
            var mh = new ModelHelper<T>(inEnt.Data);
            string sql = mh.GetSaveSql(null, inEnt.IgnoreFieldList);
            var result = connection.Execute(sql, mh.GetDynamicParameters());
            return result;
        }

        public int Saves(DtoSave<List<T>> inEnt)
        {
            int reInt = 0;
            var mh = new ModelHelper<T>();
            string sql = mh.GetSaveSql(inEnt.SaveFieldList, inEnt.IgnoreFieldList);
            var result = connection.Execute(sql, inEnt.Data);
            return result;
        }


        public List<T> FindAll(T inEnt, DtoSearch inSearch)
        {

            var mh = new ModelHelper<T>(inEnt);
            string sql = mh.GetFindAllSql(inSearch);
            var query = connection.Query<T>(sql, mh.GetDynamicParameters());
            return query.ToList();
        }


        public List<T> FindAll(object inParm = null)
        {
            string sql = "";
            List<T> reList = new List<T>();
            if (inParm == null)
            {
                sql = modelHelper.GetFindAllSql();
                reList = connection.Query<T>(sql).ToList();
            }
            else
            {
                sql = modelHelper.GetFindAllSql(TypeChange.DynamicToKeyList(inParm));
                reList = connection.Query<T>(sql, inParm).ToList();
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
            return connection.Query<T>(sql).ToList();
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
            var ds = connection.ExecuteScalar(sql, mh.GetDynamicParameters());
            return Convert.ToInt32(ds);
        }


        public int Count(object inParm)
        {
            var mh = new ModelHelper<T>();
            string sql = mh.GetFindNumSql(TypeChange.DynamicToKeyList(inParm));

            var ds = connection.ExecuteScalar(sql, inParm);
            return Convert.ToInt32(ds);

        }


        public int Count(string whereStr)
        {
            var mh = new ModelHelper<T>();
            string sql = mh.GetFindNumSql(whereStr);
            var ds = connection.ExecuteScalar(sql);
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
            var result = connection.Execute(sql, mh.GetDynamicParameters());
            return result;
        }

        public int Update(string upStr, string whereStr)
        {
            string sql = modelHelper.GetUpdateSql(upStr, whereStr);
            var result = connection.Execute(sql);
            return result;
        }

        public T Single(object inParm = null, string order = "")
        {
            string sql = modelHelper.GetSingleSql(TypeChange.DynamicToKeyList(inParm), order);
            var query = connection.QueryFirst<T>(sql, inParm);
            return query;
        }

        public T SingleByKey<t>(t key)
        {
            string sql = modelHelper.GetSingleSql();
            DynamicParameters dynamicP = new DynamicParameters();
            dynamicP.Add(modelHelper.GetKeyField(), key);

            var query = connection.QueryFirst<T>(sql, dynamicP);
            return query;
        }

        public T SingleWhereSql(string whereStr)
        {
            var mh = new ModelHelper<T>();
            string sql = mh.GetSingleSql(whereStr);
            var query = connection.QueryFirst<T>(sql);
            return query;
        }

        public int Delete(object inParm = null)
        {
            try
            {
                string sql = modelHelper.GetDeleteSql(TypeChange.DynamicToKeyList(inParm));
                var query = connection.Execute(sql);
                return query;
            }
            catch (Exception ex)
            {
                throw new ExceptionExtend("请先删除子项");
            }
        }
    }
}
