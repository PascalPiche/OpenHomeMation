using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Plugins;
using OHM.RAL;
using OHM.SYS;
using OHM.VAL;
using System;

namespace OHM.Tests
{
    [TestClass]
    public class PluginBaseUnitTest
    {

        [TestMethod]
        public void TestNodePropertyConstructorDefault()
        {
            PluginBase target = new PluginBaseStub();
            Assert.AreEqual(PluginStates.Normal, target.State);
        }

        [TestMethod]
        public void TestNodePropertySetState()
        {
            PluginBaseStub target = new PluginBaseStub();

            target.SetStateTest(PluginStates.Error);
            Assert.AreEqual(PluginStates.Error, target.State);
        }

        [TestMethod]
        public void TestNodePropertySetStateNotFound()
        {
            PluginBaseStub target = new PluginBaseStub();
            try {
                target.SetStateTest(PluginStates.NotFound);
                Assert.Fail("Should Throw error");
            }
            catch (Exception ex) {

            }
            Assert.AreEqual(PluginStates.Normal, target.State);
        }

    }

    internal class PluginBaseStub : PluginBase
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
