
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Helper.Query;
using Helper.Query.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Controllers
{
    /// <summary>
    /// 员工接口
    /// </summary>
    [Route("[controller]/[action]")]
    [EnableCors("AllowSameDomain")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        Helper.Query.IQuery dal;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public QueryController(Helper.Query.IQuery dal)
        {
            this.dal = dal;
        }

        /// <summary>
        /// 删除查询
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> delete(DtoDo<int> inObj)
        {
            return dal.delete(inObj);
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<Dictionary<string, object>>> getListData(SearchDto inObj)
        {
            return dal.getListData(inObj);
        }
        /// <summary>
        /// 获取单个Query
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<QueryEntity>> getSingleQuery(DtoKey inObj)
        {
            return dal.getSingleQuery(inObj);
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<Dictionary<string, Dictionary<string, object>>>> makeQueryCfg(DtoKey inObj)
        {
            return dal.makeQueryCfg(inObj);
        }

        /// <summary>
        /// 保存查询
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> save(DtoSave<QueryEntity> inObj)
        {
            return dal.save(inObj);
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<QueryEntity>> singleByKey(DtoDo<int> inObj)
        {
            return dal.singleByKey(inObj);
        }

        [HttpGet]
        public async Task<IActionResult> exportCsv(string code)
        {
            var tmepObj = await dal.exportCsv(new SearchDto
            {
                code = code,
                page = 1,
                rows = 10000
            });
            return File(tmepObj.data.ToArray(), "application/octet-stream", string.Format("{0}.csv", code));
        }
    }
}