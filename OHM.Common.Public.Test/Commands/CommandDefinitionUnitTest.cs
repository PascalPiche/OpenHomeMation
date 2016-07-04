using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Commands;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class CommandDefinitionUnitTest
    {
        [TestMethod]
        public void TestCommandDefinitionConstructorDefault()
        {
            string key = "key";
            string name = "name";
            var target = new CommandDefinition(key, name);

            //Passed value
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);

            //Default value
            Assert.AreEqual(String.Empty, target.Description);
            Assert.IsNotNull(target.ArgumentsDefinition);
            Assert.AreEqual(0, target.ArgumentsDefinition.Count);
        }

        [TestMethod]
        public void TestCommandDefinitionConstructorWithDescription()
        {
            string key = "key2";
            string name = "name2";
            string desc = "Description";
            var target = new CommandDefinition(key, name, desc);

            //Passed value
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(desc, target.Description);

            //Default value
            Assert.IsNotNull(target.ArgumentsDefinition);
            Assert.AreEqual(0, target.ArgumentsDefinition.Count);
        }

        [TestMethod]
        public void TestCommandDefinitionConstructorWithArgumentsDefinition()
        {
            string key = "key2";
            string name = "name2";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            var target = new CommandDefinition(key, name, desc, args);

            //Passed value
            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(desc, target.Description);
            Assert.AreSame(args, target.ArgumentsDefinition);

            //Default value
            Assert.IsNotNull(target.ArgumentsDefinition);
            Assert.AreEqual(0, target.ArgumentsDefinition.Count);
        }
    }
}
