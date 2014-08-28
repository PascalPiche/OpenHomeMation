using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.System;
using Rhino.Mocks;
using OHM.Interfaces;
using OHM.Plugins;
using OHM.Logger;

namespace OHM.System.Tests
{
    [TestClass]
    public class OhmSystemUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var intMng = MockRepository.GenerateMock<IInterfacesManager>();
            var loggerMng = MockRepository.GenerateStub<ILoggerManager>();
            
            var target = new OhmSystem(intMng, loggerMng);
            var plugin = MockRepository.GenerateStub<IPlugin>();
            var logger = MockRepository.GenerateStub<ILogger>();
            plugin.Stub(x => x.Name).Return("name");
            loggerMng.Stub(x => x.GetLogger(plugin.Name)).Return(logger);
            
            var result = target.GetInstallGateway(plugin);

            Assert.IsNotNull(result);

            result.RegisterInterface("key");
            Assert.AreSame(logger, result.Logger);
            target.InterfacesMng.AssertWasCalled(x => x.RegisterInterface("key", plugin));

        }
    }
}
