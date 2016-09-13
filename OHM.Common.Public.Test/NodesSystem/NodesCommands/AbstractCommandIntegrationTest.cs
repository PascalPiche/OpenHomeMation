using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OHM.Common.Public.Test.Commands
{
    [TestClass]
    public class AbstractCommandIntegrationTest
    {
        /*[TestMethod]
        public void TestInitIntegration()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub holder = new ALRAbstractTreeNodeStub(key, name);
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

            ALRAbstractTreeNodeStub holder = new ALRAbstractTreeNodeStub(key, name);
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

            ALRAbstractTreeNodeStub holder = new ALRAbstractTreeNodeStub(key, name);
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
