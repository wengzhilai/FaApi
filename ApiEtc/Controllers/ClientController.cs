
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
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
    public class ClientController : ControllerBase, IClientController
    {
        /// <summary>
        /// 推广汇总
        /// </summary>
        /// <param name="inOby"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<ClientReportResult>> clientReport(DtoKey inOby)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取推广明细
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcClentEntity>> list(ClientListDto inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Etc申请
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> regClient(RegClientDto inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 后台添加客户资料
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> save(DtoSave<EtcClentEntity> inEnt)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcClentEntity>> singleByKey(DtoDo<int> inEnt)
        {
            throw new System.NotImplementedException();
        }
    }
}