using ApiEtc.Models.Entity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Controllers.Interface
{
    public interface IWeixin
    {
        /// <summary>
        /// 更新微信用户
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<ResultObj<int>> save(EtcWeixinEntity inEnt);

    }
}
