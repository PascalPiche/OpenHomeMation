using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.Nodes.Commands;
using OHM.Nodes.Commands.ALR;
using OHM.Nodes.Properties;

namespace OHM.Tests
{
    [TestClass]
    public class InterfaceCommandAbstractUnitTest
    {
        [TestMethod]
        public void TestInterfaceCommandStubConstructorDefault()
        {
            string key = "key";
            string name = "name";
            string description = "description";

            InterfaceCommandStub target = new InterfaceCommandStub(key, name, description);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsNull(target.testGetNode());
            Assert.IsNull(target.testGetInterface());
            Assert.IsFalse(target.testIsInterfaceRunning());
            Assert.IsFalse(target.CanExecute());
        }

        private class InterfaceCommandStub : InterfaceCommandAbstract
        {
            public InterfaceCommandStub(string key, string name, string description) : base(key, name, description) { }

            protected override bool RunImplementation(System.Collections.Generic.IDictionary<string, string> arguments)
            {
                throw new System.NotImplementedException();
            }

            public ALRAbstractTreeNode testGetNode()
            {
                return this.Node;
            }

            public bool testIsInterfaceRunning()
            {
                return this.IsInterfaceRunning();
            }

            public ALRInterfaceAbstractNode testGetInterface() {
                return this.Interface;
            }
        }

        /*[TestMethod]
        public void TestALRBasicNodeConstructorDefault()
        {
            string key = "key";
            string name = "name";
            ALRBasicNode target = new ALRBasicNode(key, name);
            
            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);
            
            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.SystemKey);

            Assert.AreEqual(name, target.SystemName);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(3, target.Properties.Count);

            Assert.AreEqual(SystemNodeStates.created, target.SystemState);

            Assert.IsNull(target.TreeKey);
        }

        [TestMethod]
        public void TestALRBasicNodeRegisterCommands()
        {
            string key = "key";
            string name = "name";
            ALRBasicNodeSub target = new ALRBasicNodeSub(key, name);

            target.testRegisterCommands();
            //NOTHING TO TEST....
        }

        [TestMethod]
        public void TestALRBasicNodeRegisterProperties()
        {
            string key = "key";
            string name = "name";
            ALRBasicNodeSub target = new ALRBasicNodeSub(key, name);

            bool result = target.testRegisterProperties();

            //Result true
            Assert.IsTrue(result);
        }

        private class ALRBasicNodeSub : ALRBasicNode
        {
            public ALRBasicNodeSub(string key, string name) : base(key, name) { }

            public void testRegisterCommands()
            {
                base.RegisterCommands();
            }

            public bool testRegisterProperties()
            {
                return base.RegisterProperties();
            }
        }*/
    }
}
