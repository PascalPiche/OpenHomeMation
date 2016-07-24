using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Nodes.Commands;
using System;
using System.Collections.Generic;

namespace OHM.Tests
{
    [TestClass]
    public class ArgurmentDefinitionUnitTest
    {
        
        [TestMethod]
        public void TestArgumentDefinitionConstructorDefault()
        {
            string key = "key";
            var target = new ArgumentDefinition(key, "name", typeof(string));

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual("name", target.Name);
            Assert.AreEqual(false, target.Required);
            Assert.AreEqual(typeof(string), target.Type);
            Assert.AreEqual(false, target.Required);
        }

        [TestMethod]
        public void TestArgumentDefinitionConstructorRequired()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            Assert.AreEqual(key, target.Key);
            Assert.AreEqual("toto", target.Name);
            Assert.AreEqual(true, target.Required);
            Assert.AreEqual(typeof(Int32), target.Type);
        }



        [TestMethod]
        public void TestArgumentDefinitionTryGetBoolWithBool()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(bool), true);
            bool targetValue = true;
            Boolean result = false;
            bool outValue = true;

            // Test
            result = target.TryGetBool(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetBoolWithString()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(bool), true);
            string inputValue = "true";
            bool targetValue = true;

            Boolean result = false;
            bool outValue = false;

            // Test
            result = target.TryGetBool(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetBoolWithInvalidString()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(bool), true);
            string inputValue = "abc";
            bool targetValue = false;

            Boolean result = false;
            bool outValue = false;

            // Test
            result = target.TryGetBool(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }




        [TestMethod]
        public void TestArgumentDefinitionTryGetDoubleWithDouble()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(double), true);

            double targetValue = 25.756;

            Boolean result = false;
            double outValue;

            // Test
            result = target.TryGetDouble(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetDoubleWithString()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(double), true);
            string inputValue = "24.75";
            double targetValue = 24.75;

            Boolean result = false;
            Double outValue;

            // Test
            result = target.TryGetDouble(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetDoubleWithInvalidString()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(double), true);
            string inputValue = "abc";
            double targetValue = 0;

            Boolean result = false;
            Double outValue;

            // Test
            result = target.TryGetDouble(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }





        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt16WithUInt16()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt16), true);
            UInt16 targetValue = 30;
            Boolean result = false;

            UInt16 outValue = 60;

            // Test
            result = target.TryGetUInt16(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt16WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt16), true);

            String inputValue = "22";
            UInt16 targetValue = 22;

            Boolean result = false;
            UInt16 outValue = 66;

            // Test
            result = target.TryGetUInt16(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt16Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt16), true);

            String inputValue = "robert";
            UInt16 targetValue = 0;

            Boolean result = true;
            UInt16 outValue = 55;

            // Test
            result = target.TryGetUInt16(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }





        [TestMethod]
        public void TestArgumentDefinitionTryGetInt16WithInt16()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(Int16), true);
            Int16 targetValue = 65;
            Boolean result = false;
            Int16 outValue = -1;

            // Test
            result = target.TryGetInt16(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt16WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int16), true);

            String inputValue = "40";
            Int16 targetValue = 40;
            Boolean result = false;
            Int16 outValue = -1;

            // Test
            result = target.TryGetInt16(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt16WithDictionnaryObject()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int16), true);

            object inputValue = "30";
            Int32 targetValue = 30;

            Dictionary<string, object> dicInput = new Dictionary<string, object>();
            dicInput.Add(key, inputValue);

            Boolean result = false;

            Int16 outValue = -1;

            // Test
            result = target.TryGetInt16(dicInput, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt16WithDictionnaryString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int16), true);

            String inputValue = "20";
            Int16 targetValue = 20;

            Dictionary<string, string> dicInput = new Dictionary<string, string>();
            dicInput.Add(key, inputValue);

            Boolean result = false;

            Int16 outValue = -1;

            // Test
            result = target.TryGetInt16(dicInput, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt16Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(Int16), true);

            Object inputValue = "rob";
            Int16 targetValue = 0;
            Dictionary<string, object> tempObject = new Dictionary<string, object>();

            Boolean result = true;
            tempObject.Add(key, inputValue);
            Int16 outValue = Int16.MinValue;

            // Test
            result = target.TryGetInt16(tempObject, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }




        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt32WithUInt32()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt32), true);
            UInt32 targetValue = 30;
            Boolean result = false;

            UInt32 outValue = 60;

            // Test
            result = target.TryGetUInt32(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt32WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt32), true);

            String inputValue = "222344";
            UInt32 targetValue = 222344;

            Boolean result = false;
            UInt32 outValue = 66;

            // Test
            result = target.TryGetUInt32(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt32Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt32), true);

            String inputValue = "robert";
            UInt32 targetValue = 0;

            Boolean result = true;
            UInt32 outValue = 55;

            // Test
            result = target.TryGetUInt32(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }




        [TestMethod]
        public void TestArgumentDefinitionTryGetInt32WithInt32()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);
            Int32 targetValue = 65;            
            Boolean result = false;
            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt32WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            String inputValue = "65";
            Int32 targetValue = 65;
            Boolean result = false;
            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt32WithDictionnaryObject()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            String inputValue = "65";
            Int32 targetValue = 65;

            Dictionary<string, object> dicInput = new Dictionary<string, object>();
            dicInput.Add(key, inputValue);

            Boolean result = false;

            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(dicInput, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt32WithDictionnaryString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            String inputValue = "65";
            Int32 targetValue = 65;

            Dictionary<string, string> dicInput = new Dictionary<string, string>();
            dicInput.Add(key, inputValue);

            Boolean result = false;

            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(dicInput, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetInt32Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            Int64 inputValue = 65;
            Int32 targetValue = 0;

            Dictionary<string, object> tempObject = new Dictionary<string, object>();

            Boolean result = true;
            tempObject.Add(key, inputValue);

            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(tempObject, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }




        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt64WithUInt64()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt64), true);
            UInt64 targetValue = 6234232345;
            Boolean result = false;

            UInt64 outValue = 45;

            // Test
            result = target.TryGetUInt64(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt64WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt64), true);

            String inputValue = "652342343";
            UInt64 targetValue = 652342343;

            Boolean result = false;
            UInt64 outValue = 44;

            // Test
            result = target.TryGetUInt64(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetUInt64Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(UInt64), true);

            String inputValue = "robert";
            UInt64 targetValue = 0;

            Boolean result = true;
            UInt64 outValue = 54;

            // Test
            result = target.TryGetUInt64(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }



        
        [TestMethod]
        public void TestArgumentDefinitionTryGetInt64WithInt64()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(Int64), true);
            Int64 targetValue = 6234232345;
            Boolean result = false;
         
            Int64 outValue = -1;

            // Test
            result = target.TryGetInt64(targetValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }
        
        [TestMethod]
        public void TestArgumentDefinitionTryGetInt64WithString()
        {
            string key = "tata2";
            var target = new ArgumentDefinition(key, "toto", typeof(Int64), true);

            String inputValue = "652342343";
            Int64 targetValue = 652342343;

            Boolean result = false;
            Int64 outValue = -1;

            // Test
            result = target.TryGetInt64(inputValue, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }
       
        [TestMethod]
        public void TestArgumentDefinitionTryGetInt64Invalid()
        {
            string key = "tata3";
            var target = new ArgumentDefinition(key, "toto", typeof(Int64), true);

            String inputValue = "robert";
            Int64 targetValue = 0;

            Boolean result = true;
            Int64 outValue = -1;

            // Test
            result = target.TryGetInt64(inputValue, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(targetValue, outValue);
        }





        [TestMethod]
        public void TestArgumentDefinitionTryGetString()
        {
            string key = "tata4";
            var target = new ArgumentDefinition(key, "toto", typeof(String), true);

            String inputValue = "allo";
            String targetValue = "allo";
            Dictionary<string, object> tempObject = new Dictionary<string, object>();
            Boolean result = false;
            tempObject.Add(key, inputValue);
            String outValue;

            // Test
            result = target.TryGetString(tempObject, out outValue);

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(targetValue, outValue);
        }

        [TestMethod]
        public void TestArgumentDefinitionTryGetStringInvalid()
        {
            string key = "tata5";
            var target = new ArgumentDefinition(key, "toto", typeof(String), true);

            Int64 inputValue = 65;
            String targetValue = "65";
            Dictionary<String, Object> tempObject = new Dictionary<String, Object>();

            Boolean result = true;
            tempObject.Add(key, inputValue);
            String outValue;

            // Test
            result = target.TryGetString(tempObject, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreNotEqual(targetValue, outValue);
            Assert.IsNull(outValue);
        }






        [TestMethod]
        public void TestArgumentDefinitionValidateValueGoodWithInt32()
        {
            string key = "tata6";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);

            Int32 targetValue = 65;
            Dictionary<string, object> tempObject = new Dictionary<string, object>();
            Boolean result = false;
            tempObject.Add(key, targetValue);

            // Test
            result = target.ValidateValue(tempObject);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestArgumentDefinitionValidateValueGoodWithString()
        {
            string key = "tata7";
            var target = new ArgumentDefinition(key, "toto", typeof(String), true);

            String targetValue = "test";
            Dictionary<string, object> tempObject = new Dictionary<String, object>();
            Boolean result = false;
            tempObject.Add(key, targetValue);

            // Test
            result = target.ValidateValue(tempObject);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
