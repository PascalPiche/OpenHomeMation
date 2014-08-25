﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OHM.Logger;

namespace OHM.Tests
{
    [TestClass]
    public class LoggerManagerUnitTest
    {
        [TestMethod]
        public void TestGetLoggerType()
        {

            var logMng = new LoggerManager();
            var result = logMng.GetLogger(typeof(object));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefaultLogger));
        }

        [TestMethod]
        public void TestGetLoggerName()
        {

            var logMng = new LoggerManager();
            var result = logMng.GetLogger("a.a");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefaultLogger));
        }
    }
}
