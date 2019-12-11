using ApiEtc.Models.Entity;
using Helper.Query.Dto;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IWallet
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
        Task<ResultObj<Dictionary<string, object>>> list(WalletListDto inObj);

        /// <summary>
        /// 查询钱包
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<EtcWalletEntity>> singleByKey(DtoDo<int> inEnt);

        /// <summary>
        /// 审核资料
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(DtoSave<EtcWalletEntity> inEnt);

    }

    public class SubmitWalletDto: DtoKey
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 提现总金额
        /// </summary>
        public decimal allMoney { get; set; }
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


    public class WalletListDto: SearchDto
    {
        /// <summary>
        /// openId
        /// </summary>
        public string Key { get; set; }
    }

}
