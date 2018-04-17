using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.Nodes.Commands;
using OHM.Nodes.Properties;

namespace OHM.Tests
{
    [TestClass]
    public class ALRBasicNodeUnitTest
    {
        [TestMethod]
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
        }
    }
}
