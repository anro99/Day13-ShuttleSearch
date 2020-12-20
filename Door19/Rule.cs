using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal abstract class Rule
    {
        public Rule(RuleSystem a_system)
        {
            System = a_system;
        }

        public Rule(Rule a_other)
        {
            System = a_other.System;
            Number = a_other.Number;
        }

        public int Number { get; protected set; }
        public RuleSystem System { get; }

        protected virtual bool TryParse( string a_line)
        {
            var pos = a_line.IndexOf(':');
            if (-1 == pos)
                return false;
            if (!int.TryParse(a_line.Substring(0, pos), out var number))
                return false;
            Number = number;
            return true;
        }

        public abstract bool CheckMessage(Message a_message, int a_startIdx, out int a_nextIdx, out bool a_done);
        public abstract Rule Clone();
        public abstract void StartCheck(int a_nbOfCharsToMatch);
        public abstract void Reset();
    }
}
