using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes;
using System;
using System.Collections.ObjectModel;

namespace OHM.Tests
{
    [TestClass]
    public class NodePropertyUnitTest
    {
        
        [TestMethod]
        public void TestNodePropertyConstructorDefault()
        {
            string key = "key1";
            string name = "name1";
            Type type = typeof(Int32);
            NodeProperty target = new NodeProperty(key, name, type);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsTrue(target.ReadOnly);

            Assert.IsTrue(String.IsNullOrEmpty(target.Description));
            Assert.AreEqual(type, target.Type);
            Assert.IsNull(target.Value);
        }

        [TestMethod]
        public void TestNodePropertyConstructorRequiredFalse()
        {
            string key = "key1";
            string name = "name1";
            Type type = typeof(Int32);
            NodeProperty target = new NodeProperty(key, name, type, false);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsFalse(target.ReadOnly);

            Assert.IsTrue(String.IsNullOrEmpty(target.Description));
            Assert.AreEqual(type, target.Type);
            Assert.IsNull(target.Value);
        }

        [TestMethod]
        public void TestNodePropertyConstructorRequiredFalseAndDescription()
        {
            string key = "key1";
            string name = "name1";
            string description = "Description";
            Type type = typeof(Int32);
            NodeProperty target = new NodeProperty(key, name, type, false, description);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsFalse(target.ReadOnly);

            Assert.AreEqual(description, target.Description);
            Assert.AreEqual(type, target.Type);
            Assert.IsNull(target.Value);
        }

        [TestMethod]
        public void TestNodePropertyConstructorRequiredFalseAndDescriptionAndErrorValue()
        {
            string key = "key1";
            string name = "name1";
            string description = "Description";
            Type type = typeof(Int32);
            NodeProperty target = new NodeProperty(key, name, type, false, description, "bla");

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsFalse(target.ReadOnly);

            Assert.AreEqual(description, target.Description);
            Assert.AreEqual(type, target.Type);
            Assert.IsNull(target.Value);
        }

        [TestMethod]
        public void TestNodePropertyConstructorRequiredFalseAndDescriptionAndValidValueAndExtraInfo()
        {
            string key = "key1";
            string name = "name1";
            string description = "Description";
            Type type = typeof(Int32);
            Int32 value = 1;
            ObservableCollection<INodeProperty> extraInfo = new ObservableCollection<INodeProperty>();
            extraInfo.Add(new NodeProperty("extra1", "extra1Name", typeof(string)));

            NodeProperty target = new NodeProperty(key, name, type, false, description, value, extraInfo);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual(name, target.Name);
            Assert.IsFalse(target.ReadOnly);

            Assert.AreEqual(description, target.Description);
            Assert.AreEqual(type, target.Type);
            Assert.AreEqual(value, target.Value);

        }

        // Boolean state for the event trigger
        private bool _CheckPropertyChangedForTestNodePropertySetValueWithNull = false;
        
        [TestMethod]
        public void TestNodePropertySetValueWithNull()
        {
            string key = "key1";
            string name = "name1";
            Type type = typeof(Int32);
            NodeProperty target = new NodeProperty(key, name, type);
            target.PropertyChanged += target_PropertyChanged;

            _CheckPropertyChangedForTestNodePropertySetValueWithNull = false;

            bool result = target.SetValue(null);

            Assert.IsTrue(result);
            Assert.IsTrue(_CheckPropertyChangedForTestNodePropertySetValueWithNull);
        }

        void target_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _CheckPropertyChangedForTestNodePropertySetValueWithNull = true;
        }
    }
}
