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
        public void TestCommandAbstract_ConstructorDefault()
        {
            string key = "key";
            string name = "name";
            string description = string.Empty;

            CommandDefinition definition = new CommandDefinition(key, name, description);

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(definition);

            // Check definition presence
            Assert.IsNotNull(target.Definition);

            // Check key
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(key, target.Key);

            // Check name
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(name, target.Name);

            // Check description
            Assert.AreEqual(description, target.Definition.Description);
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_CanExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";
            string description = "description";
            CommandDefinition definition = new CommandDefinition(key, name, description);

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(definition);

            //Node not inited
            Assert.IsFalse(target.CanExecute());
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_Execute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";
            string description = "description";
            CommandDefinition definition = new CommandDefinition(key, name, description);

            var target = MockRepository.GeneratePartialMock<AbstractCommand>(definition);

            Assert.IsFalse(target.Execute(null));
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_ProtectedGetNode()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";
            string description = "description";
            CommandDefinition definition = new CommandDefinition(key, name, description);

            var target = new CommandAbstractMock(definition);

            Assert.IsNull(target.ProtectedGetNode());
        }

        [TestMethod]
        public void TestCommandAbstractNotInited_ExecuteWithArgs()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";
            string description = "description";
            CommandDefinition definition = new CommandDefinition(key, name, description);

            var target = new CommandAbstractMock(definition);

            Assert.IsFalse(target.Execute(new Dictionary<string, string>()));
        }

        #region Private Mock for protected member test

        private class CommandAbstractMock : AbstractCommand
        {
            public CommandAbstractMock(ICommandDefinition definition)
                : base(definition)
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
