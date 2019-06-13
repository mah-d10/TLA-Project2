using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Project2.Tests
{
    [TestClass()]
    public class NPDATests
    {
        [TestMethod()]
        public void NPDATest()
        {
            string path = @"..\..\..\TestData\1.txt";
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
    }
}