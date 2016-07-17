using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataValueStringUnitTest
    {
        [TestMethod]
        public void TestDataValueString()
        {

            var t = new DataValueString();

            Assert.AreEqual(typeof(DataValueString), t.Type);
            Assert.IsNull(t.Value);

            t = new DataValueString("111");

            Assert.AreEqual("111", t.Value);

            t.Value = "Test";

            Assert.AreEqual("Test", t.Value);
        }
    }
}
