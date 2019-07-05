using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Project2.Tests
{
    [TestClass()]
    public class CFGTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            string path = @"..\..\..\TestData\npda2.txt";
            var n = new NPDA(path);
            var expected = "(q00q0)->a(q00q0)(q01q0)\n(q0$q0)->_\n";
            File.WriteAllText(@"..\..\..\TestData\cfg_2.txt", expected);
            Assert.AreEqual(n.ToCFG().ToString(), expected);
        }

        [TestMethod()]
        public void CheckTest()
        {
            string path = @"..\..\..\TestData\npda1.txt";
            var n = new NPDA(path);
            var cfg = n.ToCFG();
            var res = cfg.RemoveNullables().Check("abba", new List<string>() { cfg.StartVariable }, "");
            File.WriteAllText(@"..\..\..\TestData\derivation_1.txt", res.ToString());
            System.Console.WriteLine(res.ToString());
            Assert.AreEqual("(q0$q1)=>a(q00q0)(q0$q1)=>ab(q0$q1)=>abb(q01q0)=>abba", res.ToString());
        }

        [TestMethod()]
        public void RemoveNullablesTest()
        {
            var dict = new Dictionary<string, List<RHS>>();
            dict.Add("S", new List<RHS> { new RHS('a', new List<string> { "M", "B" }) });
            dict.Add("M", new List<RHS> { new RHS('a', new List<string> { "M", "B" }), new RHS('_') });
            dict.Add("B", new List<RHS> { new RHS('b') });
            var cfg = new CFG("S", dict);
            Assert.AreEqual("S->aMB|aB\nM->aMB|aB\nB->b\n", cfg.RemoveNullables().ToString());
        }
    }
}