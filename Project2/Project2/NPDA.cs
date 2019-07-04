using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Project2
{
    public class NPDA
    {
        public int StateCount;
        public int StartState;
        public int FinalState;
        public char[] Alphabet;
        public char[] StackSymbols;
        public char BottomOfStack;
        public HashSet<int>[] AdjacencyList;
        public List<Transition> Transitions;

        public NPDA(string path)
        {
            string[] fileLines = File.ReadAllLines(path);

            this.StateCount = int.Parse(fileLines[0]);
            this.Alphabet = fileLines[1].Split(' ', ',').Select(s => s[0]).ToArray();
            this.StackSymbols = fileLines[2].Split(' ', ',').Select(s => s[0]).ToArray();
            this.BottomOfStack = fileLines[3][0];

            this.AdjacencyList = new HashSet<int>[this.StateCount];
            for (int i = 0; i < this.AdjacencyList.Length; i++)
                this.AdjacencyList[i] = new HashSet<int>();

            this.Transitions = new List<Transition>();

            for (int i = 4; i < fileLines.Length; i++)
            {
                string[] edge = fileLines[i].Split(',');
                int s = int.Parse(edge[0][edge[0].Length - 1].ToString());
                int t = int.Parse(edge[4][edge[4].Length - 1].ToString());

                if (edge[0][0] == '-')
                    this.StartState = s;
                if (edge[0][0] == '*')
                    this.FinalState = s;
                if (edge[4][0] == '-')
                    this.StartState = t;
                if (edge[4][0] == '*')
                    this.FinalState = t;

                this.AdjacencyList[s].Add(t);
                this.Transitions.Add(new Transition(s, edge[1][0], edge[2][0], edge[3], t));
            }
        }

        public CFG ToCFG()
        {
            var productions = new Dictionary<string, List<RHS>>();
            var start = $"({this.StartState}{this.BottomOfStack}{this.FinalState})";

            foreach (var t in Transitions)
                if (t.StackPush[0] == '_')
                    try
                    {
                        productions[$"(q{t.Start}{t.StackPop}q{t.End})"].Add(new RHS(t.ReadInput));
                    }
                    catch (Exception)
                    {
                        productions[$"(q{t.Start}{t.StackPop}q{t.End})"] = new List<RHS>();
                        productions[$"(q{t.Start}{t.StackPop}q{t.End})"].Add(new RHS(t.ReadInput));
                    }
                else
                    for (int k = 0; k < this.StateCount; k++)
                    {
                        if (!productions.ContainsKey($"(q{t.Start}{t.StackPop}q{k})"))
                            productions[$"(q{t.Start}{t.StackPop}q{k})"] = new List<RHS>();

                        for (int l = 0; l < this.StateCount; l++)
                        {
                            productions[$"(q{t.Start}{t.StackPop}q{k})"].Add(new RHS(t.ReadInput,
                                new List<string>() { $"(q{t.End}{t.StackPush[0]}q{l})", $"(q{l}{t.StackPush[1]}q{k})" }));
                        }
                    }
            return new CFG(start, productions);
        }
    }
}
