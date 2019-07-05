﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

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
            File.WriteAllText(@"..\..\..\TestData\res_2.txt", expected);
            Assert.AreEqual(n.ToCFG().ToString(), expected);
        }

        [TestMethod()]
        public void CheckTest()
        {
            string path = @"..\..\..\TestData\1.txt";
            var n = new NPDA(path);
            var res = n.ToCFG().Check("abba");
            if (res == null)
                File.WriteAllText(@"..\..\..\TestData\check_1.txt", "False");
            else
            {
                File.WriteAllText(@"..\..\..\TestData\check_1.txt", "True\n");
                var derivation = new StringBuilder();
                for (int i = 0; i < res.Count; i++)
                    if (i == res.Count - 1)
                        derivation.Append($"{res[i]}=>");
                    else
                        derivation.Append($"{res[i]}");
                File.AppendAllText(@"..\..\..\TestData\check_1.txt", derivation.ToString());
            }
        }

        [TestMethod()]
        public void RemoveNullablesTest()
        {
            string path = @"..\..\..\TestData\1.txt";
            var n = new NPDA(path);
            File.WriteAllText(@"..\..\..\TestData\removeNullable_1.txt", n.ToCFG().RemoveNullables().ToString());
        }
    }
}