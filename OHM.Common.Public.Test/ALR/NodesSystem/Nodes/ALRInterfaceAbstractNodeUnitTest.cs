using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;
using OHM.Logger;
using OHM.Nodes;
using OHM.Nodes.ALR;
using OHM.SYS;

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
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);
            
            Assert.IsNotNull(target.Children);
            Assert.AreEqual(0, target.Children.Count);
            
            Assert.IsNotNull(target.Commands);
            Assert.AreEqual(0, target.Commands.Count);

            Assert.AreEqual(key, target.Key);

            Assert.AreEqual(name, target.Name);

            Assert.IsNotNull(target.Properties);
            Assert.AreEqual(3, target.Properties.Count);

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
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.CanExecuteCommand(null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.CanExecuteCommand("garbage not found", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithSelfKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.CanExecuteCommand(key, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithSelfKeyAndGarbage()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.CanExecuteCommand(key, "garbage command key not found");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestCanExecuteCommandWithDottedKeyAndGarbage()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.CanExecuteCommand(key + ".test", "garbage command key not found");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.ExecuteCommand(null, null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithSelfKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.ExecuteCommand(key, null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithDottedKeyAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.ExecuteCommand(key + ".test", null, null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithSelfKeyAndGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.ExecuteCommand(key, "garbage command key", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestExecuteCommandWithDottedKeyAndGarbageAndNull()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.ExecuteCommand(key + ".test", "garbage command key", null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestShutdowning()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            bool result = target.Shutdowning();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestInit()
        {
            string key = "key";
            string name = "name";

            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);
            IDataStore data = null;
            ILogger logger = null;
            IOhmSystemInterfaceGateway sys = null;

            target.Init(data, logger, sys);
        }


        [TestMethod]
        public void TestStartingNotInited()
        {
            string key = "key";
            string name = "name";
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

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
            ALRInterfaceAbstractNodeStub target = new ALRInterfaceAbstractNodeStub(key, name);

            Assert.AreEqual(NodeStates.initializing, target.State);
            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);

            target.TestSetInterfaceState(ALRInterfaceStates.Enabled);

            Assert.AreEqual(ALRInterfaceStates.Disabled, target.InterfaceState);
            Assert.AreEqual(NodeStates.initializing, target.State);
        }

        

        #region Stubs

        private class ALRInterfaceAbstractNodeStub : ALRInterfaceAbstractNode
        {
            public ALRInterfaceAbstractNodeStub(string key, string name)
            : base(key, name)
            { }

            public void TestSetInterfaceState(ALRInterfaceStates newState)
            {
                base.InterfaceState = newState;
            }

            protected override void Start()
            {
                throw new System.NotImplementedException();
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
                
            }

            protected override bool RegisterProperties()
            {
                return true;
            }
        }

        #endregion
    }
}
