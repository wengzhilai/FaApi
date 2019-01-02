using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    /// <summary>
    /// 用于，根据实体类，生成相应的SQL，以及数据验证
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelHelper<T> where T : new()
    {
        /// <summary>
        /// 传入的参数
        /// </summary>
        T inEnt { get; set; }
        public ModelHelper(T model)
        {
            inEnt = model;
        }

        public ModelHelper()
        {
            inEnt = new T();
        }
        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(inEnt, new ValidationContext(inEnt, null, null), results, true);
            return results;
        }

        private List<string> _TableFields = null;
        /// <summary>
        /// 获取所有字段列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTableFields(List<string> saveFieldList = null, List<string> ignoreFieldList = null)
        {
            if (_TableFields == null)
            {
                List<string> reFieldStr = new List<string>();   //所有数据字段名
                Type type = typeof(T);
                PropertyInfo[] PropertyList = type.GetProperties();//得到该类的所有公共属性
                foreach (PropertyInfo proInfo in PropertyList)
                {
                    if (
                        (saveFieldList != null && saveFieldList.Count != 0 && !saveFieldList.Contains(proInfo.Name)) ||
                        (ignoreFieldList != null && ignoreFieldList.Count != 0 && ignoreFieldList.Contains(proInfo.Name))
                        )
                    {
                        continue;
                    }
                    object[] attrsPi = proInfo.GetCustomAttributes(true);
                    foreach (object obj in attrsPi)
                    {
                        if (obj is ColumnAttribute)//定义了Display属性的字段为字数库字段
                        {
                            reFieldStr.Add(proInfo.Name);
                            continue;
                        }
                    }
                }
                _TableFields = reFieldStr;
            }
            return _TableFields;
        }


        Dictionary<string, object> _Dirct;
        /// <summary>
        /// 获取类的键值数据
        /// </summary>
        /// <param name="saveFieldList"></param>
        /// <param name="ignoreFieldList"></param>
        /// <returns></returns>
        public Dictionary<string, object> GetDirct(List<string> saveFieldList = null, List<string> ignoreFieldList = null)
        {
            if (_Dirct == null)
            {

                Dictionary<string, object> reField = new Dictionary<string, object>();   //所有数据字段名

                Type type = typeof(T);
                PropertyInfo[] PropertyList = type.GetProperties();//得到该类的所有公共属性
                foreach (PropertyInfo proInfo in PropertyList)
                {
                    object[] attrsPi = proInfo.GetCustomAttributes(true);
                    foreach (object obj in attrsPi)
                    {
                        if (obj is DisplayAttribute)//定义了Display属性的字段为字数库字段
                        {
                            reField.Add(proInfo.Name, proInfo.GetValue(inEnt, null));
                            continue;
                        }
                    }
                }
                _Dirct = reField;
            }
            return _Dirct;
        }

        public string GetGetStr(List<string> saveFieldList = null, List<string> ignoreFieldList = null)
        {
            var dictList = GetDirct(saveFieldList, ignoreFieldList);
            string reStr = string.Join("&",dictList.Select(x => x.Key + "=" + x.Value.ToString()).ToArray());
            return reStr;
        }


        Dictionary<string, object> _DisplayDirct;
        /// <summary>
        /// 获取类的每个字段的中文说明
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetDisplayDirct()
        {
            if (_DisplayDirct == null)
            {

                Dictionary<string, object> reField = new Dictionary<string, object>();   //所有数据字段名
                Type type = typeof(T);
                PropertyInfo[] PropertyList = type.GetProperties();//得到该类的所有公共属性
                foreach (PropertyInfo proInfo in PropertyList)
                {
                    object[] attrsPi = proInfo.GetCustomAttributes(true);
                    foreach (object obj in attrsPi)
                    {
                        if (obj is DisplayAttribute)//定义了Display属性的字段为字数库字段
                        {
                            var display = (DisplayAttribute)obj;
                            reField.Add(proInfo.Name, display.Name);
                            continue;
                        }
                    }
                }
                _DisplayDirct = reField;
            }
            return _DisplayDirct;
        }


        private string _TableName = null;
        /// <summary>
        /// 得到表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            if (_TableName == null)
            {

                Type type = typeof(T);
                Attribute[] attrs = Attribute.GetCustomAttributes(type);  // 得到对类的自定义属性数组
                foreach (System.Attribute attr in attrs)
                {
                    if (attr is TableAttribute)
                    {
                        TableAttribute a = (TableAttribute)attr;
                        _TableName = a.Name;
                    }
                }
            }
            return _TableName;
        }

        string _key;
        /// <summary>
        /// 获取主键
        /// </summary>
        /// <returns></returns>
        public string GetKeyField()
        {
            if (_key == null)
            {

                Type type = typeof(T);
                PropertyInfo[] proInfoArr = type.GetProperties();//得到该类的所有公共属性
                for (int a = 0; a < proInfoArr.Length; a++)
                {
                    PropertyInfo proInfo = proInfoArr[a];
                    object[] attrsPro = proInfo.GetCustomAttributes(typeof(KeyAttribute), true);
                    if (attrsPro.Length > 0)
                    {
                        _key = proInfo.Name;
                        break;
                    }
                }
            }
            return _key;
        }

        /// <summary>
        /// 新增加数据，并返回增加的ID
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string GetSaveSql(List<string> saveFieldList = null, List<string> ignoreFieldList = null)
        {
            string sql = null;
            if (ignoreFieldList == null) ignoreFieldList = new List<string> { GetKeyField() };
            sql = "INSERT INTO  " + GetTableName() + "(" + string.Join(",", GetTableFields(saveFieldList, ignoreFieldList)) + ") VALUES(" + string.Join(",", GetTableFields(saveFieldList, ignoreFieldList).Select(x => "@" + x)) + ")";
            sql += "\r\n select @@IDENTITY ";
            return sql;
        }

        public string GetSaveSqlNoIdentity(List<string> saveFieldList = null)
        {
            string sql = null;
            var ignoreFieldList = new List<string> { GetKeyField() };
            sql = "INSERT INTO  " + GetTableName() + "(" + string.Join(",", GetTableFields(saveFieldList, ignoreFieldList)) + ") VALUES(" + string.Join(",", GetTableFields(saveFieldList, ignoreFieldList).Select(x => "@" + x)) + ")";
            return sql;
        }

        /// <summary>
        /// 生成查询的SQL语句
        /// 如果没有传条件值，则默认为主键
        /// </summary>
        /// <param name="saveFieldList">保存的字段</param>
        /// <param name="ignoreFieldList">忽略的字段</param>
        /// <param name="whereList">条件字段</param>
        /// <returns></returns>
        public string GetUpdateSql(List<string> saveFieldList = null, List<string> ignoreFieldList = null, List<string> whereList = null)
        {
            string key = GetKeyField();
            if (ignoreFieldList == null) ignoreFieldList = new List<string> { key };
            if (whereList == null) whereList = new List<string> { key };
            string sql = null;
            sql = "UPDATE " + GetTableName() + " SET ";
            sql += string.Join(",", GetTableFields(saveFieldList, ignoreFieldList).Select(x => string.Format("{0}=@{0}", x)));
            sql += " WHERE ";
            sql += string.Join(" AND ", whereList.Select(x => string.Format("{0}=@{0}", x)));
            return sql;
        }

        public string GetUpdateSql(string upStr, string whereStr)
        {
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2}", GetTableName(), upStr, whereStr);
            return sql;
        }

        /// <summary>
        /// 获取删除SQL
        /// </summary>
        /// <param name="filterList"></param>
        /// <returns></returns>
        public string GetDeleteSql(List<string> filterList = null)
        {
            if (filterList == null || filterList.Count() == 0)
            {
                throw new Exception("删除参数，不能为空");
            }

            string sql = "DELETE FROM {0} WHERE {1}";
            sql = string.Format(sql, GetTableName(), string.Join(" AND ", filterList.Select(x => string.Format("{0}=@{0}", x))));
            return sql;
        }

        /// <summary>
        /// 获取查看所有SQL
        /// </summary>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public string GetFindAllSql(DtoSearch inSearch)
        {
            string key = GetKeyField();
            if (inSearch.OrderType == null)
            {
                inSearch.OrderType = string.Format("{0} DESC", key);
            }
            string sql = "SELECT Row_number()OVER (ORDER BY {2}) AS RowNumber ,{0} FROM {1} WHERE {3}";
            if (inSearch.FilterList == null)
            {
                sql = "SELECT Row_number()OVER (ORDER BY {2}) AS RowNumber ,{0} FROM {1}";
                sql = string.Format(sql, string.Join(",",
                                GetTableFields()),
                                GetTableName(),
                                inSearch.OrderType
                                );
            }
            else
            {
                sql = string.Format(sql, string.Join(",",
                                GetTableFields()),
                                GetTableName(),
                                inSearch.OrderType,
                                string.Join(" AND ", inSearch.FilterList.Select(x => string.Format("{0}=@{0}", x)))
                                );
            }


            if (inSearch.PageIndex < 1) inSearch.PageIndex = 1;
            if (inSearch.PageSize < 1) inSearch.PageSize = 10;
            sql = string.Format(@"
                SELECT * FROM ({0}) T 
                WHERE  RowNumber BETWEEN ( ( ( {1} - 1 ) * {2} ) + 1 ) AND ( {1} * {2} )
                ORDER  BY  {3}
                ", sql, inSearch.PageIndex, inSearch.PageSize, inSearch.OrderType);
            return sql;
        }

        public string GetFindAllSql(List<string> filterList)
        {
            if (filterList == null || filterList.Count() == 0)
            {
                return GetFindAllSql();
            }
            else
            {
                return GetFindAllSql(string.Join(" AND ", filterList.Select(x => string.Format("{0}=@{0}", x))));
            }
        }

        public string GetFindAllSql(string whereStr = null, List<string> allItems = null)
        {
            string sql = "SELECT {0} FROM {1} WHERE {2}";
            if (string.IsNullOrEmpty(whereStr))
            {
                sql = "SELECT {0} FROM {1}";
                sql = string.Format(sql, string.Join(",", GetTableFields(allItems)), GetTableName());
            }
            else
            {
                sql = string.Format(sql, string.Join(",", GetTableFields(allItems)), GetTableName(), whereStr);
            }
            return sql;
        }

        /// <summary>
        /// 查看满足条件的数量
        /// </summary>
        /// <param name="inSearch"></param>
        /// <returns></returns>
        public string GetFindNumSql(DtoSearch inSearch)
        {
            string key = GetKeyField();

            string sql = "SELECT Count(1) num FROM {0} WHERE {1}";
            if (inSearch.FilterList == null)
            {
                sql = "SELECT  Count(1) num FROM {0}";
                sql = string.Format(sql, GetTableName());
            }
            else
            {
                sql = string.Format(sql,
                                GetTableName(),
                                string.Join(" AND ", inSearch.FilterList.Select(x => string.Format("convert(nvarchar(max),{0})=@{0}", x.Key)))
                                );
            }
            return sql;
        }

        public string GetFindNumSql(List<string> filterList)
        {
            if (filterList == null || filterList.Count() == 0)
            {
                return GetFindNumSql();
            }
            else
            {
                return GetFindNumSql(string.Join(" AND ", filterList.Select(x => string.Format("convert(nvarchar(max),{0})=@{0}", x))));

            }
        }

        public string GetFindNumSql(string whereStr = null)
        {

            string sql = "SELECT Count(1) num FROM {0} WHERE {1}";
            if (string.IsNullOrEmpty(whereStr))
            {
                sql = "SELECT  Count(1) num FROM {0}";
                sql = string.Format(sql, GetTableName());
            }
            else
            {
                sql = string.Format(sql, GetTableName(), whereStr);
            }
            return sql;
        }


        /// <summary>
        /// 获取单条SQL
        /// </summary>
        /// <param name="filterList"></param>
        /// <returns></returns>
        public string GetSingleSql(List<string> filterList,string orderByStr="")
        {
            string key = GetKeyField();

            string sql = "SELECT top 1 {0} FROM {1} WHERE {2} {3}";
            if (filterList == null || filterList.Count() == 0)
            {
                sql = "SELECT top 1 {0} FROM {1} {2}";
                sql = string.Format(sql, string.Join(",", GetTableFields()), GetTableName(),orderByStr);
            }
            else
            {
                sql = string.Format(sql, string.Join(",", GetTableFields()), GetTableName(), string.Join(" AND ", filterList.Select(x => string.Format("{0}=@{0}", x))), orderByStr);
            }
            return sql;
        }

        /// <summary>
        /// 获取单条SQL 根据主键获取单条
        /// </summary>
        /// <returns></returns>
        public string GetSingleSql()
        {
            string key = GetKeyField();
            string sql = "SELECT top 1 {0} FROM {1} WHERE {2}=@{2}";
            sql = string.Format(sql, string.Join(",", GetTableFields()), GetTableName(), key);
            return sql;
        }

        public string GetSingleSql(string whereStr = "")
        {
            string key = GetKeyField();
            string sql = "SELECT top 1 {0} FROM {1} WHERE {2}=@{2}";
            if (string.IsNullOrEmpty(whereStr))
            {
                sql = string.Format(sql, string.Join(",", GetTableFields()), GetTableName(), key);
            }
            else
            {
                sql = "SELECT top 1 {0} FROM {1} WHERE {2}";
                sql = string.Format(sql, string.Join(",", GetTableFields()), GetTableName(), whereStr);

            }
            return sql;
        }


    }
}
