
using Helper;
using Helper.WeiChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.IO;
using System.Text;
using Helper.WeiChat.Entities;
using ApiEtc.Config;
using System.Collections.Generic;
using ApiEtc.Controllers.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using ApiEtc.Models.Entity;

namespace ApiEtc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private IApplicants dal;
        public ApplicantsController(IApplicants _dal)
        {
            this.dal = _dal;
        }



        /// <summary>
        /// 提交申请人
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Result> AddApplicants(AddApplicantsDto inObj)
        {
            return dal.addApplicants(inObj);
        }


        /// <summary>
        /// 已经联系
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        [HttpGet]
        public Task<Result> IsContact(string key)
        {
            DtoKey inObj = new DtoKey()
            {
                Key = key
            };
            return dal.IsContact(inObj,"");

        }

        /// <summary>
        /// 保存资料
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultObj<int>> save(DtoSave<EtcApplicantsEntity> inEnt)
        {
            return dal.save(inEnt);

        }
    }
}