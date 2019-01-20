
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
            DapperHelper<FaLoginEntity> dapperLogin=new DapperHelper<FaLoginEntity>();
            
                var login =await dapperLogin.Single(x => x.LOGIN_NAME == phone);
                if (login != null)
                {
                    login.VERIFY_CODE = code;
                }

                FaSmsSendEntity ent = new FaSmsSendEntity()
                {
                    GUID = Guid.NewGuid().ToString().Replace("-", ""),
                    ADD_TIME = DateTime.Now,
                    CONTENT = code,
                    STAUTS = "成功",
                    PHONE_NO = phone
                };
                if (await SmsSendCode(phone, code))
                {
                    reEnt.Msg = "发送成功";
                }
                else
                {
                    reEnt.IsSuccess = false;
                    reEnt.Msg = "短信服务已欠费，请联系管理员";
                }
                await new DapperHelper<FaSmsSendEntity>().Save(new DtoSave<FaSmsSendEntity>{
                    Data=ent
                });
                return reEnt;
        }

        public async Task<bool> SmsSendCode(string mobile, string code)
        {
            JiguangHelper.SendValidSms(mobile,code)
            return true;
        }
    }
}