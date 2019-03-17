
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
using System.Globalization;
using System.Text;

namespace Repository
{
    public class QueryRepository:IQueryRepository
    {
        public QueryRepository()
        {
            //ExceptionlessClient.Default.Startup("ZevZKuGg0jZOohtFYKma1kcDiCWvUhBTROnzR1pN");
        }
        private DapperHelper<FaQueryEntity> dal = new DapperHelper<FaQueryEntity>();

        /// <summary>
        /// 获取所有数据
        /// msg为SQL语句
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        public async Task<Result<DataGridDataJson>> QueryExecute(QuerySearchModel inEnt)
        {
            Result<DataGridDataJson> reObj = new Result<DataGridDataJson>();
            DataGridDataJson reEnt = new DataGridDataJson();

            FaQueryEntity query = await dal.Single(i => i.CODE == inEnt.Code);
            if (query == null)
            {
                return reObj;
            }
            IList<QueryCfg> cfg = TypeChange.ToJsonObject<List<QueryCfg>>(query.QUERY_CFG_JSON);

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query, ref whereStr);
            reObj.Msg = AllSql;
            if (string.IsNullOrEmpty(inEnt.OrderStr)) inEnt.OrderStr = "(SELECT 0)";
            try
            {
                DataTable dt = DapperHelper.GetDataTable(AllSql);
                reEnt.rows = dt;
                reEnt.rows.TableName = "tables1";
                reEnt.total = reEnt.rows.Rows.Count;
                reObj.Data = reEnt;

            }
            catch
            {
                return reObj;
            }
            return reObj;
        }
        /// <summary>
        /// 获取Csv数据,支持大数据下载
        /// </summary>
        /// <param name="inEnt"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public async Task<Result<List<byte>>> QueryExecuteCsv(QuerySearchModel inEnt)
        {
            Result<List<byte>> reObj = new Result<List<byte>>();
            List<byte> reEnt = new List<byte>();

            FaQueryEntity query = await dal.Single(i => i.CODE == inEnt.Code);
            if (query == null)
            {

                return reObj;
            }
            IList<QueryCfg> cfg = TypeChange.ToJsonObject<List<QueryCfg>>(query.QUERY_CFG_JSON);

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query, ref whereStr);
            //如果条件为空
            if (!string.IsNullOrEmpty(whereStr))
            {
                if (!whereStr.Trim().ToLower().StartsWith("where")) whereStr = "where " + whereStr;

                AllSql = string.Format(@"
                    SELECT T.* FROM 
                    ( 
	                    {0}
                    ) T {1}
                    ", AllSql, whereStr);

            }
            try
            {
                reObj.Msg = AllSql;
                reObj.Data = DapperHelper.ExecuteBytesAsync(AllSql);
            }
            catch
            {
                return reObj;
            }
            return reObj;
        }


