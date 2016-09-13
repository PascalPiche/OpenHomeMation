using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.Nodes.Commands;

namespace OHM.Tests
{
    [TestClass]
    public class ALRAbstractTreeNodeUnitTest
    {

        [TestMethod]
        public void TestALRAbstractTreeNodeConstructorDefault()
        {
            string key = "key";
            string name = "name";
            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);

            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.Key);

            Assert.AreEqual(name, target.Name);

            //Assert.IsNull(target.Parent);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(0, target.Properties.Count);


            Assert.IsNull(target.TreeKey);

            Assert.AreEqual(NodeStates.initializing, target.State);
            
        }

        /*[TestMethod]
        public void TestCanExecuteCommandWithInvalidKey()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            Assert.IsFalse(target.CanExecuteCommand("test"));
        }
        */
        /*
        [TestMethod]
        public void TestCanExecuteCommandWithValidKey()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);
            target.TestRegisterCommand(new CommandAbstractStub("key2", "name2"));

            Assert.IsTrue(target.CanExecuteCommand("key2"));
        }
        */

        /*[TestMethod]
        public void TestExecuteCommandWithInvalidKey()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            Assert.IsFalse(target.ExecuteCommand(key, "test", null));
        }

        [TestMethod]
        public void TestExecuteCommandWithValidKey()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);
            target.TestRegisterCommand(new CommandAbstractStub("key2", "name2"));

            Assert.IsTrue(target.ExecuteCommand(key, "key2", null));
        }*/

        private class ALRAbstractTreeNodeStub : ALRAbstractTreeNode
        {
        public ALRAbstractTreeNodeStub(string key, string name) 
            : base(key, name)
        {}

        public bool TestRegisterCommand(AbstractCommand command)
        {
            return this.RegisterCommand(command);
        }

        protected override void RegisterCommands()
        {
            //throw new System.NotImplementedException();
        }

        protected override bool RegisterProperties()
        {
            throw new System.NotImplementedException();
        }
    }
    }
}
