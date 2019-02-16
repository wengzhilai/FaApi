using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jiguang.JSMS;
using Jiguang.JSMS.Model;

namespace Helper
{
    /// <summary>
    /// 极光短信服务
    /// </summary>
    public class JiguangHelper
    {
        
        /// <summary>
        /// 发送验证短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<HttpResponse> SendValidSms(string mobile,string code){
            JSMSClient jp=new JSMSClient(AppSettingsManager.JpushCfg.AppKey,AppSettingsManager.JpushCfg.MasterSecret);
            var senEnt=new TemplateMessage();
            senEnt.Type=1;
            senEnt.Mobile=mobile;
            senEnt.TemplateId=1;
            senEnt.ValidDuration=60*10;
            senEnt.TemplateParameters=new Dictionary<string, string>();
            senEnt.TemplateParameters.Add("code",code);
            var reObj=await jp.SendTemplateMessageAsync(senEnt);
            Console.WriteLine(reObj);
            // reObj.Content=
            // {\"error\":{\"code\":50051,\"message\":\"signatures not exist\"}}
            return reObj;
        }
    }
}