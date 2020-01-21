using Helper;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace MSTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void TestMethod1()
        {
            string str = @"
[
    {
        ""id"": 1,
        ""parentId"": 0,
        ""name"": ""系统管理"",
        ""location"": """",
        ""code"": ""system"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 1,
        ""description"": """",
        ""imageUrl"": ""nb-gear"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 2,
        ""parentId"": 1,
        ""name"": ""角色管理"",
        ""location"": ""/pages/query/query?code=role"",
        ""code"": ""role"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 3,
        ""description"": ""123"",
        ""imageUrl"": ""ion-person-stalker"",
        ""desktopRole"": ""3"",
        ""w"": 1,
        ""h"": 2
    },
    {
        ""id"": 3,
        ""parentId"": 1,
        ""name"": ""模块管理"",
        ""location"": ""/pages/query/query?code=module"",
        ""code"": ""module"",
        ""isDebug"": 0,
        ""isHide"": 0,
        ""showOrder"": 2,
        ""description"": """",
        ""imageUrl"": ""nb-grid-b-outline"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 4,
        ""parentId"": 1,
        ""name"": ""组织结构"",
        ""location"": ""/pages/query/query/district"",
        ""code"": ""district"",
        ""isDebug"": 0,
        ""isHide"": 1,
        ""showOrder"": 3,
        ""description"": """",
        ""imageUrl"": ""ion-network"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 5,
        ""parentId"": 1,
        ""name"": ""脚本管理"",
        ""location"": ""/pages/query/query?code=script"",
        ""code"": ""script"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 6,
        ""description"": ""脚本管理"",
        ""imageUrl"": ""nb-snowy-circled"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 6,
        ""parentId"": 1,
        ""name"": ""定时任务"",
        ""location"": ""/pages/quartztask/list"",
        ""code"": ""quartztask"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 6,
        ""description"": ""定时任务"",
        ""imageUrl"": ""nb-sunny-circled"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 7,
        ""parentId"": 0,
        ""name"": ""设备管理"",
        ""location"": """",
        ""code"": ""Equiment"",
        ""isDebug"": 0,
        ""isHide"": 0,
        ""showOrder"": 2,
        ""description"": """",
        ""imageUrl"": ""nb-tables"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 8,
        ""parentId"": 1,
        ""name"": ""用户管理"",
        ""location"": ""/pages/query/query?code=user"",
        ""code"": ""user"",
        ""isDebug"": 0,
        ""isHide"": 0,
        ""showOrder"": 4,
        ""description"": """",
        ""imageUrl"": ""ion-person"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 9,
        ""parentId"": 1,
        ""name"": ""查询管理"",
        ""location"": ""/pages/query/list"",
        ""code"": ""query"",
        ""isDebug"": 0,
        ""isHide"": 0,
        ""showOrder"": 1,
        ""description"": ""222"",
        ""imageUrl"": ""ion-android-archive"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 10,
        ""parentId"": 7,
        ""name"": ""自定义表"",
        ""location"": ""/pages/query/query?code=tableType"",
        ""code"": ""tableType"",
        ""isDebug"": 0,
        ""isHide"": 0,
        ""showOrder"": 1,
        ""description"": """",
        ""imageUrl"": ""nb-layout-centre"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    }
]";

            
            var obj = TypeChange.JsonToObject<List<Dictionary<string,string>>>(str);
            str = TypeChange.ObjectToStr(obj);
            Console.WriteLine(str);
        }

        [TestMethod]
        public void TestPwd()
        {
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.CreateRepository("NETCoreRepository");

            try
            {

                string pwdStr = Fun.HashEncrypt("{\"password\":\"12345678.\",\"username\":\"13028165793\"}");

                //string reOjb = Fun.HashDecrypt(pwdStr, pwd);
                Console.WriteLine(pwdStr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw e;
            }

        }



        [TestMethod]
        public async Task HttpPost()
        {
            string postStr = @"

{
  ""account"": ""18180770313"",
  ""imgUrl"": ""3333"",
  ""nickName"": ""特上他"",
  ""pcName"": ""pc-sklfj""
}";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyTmFtZSI6IjE4MTgwNzcwMzEzIiwiZXhwIjoxNTc5NDg1NjM5LCJ1c2VySWQiOjQyLCJ0aW1lc3RhbXAiOjE1Nzk0ODU2Mzl9.4URx8gpBL3YBVVDJqdkjj9hHPk2cJo8Ld7VznsgDQfo");
            string reStr= await HttpHelper.HttpPost("http://192.168.2.34:9300/api/WechatInfo/addWechatLogin", postStr, "application/json",3, headers);
            Console.WriteLine(reStr);
            Console.WriteLine(11);

        }
    }
}
