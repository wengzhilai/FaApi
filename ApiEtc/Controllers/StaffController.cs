
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
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
    public class StaffController : ControllerBase
    {
        IStaff dal;

        public StaffController(IStaff dal)
        {
            this.dal = dal;
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> bindUser(BindUserDto inObj)
        {
            return dal.bindUser(inObj);
        }

        /// <summary>
        /// 检测用户,data为true表示，存在
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<bool>> checkIsBind(DtoKey inObj)
        {
            return dal.checkIsBind(inObj);
        }

        /// <summary>
        /// 获取员工的信息，包括二维码地址，Key为OpenId
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcStaffEntity>> getStaff(DtoKey inObj)
        {
            return dal.getStaff(inObj);
        }

        /// <summary>
        /// 获取员工信息
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<EtcStaffEntity>> singleByKey(DtoDo<int> inObj)
        {
            return dal.singleByKey(inObj);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> save(DtoSave<EtcStaffEntity> inEnt)
        {
            return dal.save(inEnt);
        }
    }
}