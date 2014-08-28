using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Plugins;
using Rhino.Mocks;
using OHM.Logger;
using OHM.Data;
using System.IO;

namespace OHM.Plugins.Tests
{
    [TestClass]
    public class PluginsManagerUnitTest
    {
        [TestMethod]
        public void TestPluginsManagerConstructor()
        {
            
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var d = new PluginsManager(loggerMng, AppDomain.CurrentDomain.BaseDirectory + "\\plugins\\");
        }

        [TestMethod]
        public void TestPluginsManagerInitWithMissingDirectory()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("PluginsManager")).Return(logger);
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\pluginsTest\\";

            var d = new PluginsManager(loggerMng, filePath);

            //Check directory not exists
            Assert.IsFalse(Directory.Exists(filePath));

            //Should not throw exception and will fail silently
            Assert.IsTrue(d.Init(dataStore));

            Assert.AreEqual(0, d.AvailablesPlugins.Count);
            Assert.AreEqual(0, d.InstalledPlugins.Count);

        }

        
        [TestMethod]
        public void TestPluginsManagerInitWithPlugin()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("PluginsManager")).Return(logger);
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\pluginsStub\\";

            var d = new PluginsManager(loggerMng, filePath);

            //Make Sure directory exists
            Assert.IsTrue(Directory.Exists(filePath));

            Assert.IsTrue(d.Init(dataStore));

            Assert.AreEqual(1, d.AvailablesPlugins.Count);
            Assert.AreEqual(0, d.InstalledPlugins.Count);

        }

        [TestMethod]
        public void TestPluginsManager()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\pluginsTest\\";
            var d = new PluginsManager(loggerMng, filePath);

            
        }
    }
}
