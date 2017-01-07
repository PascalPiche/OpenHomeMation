using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Logger;
using Rhino.Mocks;
using System;
using System.IO;

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
            loggerMng.Stub(x => x.GetLogger("", "FileDataManager")).Return(logger);
            var d = new FileDataManager(_filePath);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath))
            {
                Directory.Delete(_filePath, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath));

            //Must create the directory
            d.Init(loggerMng);

            Assert.IsTrue(Directory.Exists(_filePath));

            //Should do nothing
            Assert.IsTrue(d.Init(loggerMng));

        }

        [TestMethod]
        public void TestFileDataManagerInitWithError()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("", "FileDataManager")).Return(logger);

            //With Invalid file Path
            var d = new FileDataManager("AB:");

            //Make Sure Data is empty
            /*if (Directory.Exists(_filePath4))
            {
                Directory.Delete(_filePath4, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath4));
            */
            //Should throw error

            bool result = d.Init(loggerMng);

            //Assert.IsTrue(Directory.Exists(_filePath4));

            //Should do nothing
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void TestFileDataManagerGetOrCreate()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("", "FileDataManager")).Return(logger);
            var d = new FileDataManager(_filePath2);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath2))
            {
                Directory.Delete(_filePath2, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath2));

            //Create Data folder
            d.Init(loggerMng);

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
            loggerMng.Stub(x => x.GetLogger("", "FileDataManager")).Return(logger);
            var d = new FileDataManager(_filePath3);

            //Make Sure Data is empty
            if (Directory.Exists(_filePath3))
            {
                Directory.Delete(_filePath3, true);
            }

            Assert.IsFalse(Directory.Exists(_filePath3));

            //Create Data folder
            d.Init(loggerMng);

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
            d = new FileDataManager(_filePath3);
            d.Init(loggerMng);
            
            //Get Again DataStore
            var data3 = d.GetDataStore("key");

            Assert.IsNotNull(data3);

            Assert.AreEqual("Hi", data3.GetString("key"));
        }

    }
}
