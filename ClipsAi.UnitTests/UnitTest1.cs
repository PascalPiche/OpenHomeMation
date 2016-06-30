using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClipsAI.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var result = ClipsAI.API.Eval("(+ 3 4)");

            Assert.IsNotNull(result);
            Assert.AreSame("7", result);
        }
    }
}
