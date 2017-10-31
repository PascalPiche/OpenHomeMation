using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes;
using OHM.Nodes.Commands;
using Rhino.Mocks;
using System.Collections.Generic;

namespace OHM.Common.Public.Test.Commands
{
    /*
    [TestClass]
    public class AbstractTreeCommandUnitTest
    {
        [TestMethod]
        public void TestCommandAbstractTree_ConstructorDefault()
        {
            string key = "key";
            string name = "name";
            //Expected as default description
            string description = string.Empty;

            var target = MockRepository.GeneratePartialMock<AbstractTreeCommand>(key, name);

            //Check definition presence
            Assert.IsNotNull(target.Definition);
            
            //Check key
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(key, target.Key);

            // Check name
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(name, target.Name);

            // Check description
            Assert.AreEqual(description, target.Definition.Description);

            // check NodeTreeKey (Not attached. should return string.empty
            Assert.AreEqual(string.Empty, target.NodeTreeKey);
        }

        [TestMethod]
        public void TestCommandAbstractTree_ConstructorWithDescription()
        {
            string key = "key";
            string name = "name";
            string description = "this is a description";

            var target = MockRepository.GeneratePartialMock<AbstractTreeCommand>(key, name, description);

            //Check definition presence
            Assert.IsNotNull(target.Definition);

            //Check key
            Assert.AreEqual(key, target.Definition.Key);
            Assert.AreEqual(key, target.Key);

            // Check name
            Assert.AreEqual(name, target.Definition.Name);
            Assert.AreEqual(name, target.Name);

            // Check description
            Assert.AreEqual(description, target.Definition.Description);

            // check NodeTreeKey (Not attached. should return string.empty
            Assert.AreEqual(string.Empty, target.NodeTreeKey);
        }
    }
     */
}
