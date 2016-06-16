using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using Rhino.Mocks;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataStoreUnitTest
    {
        [TestMethod]
        public void TestDataStoreKeyProperty()
        {
            var d = new DataStore("key");

            Assert.AreEqual("key", d.Key);

        }

        [TestMethod]
        public void TestDataStoreInitAndSave()
        {
            var d = new DataStore("Key");
            IDataManager mng = MockRepository.GenerateStub<IDataManager>();

            Assert.IsFalse(d.Save());
            mng.AssertWasNotCalled(x => x.SaveDataStore(d));
            
            //Need a manager to save data
            d.Init(mng);

            /*Assert.IsFalse(d.Save()); //Need a key for saving
            mng.AssertWasNotCalled(x => x.SaveDataStore(d));


            d = new DataStore("key");
            d.Init(mng);*/

            //Fack good result
            mng.Stub(x => x.SaveDataStore(d)).Return(true);
            
            Assert.IsTrue(d.Save());

            mng.AssertWasCalled(x => x.SaveDataStore(d));
        }
    }
}
