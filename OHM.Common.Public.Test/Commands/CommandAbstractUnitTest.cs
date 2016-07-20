using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes.Commands;
using System;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class CommandAbstractUnitTest
    {

        [TestMethod]
        public void TestCommandAbstractConstructorDefaultNotInited()
        {
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(key, name); ;

            Assert.IsNotNull(target.Definition);
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);

            try
            {
                var temp = target.NodeFullKey;
                Assert.Fail("Should throw exception. This command is not inited");
            } catch(Exception ex) {
                
            }
        }

        /*[TestMethod]
        public void TestCommandAbstractInit()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(key, name); ;

            Assert.IsNotNull(target.Definition);
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);

            try
            {
                var temp = target.NodeFullKey;
                Assert.Fail("Should throw exception. This command is not inited");
            } catch(Exception ex) {
                
            }
        }*/

        [TestMethod]
        public void TestCommandAbstractCanExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(null, key, name); ;

            Assert.IsTrue(target.CanExecute());
        }

        [TestMethod]
        public void TestCommandAbstractExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(null, key, name); ;

            Assert.IsTrue(target.Execute(null));

        }

        [TestMethod]
        public void TestCommandAbstractExecuteWithArgs()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractIMP(null, key, name); ;

            Assert.IsTrue(target.Execute(new Dictionary<string, string>()));

        }
    }

    public class CommandAbstractIMP : CommandAbstract
    {
        public CommandAbstractIMP(string key, string name)
            : base(key, name) { }

        public CommandAbstractIMP(string key, string name, string description) 
            : base (key, name, description) { }

        public CommandAbstractIMP(string key, string name, string description, Dictionary<string, IArgumentDefinition> argumentsDefinition)
            : base(key, name, description, argumentsDefinition) { }

        protected override bool RunImplementation(IDictionary<string, string> arguments)
        {
            return true;
        }
    }
}
