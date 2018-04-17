using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Logger;
using OHM.Managers.ALR;
using OHM.Managers.ALV;
using OHM.Managers.Plugins;
using OHM.Plugins;
using OHM.SYS;
using Rhino.Mocks;

namespace OHM.Sys.Tests
{
    [TestClass]
    public class OhmSystemUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var intMng = MockRepository.GenerateMock<IInterfacesManager>();
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            var dataMng = MockRepository.GenerateStub<IDataManager>();
            var vrMng = MockRepository.GenerateStub<IVrManager>();
            var pluginsMng = MockRepository.GenerateStub<IPluginsManager>();

            var target = new OhmSystem(intMng, vrMng, loggerMng, dataMng, pluginsMng);

            var plugin = MockRepository.GenerateStub<IPlugin>();
            var logger = MockRepository.GenerateStub<ILog>();
            plugin.Stub(x => x.Name).Return("name");
            loggerMng.Stub(x => x.GetLogger(plugin.Name)).Return(logger);
            //var system = MockRepository.GenerateStub<OhmSystem>();

            var result = target.GetInstallGateway(plugin);

            Assert.IsNotNull(result);

            result.RegisterInterface("key");
            Assert.AreSame(logger, result.Logger);

            //TODO : TO FIX
            //target.InterfacesMng.AssertWasCalled(x => x.RegisterInterface("key", plugin));

        }
    }
}
