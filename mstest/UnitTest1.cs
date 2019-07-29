using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Helper;
namespace mstest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Console.WriteLine(Fun.FormatLunlarTime(DateTime.Now));
            Assert.IsTrue(false);
        }
    }
}
