
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
using System;
using ApiEtc.Models.Entity;

namespace ApiEtc.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class WeiXinController : ControllerBase
    {
        private IStaff staff;
        private IWeixin weixin;
        public WeiXinController(IStaff dal, IWeixin weixin)
        {
            this.staff = dal;
            this.weixin = weixin;
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
                var saveEnt= new EtcWeixinEntity()
                {
                    openid = inObj.FromUserName,
                    createTime = DateTime.Now,
                    parentTicket = inObj.Ticket,
                    eventKey = inObj.EventKey,
                };
                return await weixin.save(saveEnt);
            }
            return reObj;
        }

        [HttpPost]
        public async Task<Result> MakeAllTicket(XmlModel inObj)
        {
            var reObj = new Result();
            var allUser = await staff.getStaffList();

            var token= RedisReadHelper.StringGet("WECHA_ACCESS_TOKEN");
            if (string.IsNullOrEmpty(token))
            {
                token = Helper.WeiChat.Utility.GetAccessToken(AppConfig.WeiXin.Appid, AppConfig.WeiXin.Secret);
                RedisWriteHelper.SetString("WECHA_ACCESS_TOKEN", token,new TimeSpan(2,0,0));
            }



            if (!string.IsNullOrEmpty(token))
            {
                foreach (var item in allUser.dataList)
                {
                    if (string.IsNullOrEmpty(item.etcNo)) item.etcNo = "87000075";
                    string postStr = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc_" + item.etcNo + "|" + item.phone + "\"}}}";
                    item.ticket = Helper.WeiChat.Utility.GetQrCodeTicket(token, postStr);

                    await staff.updateTicket(item);
                }
            }
            return reObj;
        }


    }
}