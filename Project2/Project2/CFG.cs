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
    }
}
