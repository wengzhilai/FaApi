
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
    /// 提现要接口
    /// </summary>
    [Route("[controller]/[action]")]
    [EnableCors("AllowSameDomain")]
    [ApiController]
    public class WalletController : ControllerBase, IWalletController
    {
        /// <summary>
        /// 获取钱包列表
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcWalletEntity>> list(WalletListDto inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 查询钱包
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcWalletEntity>> singleByKey(DtoDo<int> inEnt)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> submitWallet(SubmitWalletDto inObj)
        {
            throw new System.NotImplementedException();
        }
    }
}