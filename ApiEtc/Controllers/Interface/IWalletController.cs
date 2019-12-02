using ApiEtc.Models.Entity;
using Models;
using Models.Entity;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IWalletController
    {
        /// <summary>
        /// 提交申请提现
        /// </summary>
        /// <returns></returns>
        Task<Result> submitWallet(SubmitWalletDto inObj);

        /// <summary>
        /// 获取钱包列表
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<EtcWalletEntity>> list(WalletListDto inObj);

        Task<ResultObj<EtcWalletEntity>> singleByKey(DtoDo<int> inEnt);

    }

    public class SubmitWalletDto: DtoKey
    {
        /// <summary>
        /// 提现总金额
        /// </summary>
        public decimal AllMoney { get; set; }
        /// <summary>
        /// 提现客户数
        /// </summary>
        public int clientNum { get; set; }

        /// <summary>
        /// 提现方式 :1WeiChat\2AliPay
        /// </summary>
        public int walletAccountType { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string walletaAcount { get; set; }

        /// <summary>
        /// 账号名
        /// </summary>
        public string walletAccountName { get; set; }
    }


    public class WalletListDto: QuerySearchDto
    {
        /// <summary>
        /// openId
        /// </summary>
        public string Key { get; set; }
    }

}
