using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helper;
using Jiguang.JSMS;
using Jiguang.JSMS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json.Linq;

namespace ApiSms.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result<bool>> SendValidSms(SendValidSmsDto inObj)
        {
            var reObj = new Result<bool>();

            JSMSClient jp = new JSMSClient(AppSettingsManager.self.JpushCfg.AppKey, AppSettingsManager.self.JpushCfg.MasterSecret);
            var senEnt = new TemplateMessage();
            senEnt.Type = 1;
            senEnt.Mobile = inObj.mobile;
            senEnt.TemplateId = 1;
            senEnt.ValidDuration = 60 * 10;
            senEnt.TemplateParameters = new Dictionary<string, string>();
            senEnt.TemplateParameters.Add("code", inObj.code);
            var jpReObj = await jp.SendTemplateMessageAsync(senEnt);
            // jpReObj.Content=
            // {\"error\":{\"code\":50051,\"message\":\"signatures not exist\"}}
            Console.WriteLine(jpReObj);

            JObject jb = TypeChange.JsonToObject(jpReObj.Content);
            var msg_id = jb.Value<string>("msg_id");
            var errorObj = jb.Value<JObject>("error");
            if (string.IsNullOrEmpty(msg_id) || errorObj != null)
            {
                reObj.success = false;
                reObj.msg = errorObj.Value<string>("message");
            }else
            {
                reObj.success = true;
            }
            return reObj;

        }

        public class SendValidSmsDto
        {
            /// <summary>
            /// 电话号码
            /// </summary>
            public string mobile { get; set; }
            /// <summary>
            /// 验证码
            /// </summary>
            public string code { get; set; }
        }
    }
}