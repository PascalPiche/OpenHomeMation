using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Plugins;
using Rhino.Mocks;
using OHM.Logger;

namespace OHM.Tests.Plugins
{
    [TestClass]
    public class PluginsManagerUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var logger = MockRepository.GenerateStub<ILoggerManager>();
            var d = new PluginsManager(logger);


        }
    }
}
