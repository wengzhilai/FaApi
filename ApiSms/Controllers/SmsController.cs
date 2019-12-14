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
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultObj<bool>> SendValidSms(DtoKey inObj)
        {
            var reObj = new ResultObj<bool>();
            var code = PicFunHelper.ValidateMake(4);

            JSMSClient jp = new JSMSClient(AppSettingsManager.self.JpushCfg.AppKey, AppSettingsManager.self.JpushCfg.MasterSecret);
            //var senEnt = new TemplateMessage();
            //senEnt.Type = 1;
            //senEnt.Mobile = inObj.Key;
            //senEnt.TemplateId = 1;
            //senEnt.ValidDuration = 60 * 10;
            //senEnt.TemplateParameters = new Dictionary<string, string>();
            //senEnt.TemplateParameters.Add("code", code);
            //var jpReObj = await jp.SendTemplateMessageAsync(senEnt);
            var jpReObj = await jp.SendCodeAsync(inObj.Key,1,null);
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
                reObj.code = msg_id;
                reObj.msg = Fun.HashEncrypt(inObj.Key + "|" + code + "|" + TypeChange.DateToInt64(DateTime.Now));
            }
            
            return reObj;

        }

        /// <summary>
        /// 验证短信
        /// </summary>
        /// <param name="inObj"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> ValidSms(ValidSmsDto inObj)
        {
            var reObj = new Result();

            JSMSClient jp = new JSMSClient(AppSettingsManager.self.JpushCfg.AppKey, AppSettingsManager.self.JpushCfg.MasterSecret);
            var jpReObj = await jp.IsCodeValidAsync(inObj.msgId, inObj.code);
            JObject jb = TypeChange.JsonToObject(jpReObj.Content);
            //{"is_valid":false,"error":{"code":50026,"message":"wrong msg_id"}}
            var is_valid = jb.Value<bool>("is_valid");
            reObj.success = is_valid;
            if (!reObj.success)
            {
                reObj.msg = jb.Value<JObject>("error").Value<string>("message");
            }
            else
            {
                reObj.msg = jpReObj.Content;
            }
            return reObj;
        }

    }
    public class ValidSmsDto
    {
        /// <summary>
        /// 短信ID
        /// </summary>
        public string msgId { get; set; } 
        /// <summary>
        /// 短信代码
        /// </summary>
        public string code { get; set; }
    }
}