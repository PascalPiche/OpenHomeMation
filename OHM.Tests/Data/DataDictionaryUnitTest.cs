using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Data;

namespace OHM.Data.Tests
{
    [TestClass]
    public class DataDictionaryUnitTest
    {
        [TestMethod]
        public void TestDataDictionary()
        {
            var d = new DataDictionary();

            //Check Null value
            Assert.AreEqual("", d.GetString("key"));
            Assert.IsNull(d.GetDataDictionary("key"));

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

            
        }
    }
}
