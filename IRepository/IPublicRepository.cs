using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Models;
using Models.Entity;

namespace IRepository
{
    public interface IPublicRepository 
    {
        /// <summary>
        /// 阳历转阳历
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> GetChineseCalendar(DateTime inDate);
        

        /// <summary>
        /// 发送验证码到手机
        /// <para>发送时会在用户的Login表里修改VERIFY_CODE，并在fa_sms_send增加记录</para>
        /// </summary>
        /// <param name="inEnt"></param>
        /// <returns></returns>
        Task<Result> SendCode(string mobile);


        /// <summary>
        /// 直接发送短信
        /// </summary>
        /// <param name="loginKey"></param>
        /// <param name="mobile">手机号码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        Task<bool> SmsSendCode(string mobile, string code);
    }
}