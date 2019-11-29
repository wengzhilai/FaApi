using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

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
        ""name"": ""ϵͳ����"",
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
        ""name"": ""��ɫ����"",
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
        ""name"": ""ģ�����"",
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
        ""name"": ""��֯�ṹ"",
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
        ""name"": ""�ű�����"",
        ""location"": ""/pages/query/query?code=script"",
        ""code"": ""script"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 6,
        ""description"": ""�ű�����"",
        ""imageUrl"": ""nb-snowy-circled"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 6,
        ""parentId"": 1,
        ""name"": ""��ʱ����"",
        ""location"": ""/pages/quartztask/list"",
        ""code"": ""quartztask"",
        ""isDebug"": 1,
        ""isHide"": 0,
        ""showOrder"": 6,
        ""description"": ""��ʱ����"",
        ""imageUrl"": ""nb-sunny-circled"",
        ""desktopRole"": """",
        ""w"": 0,
        ""h"": 0
    },
    {
        ""id"": 7,
        ""parentId"": 0,
        ""name"": ""�豸����"",
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
        ""name"": ""�û�����"",
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
        ""name"": ""��ѯ����"",
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
        ""name"": ""�Զ����"",
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
    }
}
