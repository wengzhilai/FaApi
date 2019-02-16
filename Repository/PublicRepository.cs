
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helper;
using IRepository;
using Models;
using System.Linq;
using Models.Entity;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using System.Linq.Expressions;

namespace Repository
{
    public class PublicRepository : IPublicRepository
    {
        public Task<Result> GetChineseCalendar(DateTime inDate)
        {
            Result reObj = new Result();
            return Task.Run(() => reObj);
        }

        public async Task<Result> SendCode(string phone)
        {
            Result reEnt = new Result();
            if (string.IsNullOrEmpty(phone))
            {
                reEnt.IsSuccess = false;
                reEnt.Msg = "电话号码不能为空";
                return reEnt;
            }

            if (!phone.IsOnlyNumber() || phone.Length != 11)
            {
                reEnt.IsSuccess = false;
                reEnt.Msg = "电话号码格式不正确";
                return reEnt;
            }

            var code = PicFunHelper.ValidateMake(4);
            DapperHelper<FaLoginEntity> dapperLogin = new DapperHelper<FaLoginEntity>();
            try
            {
                dapperLogin.TranscationBegin();
                var login = await dapperLogin.Single(x => x.LOGIN_NAME == phone);
                if (login != null)
                {
                    login.VERIFY_CODE = code;
                    login.VERIFY_TIME=DateTime.Now;
                    reEnt.IsSuccess= await dapperLogin.Update(new DtoSave<FaLoginEntity>{
                        Data=login,
                        SaveFieldList=new List<string>{"VERIFY_CODE","VERIFY_TIME"},
                        IgnoreFieldList=null
                    })>0;
                    if(!reEnt.IsSuccess){
                        reEnt.Msg="更新用户验证码失败";
                        dapperLogin.TranscationRollback();
                        return reEnt;
                    }
                }

                FaSmsSendEntity ent = new FaSmsSendEntity()
                {
                    GUID = Guid.NewGuid().ToString().Replace("-", ""),
                    ADD_TIME = DateTime.Now,
                    CONTENT = code,
                    STAUTS = "成功",
                    PHONE_NO = phone
                };

                //发送短信
                reEnt.IsSuccess=await SmsSendCode(phone, code);
                if (!reEnt.IsSuccess)
                {
                    reEnt.IsSuccess = false;
                    reEnt.Msg = "短信服务已欠费，请联系管理员";
                    dapperLogin.TranscationRollback();
                    return reEnt;
                }
                reEnt.IsSuccess=await new DapperHelper<FaSmsSendEntity>().Save(new DtoSave<FaSmsSendEntity>
                {
                    Data = ent
                })>0;
                if (!reEnt.IsSuccess)
                {
                    reEnt.IsSuccess = false;
                    reEnt.Msg = "保存发送记录失败";
                    dapperLogin.TranscationRollback();
                    return reEnt;
                }
                dapperLogin.TranscationCommit();
                reEnt.IsSuccess=true;
                reEnt.Msg="发送成功";
            }
            catch (Exception e)
            {
                reEnt.IsSuccess = false;
                reEnt.Msg=e.Message;

                LogHelper.WriteErrorLog<PublicRepository>(e.ToString());
                dapperLogin.TranscationRollback();
            }
            return reEnt;
        }

        public async Task<bool> SmsSendCode(string mobile, string code)
        {
            await JiguangHelper.SendValidSms(mobile, code);
            

            return true;
        }
    }
}