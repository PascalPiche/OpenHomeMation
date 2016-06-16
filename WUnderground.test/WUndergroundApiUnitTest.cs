using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WUnderground.Api.Data;

namespace WUnderground.test
{
    [TestClass]
    public class WUndergroundApiUnitTest
    {
        [TestMethod]
        public void QueryLocationExistWithValidValue()
        {
            bool result = WUnderground.Api.WUndergroundApi.QueryLocationExist("fef59313084b598a", 0, 1, "71627");
            Assert.IsTrue(result, "Should return true");
        }

        [TestMethod]
        public void QueryLocationExistWithInvalidValue()
        {
            bool result = WUnderground.Api.WUndergroundApi.QueryLocationExist("fef59313084b598a", 0, 0, "0");
            Assert.IsFalse(result, "Should return true");
        }

        [TestMethod]
        public void QueryConditions()
        {
            
            WUndergroundConditionsResponse result = WUnderground.Api.WUndergroundApi.QueryConditions("fef59313084b598a", 0, 1, "71627");
            Assert.IsNotNull(result);
        }
    }
}
