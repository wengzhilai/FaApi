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
    public class ApplicantsRepository : IApplicants
    {
        DapperHelper<EtcApplicantsEntity> dapper = new DapperHelper<EtcApplicantsEntity>();

        public async Task<Result> addApplicants(AddApplicantsDto inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                LogHelper.WriteDebugLog(this.GetType(), string.Format("addApplicants：请求{0}", TypeChange.ObjectToStr(inEnt)));
                var client = await dapper.Single(x=>x.phone== inEnt.phone);
                if (client == null)
                {
                    reObj.data=await dapper.Save(new DtoSave<EtcApplicantsEntity>
                    {
                        data = new EtcApplicantsEntity { phone = inEnt.phone, name = inEnt.name, city = inEnt.city, createTime=DateTime.Now },
                        saveFieldList=null
                    });
                }
                else
                {
                    reObj.data = await dapper.Update(new DtoSave<EtcApplicantsEntity>
                    {
                        data = new EtcApplicantsEntity { phone = inEnt.phone, name = inEnt.name, city = inEnt.city, id = client.id,status="未处理", createTime=DateTime.Now },
                        saveFieldListExp = x => new string[] { x.city, x.name },
                        whereListExp = x => new string[] { x.phone }
                    });
                }

                reObj.success = reObj.data > 0;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            LogHelper.WriteDebugLog(this.GetType(), string.Format("addApplicants：返回{0}", TypeChange.ObjectToStr(reObj)));
            return reObj;
        }

        public async Task<Result> IsContact(DtoKey inEnt, string opName)
        {
            var reObj = new Result();
            try
            {
                LogHelper.WriteDebugLog(this.GetType(), string.Format("IsContact：请求{0}", TypeChange.ObjectToStr(inEnt)));
                int key = Convert.ToInt32(inEnt.Key);
                var client = await dapper.Single(x => x.id == key);
                if (client == null)
                {
                    reObj.success = false;
                    reObj.msg = "ID有误";
                }
                else
                {
                    reObj.success = await dapper.Update(new DtoSave<EtcApplicantsEntity>
                    {
                        data = new EtcApplicantsEntity {
                            id = client.id,
                            status = "已联系",
                            opTime = DateTime.Now,
                            opUser = opName
                        },
                        saveFieldListExp = x => new object[] { x.status, x.opTime,x.opUser },
                        whereListExp = x => new object[] { x.id }
                    })>0;
                }

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            LogHelper.WriteDebugLog(this.GetType(), string.Format("IsContact：返回{0}", TypeChange.ObjectToStr(reObj)));
            return reObj;
        }

        public async Task<ResultObj<int>> save(DtoSave<EtcApplicantsEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            reObj.data = await dapper.Update(inEnt);
            reObj.success = reObj.data > 0;
            return reObj;
        }
    }
}
