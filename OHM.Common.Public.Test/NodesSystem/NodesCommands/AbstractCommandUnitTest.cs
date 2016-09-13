using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes;
using OHM.Nodes.Commands;
using Rhino.Mocks;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class AbstractCommandUnitTest
    {
        [TestMethod]
        public void TestCommandAbstractNotInited_ConstructorDefault()
        {
            string key = "key";
            string name = "name";

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(key, name);

            Assert.IsNotNull(target.Definition);
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_CanExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(key, name);

            //Node not inited
            Assert.IsFalse(target.CanExecute());
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_Execute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(key, name);

            Assert.IsFalse(target.Execute(null));
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_ProtectedGetNode()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractMock(key, name);

            Assert.IsNull(target.ProtectedGetNode());
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_ExecuteWithArgs()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractMock(key, name); ;

            Assert.IsFalse(target.Execute(new Dictionary<string, string>()));
        }

        #region Private Mock for protected member test

        private class CommandAbstractMock : AbstractCommand
        {
            public CommandAbstractMock(string key, string name)
                : base(key, name)
            { }

            public ICommandsNode ProtectedGetNode()
            {
                return this.Node;
            }

            protected override bool RunImplementation(IDictionary<string, string> arguments)
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion
    }
}
