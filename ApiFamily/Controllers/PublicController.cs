
using System;
using Microsoft.AspNetCore.Mvc;
using Models;
using Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace ApiFamily.Controllers
{
    /// <summary>
    /// 授权管理
    /// </summary>
    [EnableCors("AllowSameDomain")]
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PublicController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public PublicController()
        {

        }

        /// <summary>
        /// 获取阴历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultObj<string> GetLunarDate(DtoKey inEnt)
        {
            var reObj = new ResultObj<string>();
            try
            {
                if (string.IsNullOrEmpty(inEnt.Key)) return reObj;
                inEnt.Key = inEnt.Key.Replace("T", " ");
                DateTime datetime = Convert.ToDateTime(inEnt.Key);
                datetime = TypeChange.DateToLunar(datetime);
                reObj.success = true;
                reObj.data = datetime.ToString("yyyy-MM-dd HH:mm");
                return reObj;
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }

        /// <summary>
        /// 获取阳历 key为时间字符串
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResultObj<string> GetSolarDate(DtoKey inEnt)
        {
            var reObj = new ResultObj<string>();
            try
            {
                if (string.IsNullOrEmpty(inEnt.Key)) return reObj;
                inEnt.Key = inEnt.Key.Replace("T", " ");
                DateTime datetime = Convert.ToDateTime(inEnt.Key);
                datetime = TypeChange.DateToLunar(datetime);
                reObj.success = true;
                reObj.data = datetime.ToString("yyyy-MM-dd HH:mm");

                return reObj;
            }
            catch (ExceptionExtend e)
            {
                reObj.success = false;
                reObj.code = e.RealCode;
                reObj.msg = e.RealMsg;
            }
            catch (Exception e)
            {
                reObj.success = false;
                reObj.msg = e.Message;
            }
            return reObj;
        }


    }
}