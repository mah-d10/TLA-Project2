using System.Collections.Generic;
using System.Text;

namespace Project2
{
    public class RHS
    {
        public char Terminal;
        public List<string> Variables;

        public RHS(char terminal, List<string> vars)
        {
            this.Terminal = terminal;
            this.Variables = vars;
        }

        public RHS(char terminal)
        {
            this.Variables = new List<string>();
            this.Terminal = terminal;
        }

        public override string ToString()
        {
            var vars = new StringBuilder();
            foreach (var v in this.Variables)
                vars.Append(v);
            return $"{this.Terminal}{vars}";
        }
    }
}