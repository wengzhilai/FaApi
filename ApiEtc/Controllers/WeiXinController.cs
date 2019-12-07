
using Helper;
using Helper.WeiChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Linq;
using System.IO;
using System.Text;
using Helper.WeiChat.Entities;
using ApiEtc.Config;
using System.Collections.Generic;
using ApiEtc.Controllers.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApiEtc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WeiXinController : ControllerBase
    {
        private IStaff staff;
        public WeiXinController(IStaff dal)
        {
            this.staff = dal;
        }


        [HttpGet]
        public string index(string echostr)
        {

            PostModel postModel= TypeChange.UrlToEntities<PostModel>(Request.QueryString.Value);
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, AppConfig.WeiXin.Token))
            {
                return echostr; //返回随机字符串则表示验证通过
            }
            else
            {
                return "failed:" + postModel.Signature + "," +
                    "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。";
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<string> index()
        {
            PostModel postModel = TypeChange.UrlToEntities<PostModel>(Request.QueryString.Value);
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, AppConfig.WeiXin.Token))
            {

                return "参数错误！";
            }
            else
            {
                Request.EnableBuffering();

                using (var reader = new StreamReader(Request.Body, encoding: Encoding.UTF8))
                {
                    var body =await reader.ReadToEndAsync();
                    var xml = TypeChange.XmlToDict(body);

                    // Do some processing with body…
                    // Reset the request body stream position so the next middleware can read it
                    Request.Body.Position = 0;
                    return body;

                }

            }
        }

        /// <summary>
        /// 用于微信绑定时间
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> PostXml(XmlModel inObj)
        {
            var reObj = new Result();
            //表示是有推广人的订阅
            if (inObj.Event.Equals("subscribe") && !string.IsNullOrEmpty(inObj.Ticket) && !string.IsNullOrEmpty(inObj.FromUserName))
            {
                string ticket = "";
                #region 获取ticket
                string access_tokenJson = Fun.HttpGetJson(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppConfig.WeiXin.Appid, AppConfig.WeiXin.Secret));
                var dict = TypeChange.JsonToObject<Dictionary<string, string>>(access_tokenJson);
                if (dict.ContainsKey("access_token"))
                {
                    string reStr = "";
                    Fun.HttpPostJson("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + dict["access_token"], "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc\"}}}", ref reStr);
                    var ticketDict = TypeChange.JsonToObject<Dictionary<string, string>>(reStr);
                    if (ticketDict.ContainsKey("ticket"))
                    {
                        ticket = ticketDict["ticket"];
                    }
                }
                #endregion

                var saveResult = await staff.regStaff(new RegStaffDto { Key = inObj.FromUserName, ticket = ticket,parentTicket= inObj.Ticket });
                reObj.success = saveResult.success;
                reObj.msg = saveResult.msg;
            }
            return reObj;
        }

        [HttpPost]
        public async Task<Result> MakeAllTicket(XmlModel inObj)
        {
            var reObj = new Result();
            var allUser = await staff.getStaffList();
            string access_tokenJson = Fun.HttpGetJson(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", AppConfig.WeiXin.Appid, AppConfig.WeiXin.Secret));
            var dict = TypeChange.JsonToObject<Dictionary<string, string>>(access_tokenJson);
            if (dict.ContainsKey("access_token"))
            {
                foreach (var item in allUser.dataList)
                {

                    string reStr = "";
                    Fun.HttpPostJson("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + dict["access_token"], "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc_"+item.phone+"\"}}}", ref reStr);
                    var ticketDict = TypeChange.JsonToObject<Dictionary<string, string>>(reStr);
                    if (ticketDict.ContainsKey("ticket"))
                    {
                        item.ticket = ticketDict["ticket"];
                        var saveResult = await staff.updateTicket(item);
                    }
                }
            }
            return reObj;
        }
    }
}