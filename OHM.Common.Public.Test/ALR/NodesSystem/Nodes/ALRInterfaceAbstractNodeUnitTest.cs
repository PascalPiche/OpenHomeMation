using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.SYS;
using Rhino.Mocks;

namespace OHM.Tests
{
    [TestClass]
    public class ALRInterfaceAbstractNodeUnitTest
    {
        [TestMethod]
        public void TestALRInterfaceAbstractNodeConstructorDefault()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            
            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);
            
            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.Key);

            Assert.AreEqual(name, target.Name);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(4, target.Properties.Count);

            Assert.AreEqual(NodeStates.initializing, target.State);

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);
            Assert.IsFalse(target.IsRunning);
            Assert.IsFalse(target.StartOnLaunch);

            Assert.IsNull(target.TreeKey);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithNullNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.CanExecuteCommand(null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.CanExecuteCommand("garbage not found", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithSelfKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.CanExecuteCommand(key, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithSelfKeyAndGarbage()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.CanExecuteCommand(key, "garbage command key not found");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithDottedKeyAndGarbage()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.CanExecuteCommand(key + ".test", "garbage command key not found");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.ExecuteCommand(null, null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithSelfKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.ExecuteCommand(key, null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithDottedKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.ExecuteCommand(key + ".test", null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithSelfKeyAndGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.ExecuteCommand(key, "garbage command key", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithDottedKeyAndGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.ExecuteCommand(key + ".test", "garbage command key", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestShutdowning()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            bool result = target.Shutdowning();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestInitWithNullNullNull()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            IDataStore data = null;
            ILogger logger = null;
            IOhmSystemInterfaceGateway sys = null;

            target.Init(data, logger, sys);
        }

        [TestMethod]
        public void TestInitWithInterfaceGatewayAndDataNullLoggerNull()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            IDataStore data = null;
            ILogger logger = null;
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreNotEqual(NodeStates.normal, target.State);
        }

        [TestMethod]
        public void TestInitWithInterfaceGatewayDataAndLoggerNull()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = null;

            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreNotEqual(NodeStates.normal, target.State);
            Assert.AreEqual(NodeStates.initializing, target.State);
        }

        [TestMethod]
        public void TestInitWithInterfaceGatewayLoggerAndDataNull()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            IDataStore data = null;
            ILogger logger = MockRepository.GenerateStub<ILogger>();

            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreNotEqual(NodeStates.normal, target.State);
            Assert.AreEqual(NodeStates.initializing, target.State);

        }

        [TestMethod]
        public void TestInitFalsePropertiesInit()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();

            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreNotEqual(NodeStates.normal, target.State);
            Assert.AreEqual(NodeStates.initializing, target.State);
        }

        [TestMethod]
        public void TestInitTruePropertiesInit()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();

            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);
        }

        [TestMethod]
        public void TestInitWitStartOnLaunchValue()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);
            
               // mng.Stub(x => x.SaveDataStore(d)).Return(true);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            data.Stub(x => x.ContainKey("StartOnLaunch")).Return(true);

            ILogger logger = MockRepository.GenerateStub<ILogger>();

            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);
        }

        [TestMethod]
        public void TestSetInterfaceStateWhenInited()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);

            target.TestSetInterfaceState(ALRInterfaceStates.Enabled);

            Assert.AreEqual(ALRInterfaceStates.Enabled, target.InterfaceState);
        }

        [TestMethod]
        public void TestStartingWhenInited()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            bool result = target.Starting();

            Assert.IsTrue(result);
            Assert.AreEqual(ALRInterfaceStates.Enabled, target.InterfaceState);
        }

        [TestMethod]
        public void TestShutdowningWhenInterfaceEnabled()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            Assert.AreEqual(NodeStates.initializing, target.State);

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            bool result = target.Starting();

            Assert.IsTrue(result);
            Assert.AreEqual(ALRInterfaceStates.Enabled, target.InterfaceState);

            bool result2 = target.Shutdowning();

            Assert.IsTrue(result2);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);
        }

        

        [TestMethod]
        public void TestStartingNotInited()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            target.Starting();

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);
            Assert.AreEqual(NodeStates.initializing, target.State);
        }

        [TestMethod]
        public void TestSetInterfaceStateNotInited()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, false);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            target.TestSetInterfaceState(ALRInterfaceStates.Enabled);

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);
            Assert.AreEqual(NodeStates.initializing, target.State);
        }

        [TestMethod]
        public void TestNodeStateErrorInitSubChild()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub2 target = new ALRInterfaceAbstractNodeStub2(key, name, false);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            target.Init(data, logger, sys);
    
            Assert.AreEqual(NodeStates.error, target.State);
        }

        [TestMethod]
        public void TestSetStartOnLaunchTrueWithInitFatalState()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub2 target = new ALRInterfaceAbstractNodeStub2(key, name, true);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            target.TestSetNodeState(NodeStates.fatal);
            Assert.AreEqual(NodeStates.fatal, target.State);
            Assert.AreEqual(false, target.StartOnLaunch);
            
            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            data.Stub(x => x.ContainKey("StartOnLaunch")).Return(true);
            data.Stub(x => x.GetBool("StartOnLaunch")).Return(true);
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            target.Init(data, logger, sys);
            
            Assert.AreEqual(NodeStates.fatal, target.State);
            Assert.IsFalse(target.StartOnLaunch);
        }

        [TestMethod]
        public void TestSetStartOnLaunchFalseWithInitFatalState()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub2 target = new ALRInterfaceAbstractNodeStub2(key, name, true);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            target.TestSetNodeState(NodeStates.fatal);
            Assert.AreEqual(NodeStates.fatal, target.State);
            Assert.AreEqual(false, target.StartOnLaunch);

            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            data.Stub(x => x.ContainKey("StartOnLaunch")).Return(true);
            data.Stub(x => x.GetBool("StartOnLaunch")).Return(false);
            
            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.fatal, target.State);
            Assert.IsFalse(target.StartOnLaunch);
        }

        //INTEGRATION TEST ALRAbstractTreeNode CreateChildNode Test
        [TestMethod]
        public void TestCreateChildNode()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name, true);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            IDataStore data = MockRepository.GenerateStub<IDataStore>();
            data.Stub(x => x.ContainKey("StartOnLaunch")).Return(true);
            data.Stub(x => x.GetBool("StartOnLaunch")).Return(false);

            ILogger logger = MockRepository.GenerateStub<ILogger>();
            IOhmSystemInterfaceGateway sys = new OhmSystemInterfaceGatewayStub();

            target.Init(data, logger, sys);

            Assert.AreEqual(NodeStates.normal, target.State);
            Assert.IsFalse(target.StartOnLaunch);

        }

        #region Stubs

        private class ALRInterfaceAbstractNodeStub : ALRInterfaceAbstractNode
        {
            private bool _internalTestRegisterProperties = true;

            public ALRInterfaceAbstractNodeStub(string key, string name, bool internalTestRegisterPropertiersResult)
            : base(key, name)
            {
                _internalTestRegisterProperties = internalTestRegisterPropertiersResult;
            }

            public void TestSetInterfaceState(ALRInterfaceStates newState)
            {
                base.InterfaceState = newState;
            }

            protected override void Start()
            {
                //throw new System.NotImplementedException();
            }

            public bool CustomTest1InitSubChild()
            {
                var result = this.CreateChildNode(null, null, null);
                Assert.IsNull(result);

                return true;
            }

            protected override bool InitSubChild()
            {
                return base.InitSubChild() && CustomTest1InitSubChild();
            }

            protected override bool Shutdown()
            {
                return true;
            }

            protected override AbstractPowerNode CreateNodeInstance(string model, string key, string name, System.Collections.Generic.IDictionary<string, object> options)
            {
                //TEMP;
                return null;
                //throw new System.NotImplementedException();
            }

            protected override void RegisterCommands()
            {
                
            }

            protected override bool RegisterProperties()
            {
                return _internalTestRegisterProperties;
            }
        }

        private class ALRInterfaceAbstractNodeStub2 : ALRInterfaceAbstractNode
        {
            //private bool _internalTestRegisterProperties = true;
            private bool _initSubChildResult = true;

            public ALRInterfaceAbstractNodeStub2(string key, string name, bool initSubChildResult)
                : base(key, name)
            {
                _initSubChildResult = initSubChildResult;
            }
            
            public void TestSetNodeState(NodeStates value)
            {
                base.State = value;
            }

            public void TestSetInterfaceState(ALRInterfaceStates newState)
            {
                base.InterfaceState = newState;
            }

            protected override void Start()
            {
                throw new System.NotImplementedException();
            }

            protected override bool InitSubChild()
            {
                //Create Custom child
                return _initSubChildResult && base.InitSubChild();
            }

            protected override bool Shutdown()
            {
                throw new System.NotImplementedException();
            }

            protected override AbstractPowerNode CreateNodeInstance(string model, string key, string name, System.Collections.Generic.IDictionary<string, object> options)
            {
                throw new System.NotImplementedException();
            }

            protected override void RegisterCommands()
            {
                //throw new System.NotImplementedException();
            }

            protected override bool RegisterProperties()
            {
                return true;
            }
        }

        private class OhmSystemInterfaceGatewayStub : IOhmSystemInterfaceGateway
        {
            public bool CreateNode(AbstractPowerTreeNode node)
            {
                throw new System.NotImplementedException();
            }

            public bool RemoveNode(AbstractPowerTreeNode node)
            {
                throw new System.NotImplementedException();
            }
        }

        #endregion
    }
}
