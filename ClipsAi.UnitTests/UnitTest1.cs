using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClipsAi.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var result = ClipsAI.API.Eval("");

            Assert.IsNotNull(result);
        }
    }
}
