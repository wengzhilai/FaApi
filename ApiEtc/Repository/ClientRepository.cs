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
    /// <summary>
    /// 客户
    /// </summary>
    public class ClientRepository : IClient
    {
        DapperHelper<EtcClientEntity> dapper = new DapperHelper<EtcClientEntity>();

        IStaff staffDal;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        public ClientRepository(IStaff dal)
        {
            this.staffDal = dal;
        }


        /// <summary>
        /// 推广汇总
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public async Task<ResultObj<ClientReportResult>> clientReport(DtoKey inObj)
        {
            var reObj = new ResultObj<ClientReportResult>();
            try
            {
                var sql = @"
select 
	(select count(1) from etc_client where StaffId=a.Id AND Status!='已结算') allNum,
	(select count(1) from etc_client where StaffId=a.Id AND Status='已安装激活') paidNum,
	(select count(1) from etc_client where StaffId=a.Id AND Status!='已安装激活' AND Status!='已结算') noPaidNum,
	(select SUM(Money) from etc_client where StaffId=a.Id and Status!='已结算') allMoney, 
	(select SUM(Money) from etc_client where StaffId=a.Id AND Status='已安装激活') paidMoney,
	(select SUM(Money) from etc_client where StaffId=a.Id AND Status!='已安装激活' AND Status!='已结算') noPaidMoney 
from etc_staff a where OpenId='{0}'
";
                sql = string.Format(sql, inObj.Key);
                reObj.dataList=dapper.Query<ClientReportResult>(sql).ToList();
                if (reObj.dataList.Count() > 0) reObj.data = reObj.dataList[0];
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<EtcClientEntity>> list(ClientListDto inObj)
        {
            var reObj = new ResultObj<EtcClientEntity>();
            try
            {

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// Etc申请
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        public async Task<Result> regClient(RegClientDto inObj)
        {
            var reObj = new Result();
            try
            {
                if(string.IsNullOrEmpty(inObj.name) || string.IsNullOrEmpty(inObj.phone) || string.IsNullOrEmpty(inObj.Key))
                {
                    reObj.success = false;
                    reObj.msg = "输入有误";
                    return reObj;
                }
                var staff = (await staffDal.getStaff(inObj)).data;
                if(staff==null || staff.id == 0)
                {
                    reObj.success = false;
                    reObj.msg = "获取员工资料有误";
                    return reObj;
                }
                var client =await dapper.Single(x => x.clientPhone == inObj.phone);
                if (client == null)
                {
                    client = new EtcClientEntity
                    {
                        staffId = staff.id,
                        clientPhone = inObj.phone,
                        clientName = inObj.name,
                        bindTime = DateTime.Now,
                        money = AppConfig.WebConfig.ClientPrice,
                        status = "已绑定"
                    };
                    client.id = await dapper.Save(new DtoSave<EtcClientEntity> {
                        data = client,
                        ignoreFieldList = null
                    });
                }
                else if(client.staffId== staff.id && client.status == "已绑定")
                {
                    client.clientPhone = inObj.phone;
                    client.clientName = inObj.name;
                    client.bindTime = DateTime.Now;
                    client.money = AppConfig.WebConfig.ClientPrice;
                    
                    await dapper.Update(new DtoSave<EtcClientEntity>
                    {
                        data = client,
                        saveFieldList=new List<string> { "clientPhone", "clientName", "bindTime", "money" },
                        whereList=new List<string> { "id" }
                    });
                }
                else
                {
                    reObj.success = false;
                    reObj.msg = "该用户已经绑定";
                    return reObj;
                }

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<int>> save(DtoSave<EtcClientEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                var client = await dapper.SingleByKey(inEnt.data.id);
                if (client == null)
                {
                    reObj.success = false;
                    reObj.msg = "Id有误";
                    return reObj;
                }

                if (client.status.Equals("已结算"))
                {
                    reObj.success = false;
                    reObj.msg = "该用户已经结算，不可修改";
                    return reObj;
                }

                inEnt.saveFieldList = new List<string> { "remark", "carNum", "carType", "submitTime", "status" };
                inEnt.ignoreFieldList = null;
                inEnt.whereList = new List<string> { "id" };
                inEnt.data.opuserName = inEnt.token;
                var opNum = await dapper.Update(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        public async Task<ResultObj<EtcClientEntity>> singleByKey(DtoDo<int> inEnt)
        {
            var reObj = new ResultObj<EtcClientEntity>();
            try
            {
                reObj.data =await dapper.SingleByKey(inEnt.Key);
                reObj.success = true;
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }
    }
}
