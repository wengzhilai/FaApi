using ApiEtc.Models.Entity;
using Models;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IStaffController
    {
        /// <summary>
        /// 检测用户是否绑定，key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        Task<Result> checkIsBind(DtoKey inObj);

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
        /// 查询员工
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<EtcStaffEntity>> singleByKey(DtoDo<int> inEnt);

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
}
