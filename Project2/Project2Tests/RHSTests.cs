using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Project2.Tests
{
    [TestClass()]
    public class RHSTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            var rhs = new RHS('a', new List<string>() { "(q0bq1)", "(q2aq1)", "(q0bq0)" });
            Assert.AreEqual(rhs.ToString(), "a(q0bq1)(q2aq1)(q0bq0)");
        }
    }
}