using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes.ALR;
using OHM.Nodes.ALV;
using OHM.Plugins;
using OHM.SYS;
using Rhino.Mocks;
using System;

namespace OHM.Tests
{
    [TestClass]
    public class PluginBaseUnitTest
    {
        [TestMethod]
        public void TestPluginBase_Constructor_Default()
        {
            PluginBase target = MockRepository.GeneratePartialMock<PluginBase>();
            Assert.AreEqual(PluginStates.Ready, target.State);
        }
        
        [TestMethod]
        public void TestPluginBase_SetState_Error()
        {
            PluginBaseStub target = new PluginBaseStub();
            target.SetStateTest(PluginStates.Error);
            Assert.AreEqual(PluginStates.Error, target.State);
        }

        [TestMethod]
        public void TestPluginBase_SetState_NotFound()
        {
            PluginBaseStub target = new PluginBaseStub();
            try {
                target.SetStateTest(PluginStates.NotFound);
                Assert.Fail("Should Throw error");
            }
            catch (Exception ex) {
                Assert.IsNotNull(ex);
            }
            Assert.AreEqual(PluginStates.Ready, target.State);
        }

        private class PluginBaseStub : PluginBase
        {
            public override Guid Id
            {
                get { throw new NotImplementedException(); }
            }

            public override string Name
            {
                get { throw new NotImplementedException(); }
            }

            public override bool Install(IOhmSystemInstallGateway system)
            {
                throw new NotImplementedException();
            }

            public override bool Uninstall(IOhmSystemUnInstallGateway system)
            {
                throw new NotImplementedException();
            }

            public override bool Update(IOhmSystemInstallGateway system)
            {
                throw new NotImplementedException();
            }

            public override ALRInterfaceAbstractNode CreateInterface(string key)
            {
                throw new NotImplementedException();
            }

            public void SetStateTest(PluginStates state)
            {
                this.State = state;
            }

            public override IVrType CreateVrNode(string key)
            {
                throw new NotImplementedException();
            }
        }
    }
}
