﻿using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public static class Util
    {
        public static string Stringify(this List<string> l)
        {
            var s = new StringBuilder();
            l.ForEach(x => s.Append(x));
            return s.ToString();
        }
    }

    public class CFG
    {
        public string StartVariable;
        public Dictionary<string, List<RHS>> ProductionRules;

        public CFG(string startVariable, Dictionary<string, List<RHS>> productions)
        {
            this.ProductionRules = new Dictionary<string, List<RHS>>();
            this.StartVariable = startVariable;
            this.ProductionRules = productions;
        }

        public override string ToString()
        {
            var ans = new StringBuilder();
            foreach (var key in this.ProductionRules.Keys)
            {
                var r = new StringBuilder();
                for (int i = 0; i < this.ProductionRules[key].Count; i++)
                    if (i != this.ProductionRules[key].Count - 1)
                        r.Append($"{this.ProductionRules[key][i].ToString()}|");
                    else
                        r.Append($"{this.ProductionRules[key][i].ToString()}");
                ans.Append($"{key}->{r.ToString()}\n");
            }
            return ans.ToString();
        }

        public CFG RemoveNullables()
        {
            foreach (var key in this.ProductionRules.Keys)
                for (int i = 0; i < this.ProductionRules[key].Count; i++)
                    if (this.ProductionRules[key][i].Terminal == '_')
                    {
                        this.ProductionRules[key].RemoveAt(i);
                        if (this.ProductionRules[key].Count == 0)
                            this.ProductionRules.Remove(key);

                        replace(key);
                    }

            return this;

            void replace(string nullableVar)
            {
                foreach (var key in this.ProductionRules.Keys)
                    for (int i = 0; i < this.ProductionRules[key].Count; i++)
                        for (int j = 0; j < this.ProductionRules[key][i].Variables.Count; j++)
                            if (this.ProductionRules[key][i].Variables[j] == nullableVar)
                            {
                                var terminal = this.ProductionRules[key][i].Terminal;
                                var vars = new List<string>();
                                vars.AddRange(this.ProductionRules[key][i].Variables);
                                vars.Remove(nullableVar);
                                this.ProductionRules[key].Add(new RHS(terminal, vars));
                            }

                foreach (var v in this.ProductionRules)
                    for (int r = v.Value.Count - 1; r >= 0; r--)
                    {
                        for (int i = 0; i < v.Value[r].Variables.Count; i++)
                            if (!this.ProductionRules.ContainsKey(v.Value[r].Variables[i]))
                            {
                                v.Value.RemoveAt(r);
                                break;
                            }
                    }
            }
        }

        public StringBuilder Check(string input, List<string> vars, string passedInput)
        {
            if (input.Length == 0 && vars.Count == 0)
                return new StringBuilder("");
            else if (input.Length != 0 && vars.Count == 0 || input.Length == 0 && vars.Count != 0)
                return new StringBuilder("False");

            foreach (RHS rhs in this.ProductionRules[vars[0]])
                if (rhs.Terminal == input[0])
                {
                    var NextVarList = new List<string>();

                    NextVarList.AddRange(rhs.Variables);
                    for (int k = 1; k < vars.Count; k++)
                        NextVarList.Add(vars[k]);

                    var res = Check(input.Substring(1), NextVarList, passedInput + input[0]);

                    if (res.ToString() != "False")
                        if (res.ToString().Length > 0)
                        {
                            if (passedInput.Length == 0)
                                passedInput = StartVariable.ToString() + "=>";
                            return new StringBuilder(passedInput + rhs.Terminal + NextVarList.Stringify() + "=>" + res);
                        }
                        else
                            return new StringBuilder(passedInput + rhs.Terminal + NextVarList.Stringify());
                }

            return new StringBuilder("False");
        }
    }
}
