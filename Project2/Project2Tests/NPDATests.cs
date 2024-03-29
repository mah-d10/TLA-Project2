﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Project2.Tests
{
    [TestClass()]
    public class NPDATests
    {
        [TestMethod()]
        public void NPDATest()
        {
            string path = @"..\..\..\TestData\npda1.txt";
            var ndpa = new NPDA(path);
            Assert.AreEqual(ndpa.StateCount, 2);
            CollectionAssert.AreEqual(ndpa.Alphabet, new char[] { 'a', 'b' });
            CollectionAssert.AreEqual(ndpa.StackSymbols, new char[] { '0', '1' });
            Assert.AreEqual(ndpa.BottomOfStack, '$');

            var adjListExpected = new HashSet<int>[] {
                new HashSet<int>() { 0, 1 },
                new HashSet<int>() {}
            };
            for (int i = 0; i < ndpa.AdjacencyList.Length; i++)
                Assert.IsTrue(ndpa.AdjacencyList[i].SetEquals(adjListExpected[i]));

            var transitionsExpected = new List<Transition> {
                new Transition(0,'a','$',"0$",0),
                new Transition(0,'a','0',"00",0),
                new Transition(0,'a','1',"_",0),
                new Transition(0,'b','$',"1$",0),
                new Transition(0,'b','1',"11",0),
                new Transition(0,'b','0',"_",0),
                new Transition(0,'_','$',"_",1)
            };

            for (int i = 0; i < ndpa.Transitions.Count; i++)
                Assert.AreEqual(ndpa.Transitions[i], transitionsExpected[i]);
        }

        [TestMethod()]
        public void ToCFGTest()
        {
            string path = @"..\..\..\TestData\npda2.txt";
            var n = new NPDA(path);
            var cfg = n.ToCFG();
            Assert.AreEqual(cfg.StartVariable, "(q0$q0)");
            CollectionAssert.AreEqual(cfg.ProductionRules["(q0$q0)"], new List<RHS> { new RHS('_') });
            CollectionAssert.AreEqual(cfg.ProductionRules["(q00q0)"], new List<RHS> { new RHS('a', new List<string> { "(q00q0)", "(q01q0)" }) });
        }
    }
}