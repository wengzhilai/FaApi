using ApiEtc.Models.Entity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IApplicants
    {
        /// <summary>
        /// 提交申请人
        /// </summary>
        /// <returns></returns>
        Task<Result> addApplicants(AddApplicantsDto inObj);

        /// <summary>
        /// 已经联系
        /// </summary>
        /// <returns></returns>
        Task<Result> IsContact(DtoKey dtoKey,string opName);

        /// <summary>
        /// 修改申请内容
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(DtoSave<EtcApplicantsEntity> inEnt);

    }

    public class AddApplicantsDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public String name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public String phone { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public String city { get; set; }
    }
}
