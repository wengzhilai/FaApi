using ApiEtc.Controllers.Interface;
using ApiEtc.Models.Entity;
using Helper;
using Helper.Query;
using Helper.Query.Dto;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEtc.Repository
{
    public class WalletRepository : IWallet
    {
        DapperHelper<EtcWalletEntity> dapper = new DapperHelper<EtcWalletEntity>();
        DapperHelper<EtcClientEntity> dapperClient = new DapperHelper<EtcClientEntity>();


        IStaff staffDal;
        IClient clientDal;
        IQuery queryDal;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dal"></param>
        /// <param name="clientDal"></param>
        public WalletRepository(IStaff dal, IClient clientDal, IQuery queryDal)
        {
            this.staffDal = dal;
            this.clientDal = clientDal;
            this.queryDal = queryDal;
        }

        public Task<ResultObj<Dictionary<String, Object>>> list(WalletListDto inObj)
        {
            inObj.code = "wallet";

            if (inObj.whereList == null) inObj.whereList = new List<SearchWhereDto>();
            inObj.whereList.Add(new SearchWhereDto
            {
                fieldType = "string",
                opType = "=",
                value = inObj.Key,
                objFiled = "StaffOpenId"
            });

            return queryDal.getListData(inObj);
        }

        public async Task<ResultObj<EtcWalletEntity>> singleByKey(DtoDo<int> inEnt)
        {
            var reObj = new ResultObj<EtcWalletEntity>();
            try
            {
                var staff = await dapper.SingleByKey(inEnt.Key);
                reObj.data = staff;
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

        public async Task<Result> submitWallet(SubmitWalletDto inObj)
        {
            var reObj = new Result();
            try
            {
                LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：请求{0}", TypeChange.ObjectToStr(inObj)));
                if (inObj.clientNum < 1)
                {
                    reObj.success = false;
                    reObj.msg = "提现数不能小于1";
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                if (string.IsNullOrEmpty(inObj.walletaAcount) || string.IsNullOrEmpty(inObj.walletAccountName))
                {
                    reObj.success = false;
                    reObj.msg = "提现账户有误";
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                var staff = (await staffDal.getStaff(inObj)).data;
                if (staff == null || staff.id == 0)
                {
                    reObj.success = false;
                    reObj.msg = "获取员工资料有误";
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                string opuserName = "";

                var waitList = await dapperClient.FindAll(x => x.staffId == staff.id && x.status == "已安装激活");

                if (inObj.clientNum > waitList.Count())
                {
                    reObj.success = false;
                    reObj.msg = string.Format("提现数有误最多提现{0}，当前提现为{1}", waitList.Count(), inObj.clientNum);
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                waitList = waitList.Take(inObj.clientNum);

                EtcWalletEntity wallet = new EtcWalletEntity()
                {
                    staffId = staff.id,
                    clientNum = inObj.clientNum,
                    money = waitList.Sum(x => x.money),
                    createTime = DateTime.Now,
                    remark = inObj.remark,
                    status = "未发放",
                    walletAccount = inObj.walletaAcount,
                    walletAccountName = inObj.walletAccountName,
                    walletAccountType = inObj.walletAccountType,
                    opuserName = opuserName
                };

                dapper.TranscationBegin();
                try
                {
                    wallet.id = await dapper.Save(new DtoSave<EtcWalletEntity>
                    {
                        data = wallet
                    });
                    if (wallet.id < 1)
                    {
                        reObj.success = false;
                        reObj.msg = "保存钱包失败";
                        dapper.TranscationRollback();
                    }

                    dapperClient = new DapperHelper<EtcClientEntity>(dapper.GetConnection(), dapper.GetTransaction());
                    var opNum = await dapperClient.Update("status='已结算',walletId=" + wallet.id, "Id in (" + string.Join(",", waitList.Select(x => x.id)) + ")");
                    if (opNum != waitList.Count())
                    {
                        reObj.success = false;
                        reObj.msg = "更新客户状态失败";
                        dapper.TranscationRollback();
                    }

                    dapper.TranscationCommit();
                }
                catch (Exception e)
                {

                    dapper.TranscationRollback();
                    throw e;
                }

            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            LogHelper.WriteDebugLog(this.GetType(), string.Format("submitWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
            return reObj;
        }

        public async Task<ResultObj<int>> save(DtoSave<EtcWalletEntity> inEnt)
        {
            var reObj = new ResultObj<int>();
            try
            {
                LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWallet：请求{0}", TypeChange.ObjectToStr(inEnt)));
                var client = await dapper.SingleByKey(inEnt.data.id);
                if (client == null)
                {
                    reObj.success = false;
                    reObj.msg = "Id有误";
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                if (client.status.Equals("已结算"))
                {
                    reObj.success = false;
                    reObj.msg = "该用户已经结算，不可修改";
                    LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
                    return reObj;
                }

                inEnt.saveFieldList = new List<string> { "remark", "carNum", "carType", "submitTime", "status", "opuserName" };
                inEnt.ignoreFieldList = null;
                inEnt.whereList = new List<string> { "id" };
                inEnt.token = inEnt.token.Replace("#", "");
                //inEnt.data.opuserName = Fun.HashDecrypt(inEnt.token);
                inEnt.data.opuserName = inEnt.token;
                var opNum = await dapper.Update(inEnt);
            }
            catch (Exception e)
            {
                LogHelper.WriteErrorLog(this.GetType(), e.ToString());
                reObj.success = false;
                reObj.msg = e.Message;
            }
            LogHelper.WriteDebugLog(this.GetType(), string.Format("saveWallet：返回{0}", TypeChange.ObjectToStr(reObj)));
            return reObj;
        }
    }
}
