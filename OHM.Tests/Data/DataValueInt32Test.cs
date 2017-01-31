using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataValueInt32UnitTest
    {
        [TestMethod]
        public void TestDataValueInt32()
        {

            var t = new DataValueInt();

            Assert.AreEqual(typeof(DataValueInt), t.Type);
            Assert.AreEqual(0, t.Value);

            t = new DataValueInt(24);

            Assert.AreEqual(24, t.Value);

            t.Value = 43;

            Assert.AreEqual(43, t.Value);
        }
    }
}
