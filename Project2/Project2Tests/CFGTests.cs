using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Project2.Tests
{
    [TestClass()]
    public class CFGTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            string path = @"..\..\..\TestData\2.txt";
            var n = new NPDA(path);
            var expected = "(q00q0)->a(q00q0)(q01q0)\n(q0$q0)->_\n";
            Assert.AreEqual(n.ToCFG().ToString(), expected);
        }
    }
}