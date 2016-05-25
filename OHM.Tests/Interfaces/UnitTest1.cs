using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Interfaces;
using Rhino.Mocks;
using OHM.Logger;
using OHM.Plugins;
using OHM.Data;
using System.Collections.Generic;
using OHM.Sys;

namespace OHM.Tests.Interfaces
{
    [TestClass]
    public class InterfacesManagerUnitTest
    {
        [TestMethod]
        public void TestInterfacesManagerConstructor()
        {

            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("InterfacesManager")).Return(logger);

            var pluginsMng = MockRepository.GenerateStub<IPluginsManager>();

            var target = new InterfacesManager(loggerMng, pluginsMng);

            Assert.IsNotNull(target.RunnableInterfaces);
            Assert.AreEqual(0, target.RunnableInterfaces.Count);

        }

        [TestMethod]
        public void TestInterfacesManagerInit()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("InterfacesManager")).Return(logger);

            var pluginsMng = MockRepository.GenerateStub<IPluginsManager>();
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            var system = MockRepository.GenerateStub<IOhmSystemInternal>();

            var target = new InterfacesManager(loggerMng, pluginsMng);

            Assert.IsTrue(target.Init(dataStore, system));
        }

        [TestMethod]
        public void TestInterfacesManagerRegisterInterface()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            var logger2 = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("InterfacesManager")).Return(logger);
            loggerMng.Stub(x => x.GetLogger("dd985d5b-2d5e-49b5-9b07-64aad480e312.testGood")).Return(logger2);

            var pluginsMng = MockRepository.GenerateStub<IPluginsManager>();
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            var dataDic = MockRepository.GenerateStub<IDataDictionary>();
            dataDic.Stub(x => x.Keys).Return(new List<string>());
            dataDic.Stub(x => x.GetDataDictionary("existing")).Return(dataDic);
            dataStore.Stub(x => x.GetDataDictionary("RegisteredInterfaces")).Return(dataDic);
            dataStore.Stub(x => x.Save()).Return(true);
            var plugin = MockRepository.GenerateStub<IPlugin>();
            plugin.Stub(x => x.Id).Return(new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e312"));
            plugin.Stub(x => x.CreateInterface("testGood", logger)).Return(MockRepository.GenerateStub<InterfaceAbstract>("test","test", logger));
            var system = MockRepository.GenerateStub<IOhmSystemInternal>();

            var target = new InterfacesManager(loggerMng, pluginsMng);
            Assert.AreEqual(0, target.RunnableInterfaces.Count);

            Assert.IsTrue(target.Init(dataStore, system));
            Assert.AreEqual(0, target.RunnableInterfaces.Count);

            //Registering will fail
            Assert.IsFalse(target.RegisterInterface("existing", plugin, system));

            //Registering will fail
            Assert.IsFalse(target.RegisterInterface("test", plugin, system));

            //Registering will succeed
            Assert.IsTrue(target.RegisterInterface("testGood", plugin, system));

            //Registering will succeed
            plugin.AssertWasCalled(x => x.CreateInterface("testGood", logger2));
            Assert.AreEqual(1, target.RunnableInterfaces.Count);

        }

        [TestMethod]
        public void TestInterfacesManagerLoadRegistered()
        {
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var logger = MockRepository.GenerateStub<ILogger>();
            loggerMng.Stub(x => x.GetLogger("InterfacesManager")).Return(logger);

            var pluginsMng = MockRepository.GenerateStub<IPluginsManager>();
            var dataStore = MockRepository.GenerateStub<IDataStore>();
            var dataDic = MockRepository.GenerateStub<IDataDictionary>();
            var listKeys = new List<string>();
            listKeys.Add("test"); //Valid key, valid plugin id, plugin found
            listKeys.Add("test2"); //valid key, valid plugin id, plugin not found
            listKeys.Add("test3"); //valid key, no plugin id
            listKeys.Add("test4"); //invalid key
            dataDic.Stub(x => x.Keys).Return(listKeys);
            dataStore.Stub(x => x.GetDataDictionary("RegisteredInterfaces")).Return(dataDic);
            var plugin = MockRepository.GenerateStub<IPlugin>();

            //Case 1 (Test)
            var dataDic2 = MockRepository.GenerateStub<IDataDictionary>();
            dataDic.Stub(x => x.GetDataDictionary("test")).Return(dataDic2);
            dataDic2.Stub(x => x.GetString("PluginId")).Return("dd985d5b-2d5e-49b5-9b07-64aad480e312");
            var guid = new Guid("dd985d5b-2d5e-49b5-9b07-64aad480e312");
            pluginsMng.Stub(x => x.GetPlugin(guid)).Return(plugin);
            var system = MockRepository.GenerateStub<IOhmSystemInternal>();

            var target = new InterfacesManager(loggerMng, pluginsMng);
            Assert.AreEqual(0, target.RunnableInterfaces.Count);

            Assert.IsTrue(target.Init(dataStore, system));


            Assert.AreEqual(1, target.RunnableInterfaces.Count);
            

        }
    }
}
