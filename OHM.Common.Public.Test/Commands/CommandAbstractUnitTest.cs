using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Commands;
using OHM.Nodes;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class CommandAbstractUnitTest
    {

        [TestMethod]
        public void TestCommandAbstractConstructorDefaultError()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(null, key, name); ;

            Assert.IsNotNull(target.Definition);
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(name, target.Definition.Name);
        }

        [TestMethod]
        public void TestCommandAbstractCanExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(null, key, name); ;

            Assert.IsTrue(target.CanExecute());
        }
    }

    public class CommandAbstractIMP : CommandAbstract
    {
        public CommandAbstractIMP(INode node, string key, string name)
            : base(node, key, name) { }

        public CommandAbstractIMP(INode node, string key, string name, string description) 
            : base (node, key, name, description) { }

        public CommandAbstractIMP(INode node, string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(node, key, name, description, argumentsDefinition) { }

        protected override bool RunImplementation(System.Collections.Generic.Dictionary<string, object> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
