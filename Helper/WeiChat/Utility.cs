﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.WeiChat
{
    public static class Utility
    {
        /// <summary>
        /// 获取微信的token
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public static string GetAccessToken(string appid,string secret)
        {
            var token = "";
            string access_tokenJson = Fun.HttpGetJson(string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, secret));
            var dict = TypeChange.JsonToObject<Dictionary<string, string>>(access_tokenJson);
            if (dict.ContainsKey("access_token"))
            {
                token = dict["access_token"];
            }
            return token;
        }

        /// <summary>
        /// 获取维护关注公众号带参数的二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="postEnt"></param>
        /// <returns></returns>
        public static string GetQrCodeTicket(string token,string postEnt=null)
        {
            var ticket = "";
            if (string.IsNullOrEmpty(postEnt))
            {
                postEnt = "{\"action_name\": \"QR_LIMIT_STR_SCENE\", \"action_info\": {\"scene\": {\"scene_str\": \"etc_test\"}}}";
            }
            ticket = Fun.HttpPostJson("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + token, postEnt);
            var ticketDict = TypeChange.JsonToObject<Dictionary<string, string>>(ticket);
            if (ticketDict.ContainsKey("ticket"))
            {
                ticket = ticketDict["ticket"];
            }

            return ticket;
        }
    }
}