using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataValueBoolUnitTest
    {
        [TestMethod]
        public void TestDataValueBool()
        {

            var t = new DataValueBool();

            Assert.AreEqual(typeof(DataValueBool), t.Type);
            Assert.IsFalse(t.Value);

            t = new DataValueBool(true);

            Assert.AreEqual(true, t.Value);

            t.Value = false;

            Assert.AreEqual(false, t.Value);

            t.Value = true;

            Assert.AreEqual(true, t.Value);
        }
    }
}
