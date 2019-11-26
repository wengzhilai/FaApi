using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Comon;
using IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Models.Entity;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Models.EntityView;

namespace WebApi.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private IQueryRepository _query;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        public QueryController(
            IQueryRepository query
            )
        {
            this._query = query;
        }
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<DataGridDataJson>> GetBindListData(QuerySearchModel querySearchModel)
        {
            Result<DataGridDataJson> reObj = new Result<DataGridDataJson>();
            try
            {
                reObj =await GeListData(querySearchModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
        /// <summary>
        /// 只获取列表数据,用于其它地方，需要获取查询的数据，比如用在二级联动
        /// </summary>
        /// <param name="querySearchModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<DataGridDataJson>> GetOnlyListData(QuerySearchModel querySearchModel)
        {
            Result<DataGridDataJson> reObj = new Result<DataGridDataJson>();
            try
            {
                reObj =await GeListData(querySearchModel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 获取数据对象
        /// </summary>
        /// <param name="querySearchModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<DataGridDataJson>> GeListData(QuerySearchModel querySearchModel)
        {
            Result<DataGridDataJson> reObj = new Result<DataGridDataJson>();
            try
            {
                if (querySearchModel.WhereList == null && !string.IsNullOrWhiteSpace(querySearchModel.WhereListStr))
                {
                    querySearchModel.WhereList = TypeChange.ToJsonObject<List<QueryRowBtnShowCondition>>(querySearchModel.WhereListStr);
                }

                if (querySearchModel.ParaList == null && !string.IsNullOrWhiteSpace(querySearchModel.ParaListStr))
                {
                    querySearchModel.ParaList = TypeChange.ToJsonObject<List<QueryPara>>(querySearchModel.ParaListStr);
                }

                //添加AdministratorModel对象加入到参数列表中,以方便在Query里进行过滤
                if (querySearchModel.ParaList == null) querySearchModel.ParaList = new List<QueryPara>();


                List<QueryPara> QueryPara = new List<QueryPara>();

                if (querySearchModel.ParaList == null) querySearchModel.ParaList = new List<QueryPara>();
                querySearchModel.ParaList.AddRange(QueryPara);

                reObj = await _query.QueryPageExecute(querySearchModel);
                // reObj.Msg="";
                // Session[string.Format("SQL_{0}", querySearchModel.Code)] = sqlStr;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.success = false;
                reObj.msg = ex.Message;
            }
            return reObj;

        }

        /// <summary>
        /// 获取Query对象
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<Result<FaQueryEntity>> GetSingleQuery(DtoKey inEnt)
        {
            Result<FaQueryEntity> reObj = new Result<FaQueryEntity>();
            try
            {
                reObj.data = await _query.Single(i => i.CODE == inEnt.Key);
                reObj.success = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
        /// <summary>
        /// 获取执行的SQL
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Result<string> GetDubug(string code)
        {
            Result<string> reObj = new Result<string>();
            try
            {
                var reStr = "";
                try
                {
                    // reStr = Session[string.Format("SQL_{0}", code)].ToString();
                    reStr = reStr.Replace("%0a", "\r\n");
                    reStr = reStr.Replace("%0d", " ");
                }
                catch { }
                reObj.data = reStr;
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
        /// <summary>
        /// 导出Excel数据
        /// </summary>
        /// <param name="querySearchModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DownFile(QuerySearchModel querySearchModel)
        {
            querySearchModel.page = 1;
            querySearchModel.rows = 1000000000;
            // var reData =await _query.QueryExecuteCsv(querySearchModel);
            // Session[string.Format("SQL_{0}", querySearchModel.Code)] = sqlStr;
            var tmepObj = await _query.QueryExecuteCsv(querySearchModel);
            return File(tmepObj.data.ToArray(), "application/octet-stream", string.Format("{0}.csv", querySearchModel.Code));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DownFile(string code)
        {
            var tmepObj = await _query.QueryExecuteCsv(new QuerySearchModel{
                Code=code,
                page=1,
                rows=10000
            });
            return File(tmepObj.data.ToArray(), "application/octet-stream", string.Format("{0}.csv", code));
        }

        /// <summary>
        /// 生成配置数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<QueryCfg>> MakeQueryCfg(string code)
        {
            Result<QueryCfg> reObj = new Result<QueryCfg>();
            try
            {
                reObj = await _query.MakeQueryCfg(code);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 获取所有查询列表
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<FaQueryEntity>> List(DtoSearch inEnt)
        {
            Result<FaQueryEntity> reObj = new Result<FaQueryEntity>();
            inEnt.OrderType = "id asc";
            inEnt.IgnoreFieldList=new List<string>{
                "QUERY_CONF",
                "QUERY_CFG_JSON",
                "IN_PARA_JSON",
                "JS_STR",
                "ROWS_BTN",
                "HEARD_BTN",
                "CHARTS_CFG",
                "REPORT_SCRIPT",
                };
            reObj = await _query.FindAllPage(inEnt);
            reObj.success = true;
            return reObj;
        }

        /// <summary>
        /// 保存Query
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<int>> Save(DtoSave<FaQueryEntity> inEnt)
        {
            Result<int> reObj = new Result<int>();
            try
            {
                if (inEnt.Data.ID == 0)
                {
                    reObj.data = await _query.Save(inEnt);
                }
                else
                {
                    reObj.data = await _query.Update(inEnt);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 查找单条
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<FaQueryEntity>> Single(DtoDo<int> inEnt)
        {
            Result<FaQueryEntity> reObj = new Result<FaQueryEntity>();
            try
            {
                reObj.data = await _query.Single(x=>x.ID==inEnt.Key);
            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }

        /// <summary>
        /// 删除单条
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<int>> Delete(DtoDo<int> inEnt)
        {
            Result<int> reObj = new Result<int>();
            try
            {
                reObj.data = await _query.Delete(x=>x.ID==inEnt.Key);

            }
            catch (Exception ex)
            {
                LogHelper.WriteErrorLog(typeof(QueryController), ex.ToString());
                reObj.msg = ex.Message;
                reObj.success = false;
            }
            return reObj;
        }
    }
}