using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataDictionaryUnitTest
    {
        [TestMethod]
        public void TestDataDictionaryDefaultConstructor()
        {
            var d = new DataDictionary();
            
            Assert.IsNotNull(d.Keys);
            Assert.IsNull(d.Keys.GetEnumerator().Current);
        }

        [TestMethod]
        public void TestDataDictionaryContainKeyNotFound()
        {
            var d = new DataDictionary();

            bool result = d.ContainKey("notFound");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestDataDictionaryStoreGetBool()
        {
            var d = new DataDictionary();

            bool resultFalse = d.ContainKey("testFalse");
            bool resultTrue = d.ContainKey("testTrue");

            Assert.IsFalse(resultFalse);
            Assert.IsFalse(resultTrue);

            Assert.IsFalse(d.GetBool("testFalse"));
            Assert.IsFalse(d.GetBool("testTrue"));

            d.StoreBool("testFalse", false);
            Assert.IsFalse(d.GetBool("testFalse"));

            d.StoreBool("testTrue", true);
            Assert.IsTrue(d.GetBool("testTrue"));
        }

        [TestMethod]
        public void TestDataDictionary()
        {
            var d = new DataDictionary();

            IDataDictionary result = d.GetDataDictionary("NotFound");
            Assert.IsNull(result);

            IDataDictionary result2 = d.GetOrCreateDataDictionary("newDict");
            Assert.IsNotNull(result2);

            IDataDictionary result3 = d.GetDataDictionary("newDict");
            Assert.IsNotNull(result3);
            Assert.AreEqual(result2, result3);
        }

        [TestMethod]
        public void TestDataDictionaryStoreGetString()
        {
            var d = new DataDictionary();

            bool resultFalse = d.ContainKey("test1");

            Assert.IsFalse(resultFalse);

            Assert.AreEqual("", d.GetString("test1"));

            d.StoreString("test1", "test1");
            Assert.AreEqual("test1", d.GetString("test1"));
        }

        [TestMethod]
        public void TestDataDictionaryStoreGetInt32()
        {
            var d = new DataDictionary();

            bool resultFalse = d.ContainKey("testInt");
            Assert.IsFalse(resultFalse);

            Assert.AreEqual(-1, d.GetInt32("testInt"));

            d.StoreInt32("testInt", 24);
            Assert.AreEqual(24, d.GetInt32("testInt"));
        }

        [TestMethod]
        public void TestDataDictionaryRemoveKey()
        {
            var d = new DataDictionary();

            bool result1 = d.ContainKey("testFalse");
            Assert.IsFalse(result1);

            bool result2 = d.RemoveKey("testFalse");
            Assert.IsFalse(result2);

            d.StoreBool("testRemove", true);
            bool result3 = d.ContainKey("testRemove");
            Assert.IsTrue(result3);

            bool result4 = d.RemoveKey("testRemove");
            Assert.IsTrue(result4);

            Assert.IsFalse(d.ContainKey("testRemove"));
        }

        /*[TestMethod]
        public void TestDataDictionary()
        {
            var d = new DataDictionary();
            //Store string
            d.StoreString("key", "value");

            //Try to get string with dataDictionary type (Should not be allowed)
            try
            {
                Assert.IsNull(d.GetDataDictionary("key"));
                Assert.Fail("Should throw invalidCastException");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidCastException));
            }
            
            //Make sure set of string is ok
            Assert.AreEqual("value", d.GetString("key"));

            //Set other string
            d.StoreString("key2", "value2");

            //Make sure string 2 is the right one
            Assert.AreEqual("value2", d.GetString("key2"));

            //Store Dictionary
            d.StoreDataDictionary("key3", new DataDictionary());
            Assert.IsNotNull(d.GetDataDictionary("key3"));
        }*/
    }
}
