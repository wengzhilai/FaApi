using Helper;
using Helper.generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Entity;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace ApiUser.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        //private IConfiguration Configuration;

        public UnitTest1()
        {
            AppSettingsManager.self.MysqlSettings = TypeChange.ToJsonObject<MysqlSettings>(@"{
    ""server"": ""dotnetapi.wjbjp.cn"",
    ""userid"": ""fa"",
    ""pwd"": ""fa"",
    ""port"": ""3306"",
    ""database"": ""study"",
    ""sslmode"": ""none""
  }");

        }

        [TestMethod()]
        public void UserCodeLoginTest()
        {
            Func<FaUserInfoEntity, string[]> func;
            func = x => new string[] {x.alias,x.authority.ToString() };

            Expression<Func<FaUserInfoEntity, object[]>> expression = x => new object[] { x.alias, x.authority,x.birthdayTime };

            switch (expression.Body.NodeType)
            {
                case ExpressionType.NewArrayInit:
                    NewArrayExpression bodyExp =(NewArrayExpression) expression.Body;

                    List<string> reList = new List<string>();
                    foreach (var item in bodyExp.Expressions)
                    {
                        switch (item.NodeType)
                        {
                            case ExpressionType.MemberAccess:
                                MemberExpression itemBody =(MemberExpression)item;
                                reList.Add(itemBody.Member.Name);
                                break;
                            case ExpressionType.Convert:
                                UnaryExpression itemBody1 =(UnaryExpression)item;
                                if(itemBody1.Operand.NodeType== ExpressionType.MemberAccess)
                                {
                                    MemberExpression itemBody2 = (MemberExpression)itemBody1.Operand;
                                    reList.Add(itemBody2.Member.Name);
                                }
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }


            Assert.Fail();
        }

        [TestMethod()]
        public void MakeEntity()
        {
            PathConfig cfg = new PathConfig();
            //        cfg.entityPath="D:\\IdeaProjects\\study_new\\study-dependencies\\src\\main\\java\\com\\wzl\\commons\\model\\entity\\";
            cfg.entityPath = "F:\\Study\\FaApi\\MSTest\\";
            //cfg.entityPath = "/Users/wengzhilai/Desktop/dotnet/FaApi/MSTest/FaModuleEntity.cs";
            cfg.tableName = "fa_user_info";
            cfg.tableNameRmark = "用户";
            //        cfg.clumStr="" +
            //                "ID\tID\tint\t\t\tTRUE\tFALSE\tTRUE\n" +
            //                "口径任务ID\tSCRIPT_TASK_ID\tint\t\t\tFALSE\tTRUE\tTRUE\n" +
            //                "记录时间\tLOG_TIME\tdatetime\t\t\tFALSE\tFALSE\tTRUE\n" +
            //                "日志级别\tLOG_TYPE\tnumeric(1)\t1\t\tFALSE\tFALSE\tTRUE\n" +
            //                "日志说明\tMESSAGE\ttext\t\t\tFALSE\tFALSE\tFALSE\n" +
            //                "SQL内容\tSQL_TEXT\ttext\t\t\tFALSE\tFALSE\tFALSE" +
            //                "";
            
           GenerateFile.MakeEntity(cfg);

        }
    }
}