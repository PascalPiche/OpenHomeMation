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

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithNullArgs()
        {
            string key = "key3";
            string name = "name3";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithOneOptionalArgs()
        {
            string key = "key4";
            string name = "name5";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            args.Add("arg1", new ArgumentDefinition("arg1", "arg1Name", typeof(Int32)));

            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(null);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithOneRequiredArgsAndNullParams()
        {
            string key = "key4";
            string name = "name5";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            args.Add("arg1", new ArgumentDefinition("arg1", "arg1Name", typeof(Int32), true));

            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithOneRequiredArgsAndNotPresentKey()
        {
            string key = "key4";
            string name = "name5";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            args.Add("arg1", new ArgumentDefinition("arg1", "arg1Name", typeof(Int32), true));

            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(new Dictionary<string,string>());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithOneRequiredArgsAndPresentKeyWithNullValue()
        {
            string key = "key4";
            string name = "name5";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            args.Add("arg1", new ArgumentDefinition("arg1", "arg1Name", typeof(Int32), true));
            var values = new Dictionary<string, string>();
            values.Add("arg1", null);

            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(values);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCommandDefinitionValidateArgumentsWithOneRequiredArgsAndPresentKeyWithInvalidValue()
        {
            string key = "key4";
            string name = "name5";
            string desc = "Description";
            Dictionary<string, IArgumentDefinition> args = new Dictionary<string, IArgumentDefinition>();
            args.Add("arg1", new ArgumentDefinition("arg1", "arg1Name", typeof(Int32), true));
            var values = new Dictionary<string, string>();
            values.Add("arg1", "toto");

            var target = new CommandDefinition(key, name, desc, args);

            bool result = target.ValidateArguments(values);

            Assert.IsFalse(result);
        }
    }
}
