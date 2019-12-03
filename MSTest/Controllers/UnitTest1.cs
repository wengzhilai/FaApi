using Helper;
using Helper.generate;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ApiUser.Controllers.Tests
{
    [TestClass()]
    public class UnitTest1
    {
        //private IConfiguration Configuration;

        public UnitTest1()
        {
            AppSettingsManager.self.MysqlSettings = TypeChange.ToJsonObject<MysqlSettings>(@"{
    ""server"": ""localhost"",
    ""userid"": ""etc"",
    ""pwd"": ""etc"",
    ""port"": ""3306"",
    ""database"": ""etc"",
    ""sslmode"": ""none""
  }");

        }

        [TestMethod()]
        public void UserCodeLoginTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MakeEntity()
        {
            PathConfig cfg = new PathConfig();
            //        cfg.entityPath="D:\\IdeaProjects\\study_new\\study-dependencies\\src\\main\\java\\com\\wzl\\commons\\model\\entity\\";
            cfg.entityPath = "F:\\Study\\FaApi\\MSTest\\";
            //cfg.entityPath = "/Users/wengzhilai/Desktop/dotnet/FaApi/MSTest/FaModuleEntity.cs";
            cfg.tableName = "query";
            cfg.tableNameRmark = "查询";
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