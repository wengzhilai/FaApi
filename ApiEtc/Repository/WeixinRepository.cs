using ApiEtc.Config;
using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Repository
{
    public class WeixinRepository : IWeixin
    {
        DapperHelper<EtcWeixinEntity> dapper = new DapperHelper<EtcWeixinEntity>();

        public async Task<ResultObj<int>> save(EtcWeixinEntity inEnt)
        {
            var reObj = new ResultObj<int>();
            LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWeixin：请求{0}", TypeChange.ObjectToStr(inEnt)));
            if (string.IsNullOrEmpty(inEnt.openid))
            {
                reObj.success = false;
                LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWeixin：返回{0}", TypeChange.ObjectToStr(reObj)));
                return reObj;
            }
            var ent = await dapper.Single(x => x.openid == inEnt.openid);
            if (ent == null)
            {
                inEnt.createTime = DateTime.Now;
                await dapper.Save(new DtoSave<EtcWeixinEntity>
                {
                    data = inEnt
                });
            }
            else
            {
                var upEnt = new DtoSave<EtcWeixinEntity>()
                {
                    data = inEnt,
                    whereList = new List<string> { "openid" },
                    saveFieldList = new List<string>()
                };
                if (!string.IsNullOrEmpty(inEnt.ticket))
                {
                    upEnt.saveFieldList.Add("ticket");
                }
                if (!string.IsNullOrEmpty(inEnt.parentTicket))
                {
                    upEnt.saveFieldList.Add("parentTicket");
                }

                if (upEnt.saveFieldList.Count > 0)
                {
                    await dapper.Update(upEnt);
                }
            }
            LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWeixin：返回{0}", TypeChange.ObjectToStr(reObj)));
            return reObj;
        }
    }
}
