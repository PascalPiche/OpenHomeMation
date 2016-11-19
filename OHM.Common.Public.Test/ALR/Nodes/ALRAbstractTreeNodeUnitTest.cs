using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.Nodes.Commands;
using OHM.Nodes.Properties;

namespace OHM.Tests
{
    [TestClass]
    public class ALRAbstractTreeNodeUnitTest
    {

        [TestMethod]
        public void TestALRAbstractTreeNodeConstructorDefault()
        {
            string key = "key";
            string name = "name";
            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);
            
            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);
            
            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.Key);

            Assert.AreEqual(name, target.Name);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(3, target.Properties.Count);

            Assert.AreEqual(NodeStates.initializing, target.State);

            Assert.IsNull(target.TreeKey);
        }

        [TestMethod]
        public void TestContainsPropertyBaseValue()
        {
            string key = "key";
            string name = "name";

            
            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            Assert.IsTrue(target.TestContainProperty("system-key"));

            Assert.IsTrue(target.TestContainProperty("system-name"));
            Assert.IsTrue(target.TestContainProperty("system-node-state"));

            //Assert.IsFalse(target.CanExecuteCommand("wrong", "test"));
        }

        [TestMethod]
        public void TestCreateChildNodeWhenNotInited()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            string childModel = "model";
            string childKey = "clef";
            string childName = "nom";

            AbstractPowerTreeNode result = target.TestCreateChildNode(childModel, childKey, childName);
            Assert.IsNull(result);
        }

         [TestMethod]
        public void TestDataStorePropertyWhenNotInited()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            IDataStore result = target.TestDataStoreProperty();

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestInterfacePropertyWhenNotInited()
        {
             string key = "key";
             string name = "name";

             ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

             ALRInterfaceAbstractNode result = target.TestInterfaceProperty();

             Assert.IsNull(result);
        }

        [TestMethod]
        public void TestLoggerPropertyWhenNotInited()
        {
             string key = "key";
             string name = "name";

             ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

             ILogger result = target.TestLoggerProperty();

             Assert.IsNull(result);
        }

        private bool TestSetStateTriggerProperty = false;

        [TestMethod]
        public void TestSetState()
        {
             string key = "key";
             string name = "name";

             ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

             target.PropertyChanged += target_PropertyChanged;
             TestSetStateTriggerProperty = false;
             target.SetState(NodeStates.error);

             Assert.AreEqual(NodeStates.error, target.State);
             Assert.IsTrue(TestSetStateTriggerProperty);
        }

        void target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
             if (e.PropertyName == "State")
             {
                 TestSetStateTriggerProperty = true;
             }
        }

        [TestMethod]
        public void TestUpdateProperty()
        {
             string key = "key";
             string name = "name";

             ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

             target.PropertyChanged += target_PropertyChanged;
             TestSetStateTriggerProperty = false;
             bool result = target.UpdateProperty("unknow", null);

             Assert.AreEqual(NodeStates.initializing, target.State);

             Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestUnRegisterPropertySystem()
        {

            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            bool result = target.UnRegisterProperty("system-key");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestUnRegisterPropertyWithNull()
        {

            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            bool result = target.UnRegisterProperty(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestUnRegisterProperty() {

            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            bool result = target.UnRegisterProperty("unknown");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestUnRegisterPropertyFunctionnal()
        {

            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);
            target.RegisterProperty("my-test", "test");
            bool result = target.UnRegisterProperty("my-test");

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestRegisteCommandWithNull()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            bool result = target.RegisterCommand(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestRegisteCommandWithValid()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);
            AbstractCommandStub cmd = new AbstractCommandStub(new CommandDefinition("keyCmd", "nameCmd"));
            bool result = target.RegisterCommand(cmd);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestInitSubChild()
        {
            string key = "key";
            string name = "name";

            ALRAbstractTreeNodeStub target = new ALRAbstractTreeNodeStub(key, name);

            bool result = target.InitSubChild();

            Assert.IsTrue(result);
        }

        private class AbstractCommandStub : AbstractCommand
        {
            public AbstractCommandStub(ICommandDefinition definition)
                : base(definition)
            {

            }

            protected override bool RunImplementation(System.Collections.Generic.IDictionary<string, string> arguments)
            {
                throw new System.NotImplementedException();
            }
        }

        private class ALRAbstractTreeNodeStub : ALRAbstractTreeNode
        {
            public ALRAbstractTreeNodeStub(string key, string name)
                : base(key, name)
            {
                
            }

            public ILogger TestLoggerProperty()
            {
                return base.Logger;
            }

            public ALRInterfaceAbstractNode TestInterfaceProperty()
            {
                return base.Interface;
            }

            public IDataStore TestDataStoreProperty()
            {
                return base.DataStore;
            }

            public AbstractPowerTreeNode TestCreateChildNode(string model, string key, string name)
            {
                return base.CreateChildNode(model, key, name, null);
            }

            public bool TestContainProperty(string key)
            {
                return base.ContainProperty(key);
            }

            public void SetState(NodeStates newState)
            {
                base.State = newState;
            }

            public new bool UpdateProperty(string key, object value)
            {
                return base.UpdateProperty(key, value);
            }

            public new bool UnRegisterProperty(string key)
            {
                return base.UnRegisterProperty(key);
            }

            public bool RegisterProperty(string key, string name)
            {
                INodeProperty p = new NodeProperty(key, name, typeof(object));
                return base.RegisterProperty(p);
            }

            public new bool RegisterCommand(AbstractCommand cmd)
            {
                return base.RegisterCommand(cmd);
            }

            public bool InitSubChild()
            {
                return base.InitSubChild();
            }

            protected override void RegisterCommands()
            {
                throw new System.NotImplementedException();
            }

            protected override bool RegisterProperties()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
