
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
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
    public class WalletController : ControllerBase, IWallet
    {
        IWallet dal;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public WalletController(IWallet dal)
        {
            this.dal = dal;
        }

        /// <summary>
        /// 获取钱包列表
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<Dictionary<string, object>>> list(WalletListDto inObj)
        {

            return dal.list(inObj);
        }

        /// <summary>
        /// 查询钱包
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcWalletEntity>> singleByKey(DtoDo<int> inObj)
        {
            return dal.singleByKey(inObj);
        }

        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> submitWallet(SubmitWalletDto inObj)
        {
            return dal.submitWallet(inObj);
        }
    }
}