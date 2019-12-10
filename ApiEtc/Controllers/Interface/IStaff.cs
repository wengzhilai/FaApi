using ApiEtc.Models.Entity;
using Models;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IStaff
    {

        /// <summary>
        /// 注册新用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<bool>> regStaff(RegStaffDto inObj);
        /// <summary>
        /// 检测用户是否绑定，key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<bool>> checkIsBind(DtoKey inObj);

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<Result> bindUser(BindUserDto inObj);

        /// <summary>
        /// 获取员工的信息，包括二维码地址，Key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> getStaff(DtoKey inObj);

        /// <summary>
        /// 使用票据 获取员工的信息
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> getStaffByTicket(DtoKey inObj);

        /// <summary>
        /// 查询员工
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> singleByKey(DtoDo<int> inEnt);

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> getStaffList();


        /// <summary>
        /// 更新用户的ticket
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> updateTicket(EtcStaffEntity inObj);

        /// <summary>
        /// 后台修改用户资料
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(DtoSave<EtcStaffEntity> inEnt);

    }

    public class BindUserDto: DtoKey
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

    public class RegStaffDto : DtoKey
    {

        /// <summary>
        /// 获取的二维码ticket
        /// </summary>
        public string ticket { get; set; }

        /// <summary>
        /// 上级Ticket
        /// </summary>
        public string parentTicket { get; set; }

    }
}
