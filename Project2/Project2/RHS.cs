using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project2
{
    public class RHS : IEquatable<RHS>
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

        public bool Equals(RHS other)
        {
            return this.Terminal == other.Terminal &&
                this.Variables.SequenceEqual(other.Variables);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is RHS b))
                return false;
            else
                return this.Equals(b);
        }

        public override int GetHashCode()
        {
            return this.Terminal.GetHashCode() ^ this.Variables.GetHashCode();
        }
    }
}