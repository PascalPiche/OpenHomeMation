using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Common.Public.Test.Commands.Stubs;
using OHM.Common.Public.Test.Nodes.Stubs;
using OHM.Nodes;
using System;
using System.Collections.ObjectModel;

namespace OHM.Tests
{
    [TestClass]
    public class NodeAbstractUnitTest
    {

        [TestMethod]
        public void TestNodeAbstractConstructorDefault()
        {
            string key = "key";
            string name = "name";
            RalNodeAbstractStub target = new RalNodeAbstractStub(key, name);

            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);

            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.Key);

            Assert.AreEqual(name, target.Name);

            //Assert.IsNull(target.Parent);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(0, target.Properties.Count);


            Assert.IsNull(target.FullKey);

            Assert.AreEqual(NodeStates.initializing, target.State);
            
        }

        /*[TestMethod]
        public void TestCanExecuteCommandWithInvalidKey()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub target = new RalNodeAbstractStub(key, name);

            Assert.IsFalse(target.CanExecuteCommand("test"));
        }
        */
        /*
        [TestMethod]
        public void TestCanExecuteCommandWithValidKey()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub target = new RalNodeAbstractStub(key, name);
            target.TestRegisterCommand(new CommandAbstractStub("key2", "name2"));

            Assert.IsTrue(target.CanExecuteCommand("key2"));
        }
        */

        /*[TestMethod]
        public void TestExecuteCommandWithInvalidKey()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub target = new RalNodeAbstractStub(key, name);

            Assert.IsFalse(target.ExecuteCommand(key, "test", null));
        }

        [TestMethod]
        public void TestExecuteCommandWithValidKey()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub target = new RalNodeAbstractStub(key, name);
            target.TestRegisterCommand(new CommandAbstractStub("key2", "name2"));

            Assert.IsTrue(target.ExecuteCommand(key, "key2", null));
        }*/
    }
}
