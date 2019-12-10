using Microsoft.VisualStudio.TestTools.UnitTesting;
using Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Tests
{
    [TestClass()]
    public class RedisWriteHelperTests
    {
        [TestMethod()]
        public void HashSetKeyTest()
        {
            RedisWriteHelper.SetObject<RedisConfig>("aa", new RedisConfig { readRedisstr="dd" });
            var t= RedisReadHelper.GetObject<RedisConfig>("aa");
            Console.WriteLine(t.Item1.readRedisstr);
            Assert.Fail();
        }
    }
}