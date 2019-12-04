
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Controllers
{
    /// <summary>
    /// 客户管理接口
    /// </summary>
    [Route("[controller]/[action]")]
    [EnableCors("AllowSameDomain")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        IClient dal;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dal"></param>
        public ClientController(IClient dal)
        {
            this.dal = dal;
        }


        /// <summary>
        /// 推广汇总
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<ClientReportResult>> clientReport(DtoKey inObj)
        {
            return dal.clientReport(inObj);
        }

        /// <summary>
        /// 获取推广明细
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<Dictionary<String, Object>>> list(ClientListDto inObj)
        {
            return dal.list(inObj);
        }

        /// <summary>
        /// Etc申请
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> regClient(RegClientDto inObj)
        {
            return dal.regClient(inObj);
        }

        /// <summary>
        /// 后台添加客户资料
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> save(DtoSave<EtcClientEntity> inObj)
        {
            return dal.save(inObj);
        }

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcClientEntity>> singleByKey(DtoDo<int> inObj)
        {
            return dal.singleByKey(inObj);
        }
    }
}