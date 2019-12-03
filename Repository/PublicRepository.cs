
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
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace Repository
{
    public class PublicRepository : IPublicRepository
    {

        public async Task<Result> SendCode(string phone)
        {
            Result reEnt = new Result();
            if (string.IsNullOrEmpty(phone))
            {
                reEnt.success = false;
                reEnt.msg = "电话号码不能为空";
                return reEnt;
            }

            if (!phone.IsOnlyNumber() || phone.Length != 11)
            {
                reEnt.success = false;
                reEnt.msg = "电话号码格式不正确";
                return reEnt;
            }

            var code = PicFunHelper.ValidateMake(4);
            // code = "1111";
            DapperHelper<FaLoginEntity> dapperLogin = new DapperHelper<FaLoginEntity>();
            try
            {
                dapperLogin.TranscationBegin();
                #region 如果用户存在，则更新用户表
                var login = await dapperLogin.Single(x => x.loginName == phone);
                if (login != null)
                {
                    login.verifyCode = code;
                    login.verifyTime = DateTime.Now;
                    reEnt.success = await dapperLogin.Update(new DtoSave<FaLoginEntity>
                    {
                        data = login,
                        saveFieldList = new List<string> { "VERIFY_CODE", "VERIFY_TIME" },
                        ignoreFieldList = null
                    }) > 0;
                    if (!reEnt.success)
                    {
                        reEnt.msg = "更新用户验证码失败";
                        dapperLogin.TranscationRollback();
                        return reEnt;
                    }
                }
                #endregion


                #region 极光发送短信

                reEnt = await SmsSendCode(phone, code);
                if (!reEnt.success)
                {
                    reEnt.success = false;
                    reEnt.msg = "短信服务已欠费，请联系管理员";
                    dapperLogin.TranscationRollback();
                    return reEnt;
                }
                #endregion

                #region 发送短信后，保存发送的历史
                FaSmsSendEntity ent = new FaSmsSendEntity()
                {
                    GUID = Guid.NewGuid().ToString().Replace("-", ""),
                    ADD_TIME = DateTime.Now,
                    CONTENT = code,
                    STAUTS = "成功",
                    PHONE_NO = phone
                };

                reEnt.success = await new DapperHelper<FaSmsSendEntity>().Save(new DtoSave<FaSmsSendEntity>
                {
                    data = ent
                }) > 0;
                if (!reEnt.success)
                {
                    reEnt.success = false;
                    reEnt.msg = "保存发送记录失败";
                    dapperLogin.TranscationRollback();
                    return reEnt;
                }
                #endregion

                dapperLogin.TranscationCommit();
                reEnt.success = true;
                reEnt.msg = "发送成功";
            }
            catch (Exception e)
            {
                reEnt.success = false;
                reEnt.msg = e.Message;

                LogHelper.WriteErrorLog<PublicRepository>(e.ToString());
                dapperLogin.TranscationRollback();
            }
            return reEnt;
        }

        public async Task<ResultObj<bool>> SmsSendCode(string mobile, string code)
        {
            ResultObj<bool> reObj = new ResultObj<bool>();

            var t = await JiguangHelper.SendValidSms(mobile, code);
            // {\"error\":{\"code\":50051,\"message\":\"signatures not exist\"}}
            JObject jb = TypeChange.JsonToObject(t.Content);
            var msg_id = jb.Value<string>("msg_id");
            if (string.IsNullOrEmpty(msg_id))
            {
                reObj.success = false;
                var errorObj = jb.Value<JObject>("error");
                if (errorObj != null)
                {
                    reObj.msg = errorObj.Value<string>("message");
                }
            }
            else
            {
                reObj.success = true;
            }
            reObj.msg = t.Content;
            return reObj;
        }

        /// <summary>
        /// 获取阴历 key为时间字符串
        /// </summary>
        /// <param name="datetime">时间字符串</param>
        /// <returns></returns>
        public Result GetLunarDate(DateTime datetime)
        {
            Result reObj = new Result();
            if (datetime < new DateTime(1900, 1, 1))
            {
                reObj.success = true;
                reObj.msg = datetime.ToString("yyyy-MM-dd HH:mm");
                return reObj;
            }

            ChineseLunisolarCalendar cc = new ChineseLunisolarCalendar();
            int lyear = cc.GetYear(datetime);
            int lmonth = cc.GetMonth(datetime);
            int lday = cc.GetDayOfMonth(datetime);
            reObj.success = true;
            reObj.msg = DateTime.Parse(string.Format("{0}-{1}-{2} {3}:{4}", lyear, lmonth, lday, datetime.Hour, datetime.Minute)).ToString("yyyy-MM-dd HH:mm");
            return reObj;
        }

        /// <summary>
        /// 获取阳历 key为时间字符串
        /// </summary>
        /// <param name="datetime">时间字符串</param>
        /// <returns></returns>
        public Result GetSolarDate(DateTime datetime)
        {
            Result reObj = new Result();
            if (datetime < new DateTime(1900, 1, 1))
            {
                reObj.success = true;
                reObj.msg = datetime.ToString("yyyy-MM-dd HH:mm");
                return reObj;
            }
            ChineseLunisolarCalendar cc = new ChineseLunisolarCalendar();
            DateTime dt = new DateTime();
            try
            {
                dt = cc.ToDateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute, datetime.Second, 0);
            }
            catch { }
            //判断到某个月份是否有润月
            for (int i = 1; i <= datetime.Month; i++)
                if (cc.IsLeapMonth(datetime.Year, i))
                    dt = dt.AddMonths(1);

            reObj.success = true;
            reObj.msg = dt.ToString("yyyy-MM-dd HH:mm");
            return reObj;
        }


    }
}