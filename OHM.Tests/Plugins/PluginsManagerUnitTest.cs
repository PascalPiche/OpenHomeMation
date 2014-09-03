using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Plugins;
using Rhino.Mocks;
using OHM.Logger;
using OHM.Data;
using System.IO;
using OHM.System;
using System.Collections;
using System.Collections.Generic;

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

            dataStore.AssertWasCalled(x => x.Save());
            Assert.AreEqual(2, d.AvailablesPlugins.Count);
            Assert.AreEqual(0, d.InstalledPlugins.Count);

        }

        [TestMethod]
        public void TestPluginsManagerInstall()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("PluginsManager")).Return(logger);
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\pluginsStub1\\";
            var ohmSystem = MockRepository.GenerateStub<IOhmSystem>();
            var d = new PluginsManager(loggerMng, filePath);
            var guid = new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e312");
            var guid2 = new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e314");

            Assert.IsTrue(d.Init(dataStore));

            Assert.AreEqual(2, d.AvailablesPlugins.Count);
            Assert.AreEqual(0, d.InstalledPlugins.Count);

            Assert.IsTrue(d.InstallPlugin(guid, ohmSystem));

            dataStore.AssertWasCalled(x => x.Save());
            Assert.AreEqual(1, d.AvailablesPlugins.Count);
            Assert.AreEqual(1, d.InstalledPlugins.Count);

            var plugin = d.GetPlugin(guid);
            Assert.IsNotNull(plugin);

            Assert.AreEqual(guid, plugin.Id);


            Assert.IsFalse(d.InstallPlugin(guid2, ohmSystem));
            Assert.AreEqual(1, d.AvailablesPlugins.Count);
            Assert.AreEqual(1, d.InstalledPlugins.Count);
            Assert.IsNull(d.GetPlugin(guid2));
        }

        [TestMethod]
        public void TestPluginsManagerLoadRegistered()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("PluginsManager")).Return(logger);
            var dataDic = MockRepository.GenerateStub<IDataDictionary>();
            var listKeys = new List<string>();
            var guidStr = "dd985d5b-2d5e-49b5-9b07-64aad480e312";
            var guid = new Guid(guidStr);
            listKeys.Add(guidStr); //Valid key
            listKeys.Add("dd985d5b-2d5e-49b5-4b07-64aad480e312"); //Invalid key
            dataDic.Stub(x => x.GetKeys()).Return(listKeys);
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            dataStore.Stub(x => x.GetDataDictionary("InstalledPlugins")).Return(dataDic);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\pluginsStub2\\";

            var target = new PluginsManager(loggerMng, filePath);

            Assert.IsTrue(target.Init(dataStore));

            Assert.AreEqual(1, target.AvailablesPlugins.Count);
            Assert.AreEqual(1, target.InstalledPlugins.Count);

            var plugin = target.GetPlugin(guid);
            Assert.IsNotNull(plugin);

        }
    }
}
