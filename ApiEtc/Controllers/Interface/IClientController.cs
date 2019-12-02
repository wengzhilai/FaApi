using ApiEtc.Models.Entity;
using Models;
using Models.Entity;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IClientController
    {
        /// <summary>
        /// Etc申请
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<Result> regClient(RegClientDto inObj);

        /// <summary>
        /// 推广汇总
        /// </summary>
        /// <param name="inOby"></param>
        /// <returns></returns>
        Task<ResultObj<ClientReportResult>> clientReport(DtoKey inOby);

        /// <summary>
        /// 获取推广明细
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<EtcClentEntity>> list(ClientListDto inObj);

        /// <summary>
        /// 后台添加客户资料
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(DtoSave<EtcClentEntity> inEnt);
        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>

        Task<ResultObj<EtcClentEntity>> singleByKey(DtoDo<int> inEnt);
    }

    public class ClientReportResult
    {
        /// <summary>
        /// 推广成功总人数
        /// </summary>
        public int allNum { get; set; }

        /// <summary>
        /// 已结算数
        /// </summary>
        public int paidNum { get; set; }

        /// <summary>
        /// 未结算数
        /// </summary>
        public int noPaidNum { get; set; }

        /// <summary>
        /// 总收入
        /// </summary>
        public decimal allMoney { get; set; }

        /// <summary>
        /// 已提现
        /// </summary>
        public decimal paidMoney { get; set; }

        /// <summary>
        /// 待结算
        /// </summary>
        public decimal noPaidMoney { get; set; }
    }

    public class RegClientDto: DtoKey
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string phone { get; set; }
    }

    public class ClientListDto : QuerySearchDto
    {
        /// <summary>
        /// openId
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 是否结算，0表示查看所有，1表示已经结算，2表示等结算
        /// </summary>
        public int payType { get; set; } 
    }
}
