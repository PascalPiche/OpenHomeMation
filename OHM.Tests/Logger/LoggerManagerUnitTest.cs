using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OHM.Logger.Tests
{
    [TestClass]
    public class LoggerManagerUnitTest
    {

        [TestMethod]
        public void TestGetLoggerName()
        {
            var logMng = new LoggerManager();
            var result = logMng.GetLogger("a.a");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ILog));

            //Generate exception code coverage
            var result2 = logMng.GetLogger("b.b");
            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(ILog));
        }
    }
}
