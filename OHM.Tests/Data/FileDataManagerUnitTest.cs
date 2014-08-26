using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using System.IO;
using Rhino.Mocks;
using OHM.Logger;

namespace OHM.Data.Tests
{
    [TestClass]
    public class FileDataManagerUnitTest
    {

        private string _filePath = AppDomain.CurrentDomain.BaseDirectory + "\\data\\";
        private string _filePath2 = AppDomain.CurrentDomain.BaseDirectory + "\\data2\\";
        private string _filePath3 = AppDomain.CurrentDomain.BaseDirectory + "\\data3\\";

        [TestMethod]
        public void TestFileDataManagerInit()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("FileDataManager")).Return(logger);
            var d = new FileDataManager(loggerMng, _filePath);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath))
            {
                Directory.Delete(_filePath, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath));

            //Must create the directory
            d.Init();

            Assert.IsTrue(Directory.Exists(_filePath));

            //Should do nothing
            d.Init();

        }

        [TestMethod]
        public void TestFileDataManagerGetOrCreate()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("FileDataManager")).Return(logger);
            var d = new FileDataManager(loggerMng, _filePath2);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath2))
            {
                Directory.Delete(_filePath2, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath2));

            //Create Data folder
            d.Init();

            Assert.IsNull(d.GetDataStore("key"));

            Assert.IsNotNull(d.GetOrCreateDataStore("key"));

            //Still not saved but should be able to get again the datastore
            Assert.IsNotNull(d.GetDataStore("key"));

            Assert.IsNotNull(d.GetOrCreateDataStore("key"));
        }

        [TestMethod]
        public void TestFileDataManagerSaveAndGet()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("FileDataManager")).Return(logger);
            var d = new FileDataManager(loggerMng, _filePath3);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath3))
            {
                Directory.Delete(_filePath3, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath3));

            //Create Data folder
            d.Init();

            Assert.IsNull(d.GetDataStore("key"));

            var data = d.GetOrCreateDataStore("key");
            Assert.IsNotNull(data);

            //Add string
            data.StoreString("key", "Hi");

            Assert.IsTrue(d.SaveDataStore(data));

            //Get again from cache
            var data2 = d.GetDataStore("key");

            Assert.IsNotNull(data2);
            Assert.AreEqual("Hi", data2.GetString("key"));

            //Create new FileDataManager (clear cache)
            d = new FileDataManager(loggerMng, _filePath3);
            d.Init();
            
            //Get Again DataStore
            var data3 = d.GetDataStore("key");

            Assert.IsNotNull(data3);

            Assert.AreEqual("Hi", data3.GetString("key"));
        }

    }
}
