using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper.Query.Dto;
using Models;

namespace Helper.Query
{
    public class QueryRepository : IQuery
    {
        private DapperHelper<QueryEntity> dal = new DapperHelper<QueryEntity>();



        public async Task<ResultObj<Dictionary<string, object>>> getListData(SearchDto inEnt)
        {

            ResultObj<Dictionary<string, object>> reObj = new ResultObj<Dictionary<string, object>>();
            if (string.IsNullOrEmpty(inEnt.code)) return reObj;
            inEnt.code = inEnt.code.Trim().Replace("#", "");
            QueryEntity query = await dal.Single(i => i.code == inEnt.code);
            if (query == null)
            {
                return reObj;
            }

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query.queryConf, ref whereStr);
            string orderStr = string.Format("{0} {1}", inEnt.sort, inEnt.order);
            if (string.IsNullOrWhiteSpace(orderStr)) orderStr = "(SELECT 0)";
            reObj.msg = MakePageSql(AllSql, inEnt.page, inEnt.rows, orderStr, whereStr);
            try
            {
                var sqlList = reObj.msg.Split(';');
                if (sqlList.Count() > 0)
                {
                    reObj.dataList = dal.Query(sqlList[0]);
                }

                if (sqlList.Count() > 1)
                {
                    int allNum = 0;
                    int.TryParse(await dal.ExecuteScalarAsync(sqlList[1]), out allNum);
                    reObj.total = allNum;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), "执行分页数据失败", e);
                return reObj;
            }
            return reObj;
        }



        public async Task<ResultObj<Dictionary<string, Dictionary<string, object>>>> makeQueryCfg(DtoKey inObj)
        {
            var reObj = new ResultObj<Dictionary<string, Dictionary<string, object>>>();
            reObj.data = new Dictionary<string, Dictionary<string, object>>();
            SearchDto inEnt = new SearchDto() { code = inObj.Key };
            QueryEntity query = await dal.Single(i => i.code == inEnt.code);

            if (query == null)
            {
                return reObj;
            }

            string whereStr = "";
            string AllSql = MakeSql(inEnt, query.queryConf, ref whereStr);
            reObj.msg = AllSql;
            try
            {

                System.Data.DataTable dt = new DapperHelper().GetDataTable(reObj.msg);
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
                    switch (tmp)
                    {
                        case "int":
                            searchType = "numberbox";
                            break;
                        case "datetime":
                            searchType = "datetimebox";
                            break;
                        default:
                            searchType = "text";
                            break;
                    }

                    var item = new Dictionary<string, object>();
                    item.Add("title", t.Caption);
                    item.Add("type", searchType);

                    reObj.data.Add(t.ColumnName, item);

                }
                #region 获取当前配置

                Dictionary<string, Dictionary<string, object>> cfg = new Dictionary<string, Dictionary<string, object>>();
                if (!string.IsNullOrEmpty(query.queryCfgJson))
                {
                    cfg = TypeChange.ToJsonObject<Dictionary<string, Dictionary<string, object>>>(query.queryCfgJson);
                }

                if (cfg != null)
                {
                    foreach (var item in reObj.data)
                    {
                        if (cfg[item.Key] != null)
                        {
                            reObj.data[item.Key] = cfg[item.Key];
                        }
                    }
                }
                #endregion
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<int>> save(DtoSave<QueryEntity> inObj)
        {
            var resultObj = new ResultObj<int>();

            try
            {
                int opNum = 0;
                if (inObj.data.id == 0)
                {
                    opNum = await dal.Save(inObj);
                }
                else
                {
                    opNum = await dal.Update(inObj);
                }
            }
            catch (Exception e)
            {
                resultObj.success = false;
                resultObj.msg = e.Message;
            }
            return resultObj;
        }

        public async Task<ResultObj<QueryEntity>> getSingleQuery(DtoKey inObj)
        {
            var resultObj = new ResultObj<QueryEntity>();
            try
            {
                resultObj.data = await dal.Single(x=>x.code==inObj.Key);
                resultObj.success = true;
            }
            catch (Exception e)
            {
                resultObj.success = false;
                resultObj.msg = e.Message;
            }
            return resultObj;
        }

        public async Task<ResultObj<QueryEntity>> singleByKey(DtoDo<int> inObj)
        {
            var resultObj = new ResultObj<QueryEntity>();
            try
            {
                resultObj.data = await dal.SingleByKey(inObj.Key);
                resultObj.success = true;
            }
            catch (Exception e)
            {
                resultObj.success = false;
                resultObj.msg = e.Message;
            }
            return resultObj;
        }

        public async Task<ResultObj<int>> delete(DtoDo<int> inObj)
        {
            var resultObj = new ResultObj<int>();
            try
            {
                resultObj.data = await dal.Delete(x => x.id == inObj.Key);
                resultObj.success = resultObj.data > 0;
            }
            catch (Exception e)
            {
                resultObj.success = false;
                resultObj.msg = e.Message;
            }
            return resultObj;
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
) T {4} {1} LIMIT {2},{3};
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
        /// <param name="querySql"></param>
        /// <param name="whereStr"></param>
        /// <returns></returns>
        public string MakeSql(SearchDto inEnt, string querySql, ref string whereStr)
        {

            if (inEnt.whereList == null)
            {
                if (string.IsNullOrEmpty(inEnt.whereListStr))
                {
                    inEnt.whereList = new List<SearchWhereDto>();
                }
                else
                {
                    inEnt.whereList = TypeChange.ToJsonObject<List<SearchWhereDto>>(inEnt.whereListStr);
                }
            }



            //替换搜索的参数
            foreach (var tmp in inEnt.whereList)
            {
                querySql = querySql.Replace("@(" + tmp.objFiled + ")", tmp.value);
            }

            StringBuilder whereSb = new StringBuilder();
            foreach (var tmp in inEnt.whereList.Where(x => x.opType != null && !string.IsNullOrEmpty(x.opType) && !string.IsNullOrEmpty(x.value)))
            {
                if (tmp.fieldType == null) tmp.fieldType = "string";
                var nowType = tmp.fieldType.ToLower();
                int subIndex = tmp.fieldType.IndexOf(".");
                if (subIndex > -1)
                {
                    nowType = nowType.Substring(subIndex + 1);
                }
                switch (nowType)
                {
                    case "text":
                    case "string":
                        switch (tmp.opType)
                        {
                            case "in":
                                whereSb.Append(string.Format(" {0} {1} ('{2}') and ", tmp.objFiled, tmp.opType, tmp.value.Replace(",", "','")));
                                break;
                            default:
                                if (tmp.opType == "like") tmp.value = "%" + tmp.value + "%";
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.objFiled, tmp.opType, tmp.value));
                                break;
                        }
                        break;
                    case "date":
                    case "datetimebox":
                    case "datetime":
                        switch (tmp.opType)
                        {
                            case "not between":
                            case "between":
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.objFiled, tmp.opType, string.Join("' and '", tmp.value.Split('~').ToList().Select(x => x.Trim()))));
                                break;
                            default:
                                whereSb.Append(string.Format(" {0} {1} '{2}' and ", tmp.objFiled, tmp.opType, tmp.value));
                                break;
                        }
                        break;
                    default:
                        if (tmp.opType == "in")
                        {
                            whereSb.Append(string.Format(" {0} {1} ('{2}') and ", tmp.objFiled, tmp.opType, tmp.value.Replace(",", "','")));
                        }
                        else
                        {
                            whereSb.Append(string.Format(" {0} {1} {2} and ", tmp.objFiled, tmp.opType, tmp.value));
                        }
                        break;
                }
            }
            if (whereSb.Length > 4)
                whereSb = whereSb.Remove(whereSb.Length - 4, 4);
            whereStr = whereSb.ToString();
            return querySql;
        }
    }
}
