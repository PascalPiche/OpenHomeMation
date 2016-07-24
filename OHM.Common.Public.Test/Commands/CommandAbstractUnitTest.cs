using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Common.Public.Test.Commands.Stubs;
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

            var target = new CommandAbstractStub(key, name); ;

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

            var target = new CommandAbstractStub(key, name); ;

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
        public void TestCommandAbstractNotInitedCanExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractStub(null, key, name); ;

            //Node not inited
            Assert.IsFalse(target.CanExecute());
        }

        [TestMethod]
        public void TestCommandAbstractNotInitedExecute()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractStub(null, key, name); ;

            Assert.IsFalse(target.Execute(null));
        }

        [TestMethod]
        public void TestCommandAbstractNotInitedExecuteWithArgs()
        {
            //Should not be valid with a null Node
            string key = "key";
            string name = "name";

            var target = new CommandAbstractStub(null, key, name); ;

            Assert.IsFalse(target.Execute(new Dictionary<string, string>()));

        }
    }
}
