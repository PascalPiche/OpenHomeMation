using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Common.Public.Test.Commands.Stubs;
using OHM.Common.Public.Test.Nodes.Stubs;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class CommandAbstractIntegrationTest
    {
        /*[TestMethod]
        public void TestInitIntegration()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub holder = new RalNodeAbstractStub(key, name);
            TreeCommandAbstractStub target = new TreeCommandAbstractStub("key", "name", "", null);

            //Assert.IsNull(target.GetAssignedNodeForTest());
            bool result = false;

            result = holder.TestRegisterCommand(target);

            Assert.IsTrue(result);
            //Assert.IsNotNull(target.GetAssignedNodeForTest());
            //Assert.AreEqual(holder, target.GetAssignedNodeForTest());
            Assert.IsNull(target.NodeTreeKey);

        }

        [TestMethod]
        public void TestCanExecuteIntegration()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub holder = new RalNodeAbstractStub(key, name);
            TreeCommandAbstractStub target = new TreeCommandAbstractStub("key", "name", "", null);

            //Assert.IsNull(target.GetAssignedNodeForTest());
            bool result = false;

            Assert.IsFalse(target.CanExecute());

            result = holder.TestRegisterCommand(target);

            Assert.IsTrue(result);
            Assert.IsTrue(target.CanExecute());
        }

        [TestMethod]
        public void TestExecuteIntegrationWithoutArgs()
        {
            string key = "key";
            string name = "name";

            RalNodeAbstractStub holder = new RalNodeAbstractStub(key, name);
            TreeCommandAbstractStub target = new TreeCommandAbstractStub("key", "name", "", null);

            //Assert.IsNull(target.GetAssignedNodeForTest());
            bool result = false;

            Assert.IsFalse(target.CanExecute());

            result = holder.TestRegisterCommand(target);

            Assert.IsTrue(result);
            Assert.IsTrue(target.CanExecute());

            Assert.IsTrue(target.Execute(null));
        }
         */
    }
}