        /// <summary>
        /// 执行分页数据
        /// </summary>
        /// <param name="inEnt"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public async Task<Result<DataGridDataJson>> QueryPageExecute(QuerySearchModel inEnt)
        {
            Result<DataGridDataJson> reObj = new Result<DataGridDataJson>();
            DataGridDataJson reEnt = new DataGridDataJson();
            FaQueryEntity query = await dal.Single(i => i.CODE == inEnt.Code);
            if (query == null)
            {
                return reObj;
            }
            IList<QueryCfg> cfg = TypeChange.ToJsonObject<List<QueryCfg>>(query.QUERY_CFG_JSON);

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query, ref whereStr);
            if (string.IsNullOrWhiteSpace(inEnt.OrderStr)) inEnt.OrderStr = "(SELECT 0)";
            reObj.Msg = MakePageSql(AllSql, inEnt.page, inEnt.rows, inEnt.OrderStr, whereStr);
            try
            {
                var sqlList = reObj.Msg.Split(';');
                if (sqlList.Count() > 0)
                {
                    reEnt.rows = DapperHelper.GetDataTable(sqlList[0]);
                }

                if (sqlList.Count() > 1)
                {
                    int allNum = 0;
                    int.TryParse(await DapperHelper.ExecuteScalarAsync(sqlList[1]), out allNum);
                    reEnt.total = allNum;
                }
                reObj.Data = reEnt;
            }
            catch(Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(),"执行分页数据失败",e);
                return reObj;
            }
            return reObj;
        }

        /// <summary>
        /// 生成配置数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public async Task<Result<QueryCfg>> MakeQueryCfg(string code)
        {
            Result<QueryCfg> reObj = new Result<QueryCfg>();
            QuerySearchModel inEnt = new QuerySearchModel() { Code = code };
            List<QueryCfg> reEnt = new List<QueryCfg>();
            FaQueryEntity query = await dal.Single(i => i.CODE == inEnt.Code);

            if (query == null)
            {
                return reObj;
            }
            IList<QueryCfg> cfg = new List<QueryCfg>();
            if (!string.IsNullOrEmpty(query.QUERY_CFG_JSON))
            {
                cfg = TypeChange.ToJsonObject<List<QueryCfg>>(query.QUERY_CFG_JSON);
            }

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query, ref whereStr);
            reObj.Msg = MakePageSql(AllSql);
            try
            {

                DataTable dt = DapperHelper.GetDataTable(reObj.Msg);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    var t = dt.Columns[i];
                    var tmp = t.DataType.FullName.ToLower().Substring(t.DataType.FullName.IndexOf(".") + 1);
                    IList<string> numberList = new[] { "int", "decimal", "double", "int64", "int16" };
                    if (numberList.Contains(tmp))
                    {
                        tmp = "int";
                    }
                    string searchType = "";
                    string searchScript = null;
                    switch (tmp)
                    {
                        case "int":
                            searchScript = "";
                            searchType = "numberbox";
                            break;
                        case "datetime":
                            searchScript = "";
                            searchType = "datetimebox";
                            break;
                        default:
                            searchType = "text";
                            break;
                    }

                    reEnt.Add(new QueryCfg()
                    {
                        FieldName = t.ColumnName,
                        Show = true,
                        FieldType = t.DataType.FullName,
                        Width = "120",
                        CanSearch = true,
                        SearchType = searchType,
                        SearchScript = searchScript,
                        Sortable = true,
                        Alias = t.Caption,
                        IsVariable = "false"
                    });
                }
                #region 获取当前状态
                {
                    if (query != null)
                    {
                        IList<QueryCfg> old = TypeChange.ToJsonObject<List<QueryCfg>>(query.QUERY_CFG_JSON);
                        if (old != null)
                        {
                            for (int i = 0; i < reEnt.Count; i++)
                            {
                                var t0 = old.SingleOrDefault(x => x.FieldName == reEnt[i].FieldName);
                                if (t0 != null) reEnt[i] = t0;
                            }
                        }
                    }
                }
                #endregion
                reObj.DataList = reEnt;
                return reObj;
            }
            catch
            {
                return reObj;
            }
        }

        /// <summary>
        /// 生成分页的SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderStr"></param>
        /// <param name="whereStr"></param>
        /// <param name="fieldList"></param>
        /// <returns></returns>
        public string MakePageSql(string sql, int pageIndex = 1, int pageSize = 1, string orderStr = "1", string whereStr = null, IList<string> fieldList = null)
        {
            if (pageIndex < 0) pageIndex = 1;
            if (pageSize < 0) pageSize = 10;
            if (!string.IsNullOrWhiteSpace(whereStr) && !whereStr.Trim().ToLower().StartsWith("where")) whereStr = "where " + whereStr;
            if (string.IsNullOrWhiteSpace(orderStr)) orderStr = "1";
            orderStr = string.Format("ORDER BY {0}", orderStr);


            string withStr = "";
            string WithStartStr = "-----WithStart-----";
            string WithEndStr = "-----WithEnd-----";
            int WithStartP = sql.IndexOf(WithStartStr);
            int WithEndP = sql.IndexOf(WithEndStr);
            if (WithStartP > -1 && WithEndP > -1 && WithEndP > WithStartP)
            {
                withStr = sql.Substring(WithStartStr.Length, WithEndP - WithStartP - WithStartStr.Length);
                sql = sql.Substring(WithEndP + WithEndStr.Length);
            }


            string t1File = "*";
            string tFile = "*";
            if (fieldList != null)
            {
                t1File = string.Join(",T1.", fieldList);
                tFile = string.Join(",T.", fieldList);
            }
            string sqlStr = @"
{5}
SELECT T.{6} FROM 
( 
    {0}
) T {4} LIMIT {2},{3};
SELECT COUNT(1) ALL_NUM FROM ({0}) T {4}
";
            if (pageIndex == 0) pageIndex = 1;
            if (pageSize == 0) pageSize = 10;
            int startNum = (pageIndex - 1) * pageSize;
            sqlStr = string.Format(sqlStr, string.Format(sql, whereStr), orderStr, startNum, startNum + pageSize, whereStr, withStr, tFile, t1File);
            return sqlStr;
        }

        /// <summary>
        /// 根据Query的SQL 生成需要的SQL
        /// </summary>
        /// <param name="inEnt"></param>
        /// <param name="query"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public string MakeSql(QuerySearchModel inEnt, FaQueryEntity query, ref string whereStr)
        {

            if (inEnt.ParaList == null) inEnt.ParaList = new List<QueryPara>();
            if (inEnt.WhereList == null)
            {
                if (string.IsNullOrEmpty(inEnt.WhereListStr))
                {
                    inEnt.WhereList = new List<QueryRowBtnShowCondition>();
                }
                else
                {
                    inEnt.WhereList = TypeChange.ToJsonObject<List<QueryRowBtnShowCondition>>(inEnt.WhereListStr);
                }
            }

            if (inEnt.ParaList == null)
            {
                if (string.IsNullOrEmpty(inEnt.ParaListStr))
                {
                    inEnt.ParaList = new List<QueryPara>();
                }
                else
                {
                    inEnt.ParaList = TypeChange.ToJsonObject<List<QueryPara>>(inEnt.ParaListStr);
                }
            }


            //替换地址参数
            foreach (var tmp in inEnt.ParaList)
            {
                if (tmp.Value == "@(NOWDATA)")
                {
                    tmp.Value = DateTime.Today.ToString("yyyy-MM-dd");
                }
                query.QUERY_CONF = query.QUERY_CONF.Replace("@(" + tmp.ParaName + ")", tmp.Value);
            }

            //替换搜索的参数
            foreach (var tmp in inEnt.WhereList)
            {
                if (string.IsNullOrEmpty(tmp.ObjFiled)) tmp.ObjFiled = tmp.FieldName;
                if (string.IsNullOrEmpty(tmp.FieldName)) tmp.FieldName = tmp.ObjFiled;
                query.QUERY_CONF = query.QUERY_CONF.Replace("@(" + tmp.ObjFiled + ")", tmp.Value);
            }

            StringBuilder whereSb = new StringBuilder();
            foreach (var tmp in inEnt.WhereList.Where(x => x.OpType != null && !string.IsNullOrEmpty(x.OpType) && !string.IsNullOrEmpty(x.Value)))
            {
                if (tmp.FieldType == null) tmp.FieldType = "string";
                var nowType = tmp.FieldType.ToLower();
                int subIndex = tmp.FieldType.IndexOf(".");
                if (subIndex > -1)
                {
                    nowType = nowType.Substring(subIndex + 1);
                }
                switch (nowType)
                {
                    case "string":
                        switch (tmp.OpType)
                        {
                            case "in":
                                whereSb.Append(string.Format(" {0} {1} ('{2}') and ", tmp.ObjFiled, tmp.OpType, tmp.Value.Replace(",", "','")));
                                break;
                            default:
                                if (tmp.OpType == "like") tmp.Value = "%" + tmp.Value + "%";
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.ObjFiled, tmp.OpType, tmp.Value));
                                break;
                        }
                        break;
                    case "date":
                    case "datetime":
                        switch (tmp.OpType)
                        {
                            case "not between":
                            case "between":
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.ObjFiled, tmp.OpType, string.Join("' and '", tmp.Value.Split('~').ToList().Select(x => x.Trim()))));
                                break;
                            default:
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.ObjFiled, tmp.OpType, tmp.Value));
                                break;
                        }
                        break;
                    default:
                        if (tmp.OpType == "in")
                        {
                            whereSb.Append(string.Format(" {0} {1} ('{2}') and ", tmp.ObjFiled, tmp.OpType, tmp.Value.Replace(",", "','")));
                        }
                        else
                        {
                            whereSb.Append(string.Format(" {0} {1} {2} and ", tmp.ObjFiled, tmp.OpType, tmp.Value));
                        }
                        break;
                }
            }
            if (whereSb.Length > 4)
                whereSb = whereSb.Remove(whereSb.Length - 4, 4);
            whereStr = whereSb.ToString();
            return query.QUERY_CONF;
        }



        public async Task<int> Save(DtoSave<FaQueryEntity> inEnt)
        {
            if(inEnt.Data.ID==0){
                inEnt.Data.ID=await new SequenceRepository().GetNextID<FaQueryEntity>();
            }
            return await dal.Save(inEnt);
        }

        public async Task<List<FaQueryEntity>> FindAll(DtoSearch inSearch)
        {
            var reList = await dal.FindAll(inSearch);
            return reList.ToList();
        }

        public async Task<Result<FaQueryEntity>> FindAllPage(DtoSearch inSearch)
        {
            var reObj=new Result<FaQueryEntity>();
            var reList = await dal.FindAllS<FaQueryEntity,KV>(inSearch);
            reObj.DataList=reList.Item1.ToList();
            if(reList.Item2!=null && reList.Item2.Count()>0){
                reObj.Msg=reList.Item2.ToList()[0].V;
            }
            return reObj;
        }

        public async Task<int> Update(DtoSave<FaQueryEntity> inEnt)
        {
            return await dal.Update(inEnt);
        }

        public async Task<FaQueryEntity> Single(Expression<Func<FaQueryEntity, bool>> where)
        {
            var reEnt = await dal.Single(where);
            reEnt._DictStr = TypeChange.ObjectToStr(new ModelHelper<FaQueryEntity>(reEnt).GetDisplayDirct());
            reEnt._DictQueryCfgStr = TypeChange.ObjectToStr(new ModelHelper<QueryCfg>(new QueryCfg()).GetDisplayDirct());
            return reEnt;

        }

        public async Task<int> Delete(Expression<Func<FaQueryEntity, bool>> where)
        {
            return await dal.Delete(where);

        }

    }

}