using System;
using System.Linq;

namespace Project2
{
    public class Transition : IEquatable<Transition>
    {
        public int Start;
        public char ReadInput;
        public char StackPop;
        public char[] StackPush;
        public int End;

        public Transition(int start, char readInput, char stackPop, string stackPush, int end)
        {
            this.Start = start;
            this.ReadInput = readInput;
            this.StackPop = stackPop;
            this.StackPush = stackPush.ToCharArray();
            this.End = end;
        }

        public bool Equals(Transition other)
        {
            return
                this.Start == other.Start && this.ReadInput == other.ReadInput &&
                this.StackPop == other.StackPop && this.StackPush.SequenceEqual(other.StackPush) &&
                this.End == other.End;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Transition b))
                return false;
            else
                return this.Equals(b);
        }

        public override int GetHashCode()
        {
            return this.Start.GetHashCode() ^ this.End.GetHashCode() ^ this.ReadInput.GetHashCode() ^ this.StackPop.GetHashCode() ^ this.StackPush.GetHashCode();
        }
    }
}
