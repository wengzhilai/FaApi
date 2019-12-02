
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
    /// 员工接口
    /// </summary>
    [Route("[controller]/[action]")]
    [EnableCors("AllowSameDomain")]
    [ApiController]
    public class StaffController : ControllerBase, IStaffController
    {
        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> bindUser(BindUserDto inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 检测用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> checkIsBind(DtoKey inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取员工的信息，包括二维码地址，Key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcStaffEntity>> getStaff(DtoKey inObj)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcStaffEntity>> singleByKey(DtoDo<int> inEnt)
        {
            throw new System.NotImplementedException();
        }
    }
}