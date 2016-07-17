﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Commands;

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
        public void TestArgumentDefinitionTryGetInt32WithInt32()
        {
            string key = "tata";
            var target = new ArgumentDefinition(key, "toto", typeof(Int32), true);


            Int32 targetValue = 65;
            
            /*Dictionary<string, object> tempObject = new Dictionary<string, object>();
             * tempObject.Add(key, targetValue);*/

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

            //Dictionary<string, object> tempObject = new Dictionary<string, object>();
            Boolean result = false;

            //tempObject.Add(key, inputValue);

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
            Int32 targetValue = 65;
            Dictionary<string, object> tempObject = new Dictionary<string, object>();

            Boolean result = true;
            tempObject.Add(key, inputValue);
            Int32 outValue = -1;

            // Test
            result = target.TryGetInt32(tempObject, out outValue);

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreNotEqual(targetValue, outValue);
            Assert.AreEqual(-1, outValue);
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
