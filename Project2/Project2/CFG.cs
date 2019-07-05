using System.Collections.Generic;
using System.Text;

namespace Project2
{
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

        public CFG ToChomsky()
        {
            this.RemoveNullables();
            return this;
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

                bool flag = false;
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

        public List<string> Check(string input)
        {
            return new List<string> { input };
        }
    }
}
