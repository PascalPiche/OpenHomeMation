using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using Rhino.Mocks;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataValueDictionaryUnitTest
    {
        [TestMethod]
        public void TestDataValueStore()
        {
            var t = new DataValueDictionary();
            var data = MockRepository.GenerateStub<IDataStore>();
            var data2 = MockRepository.GenerateStub<IDataStore>();
            Assert.AreEqual(typeof(DataValueDictionary), t.Type);

            Assert.IsNull(t.Value);

            t = new DataValueDictionary(data);

            Assert.AreSame(data, t.Value);

            t.Value = data2;

            Assert.AreSame(data2, t.Value);

        }
    }
}
