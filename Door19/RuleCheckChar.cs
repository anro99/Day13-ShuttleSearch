using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal class RuleCheckChar : Rule
    {
        private char m_charToCheck;

        private RuleCheckChar(RuleSystem a_system)
            : base(a_system)
        {

        }

        private RuleCheckChar(RuleCheckChar a_other)
            : base(a_other)
        {
            m_charToCheck = a_other.m_charToCheck;
        }

        public static Rule Create(string a_line, RuleSystem a_system)
        {
            var rule = new RuleCheckChar(a_system);
            if (!rule.TryParse(a_line))
                return null;
            return rule;
        }

        public override Rule Clone()
        {
            return new RuleCheckChar(this);
        }


        public override void StartCheck(int a_nbOfCharsToMatch)
        {
        }

        public override bool CheckMessage(Message a_message, int a_startIdx, out int a_nextIdx, out bool a_done)
        {
            a_nextIdx = -1;
            a_done = true;
            if (a_startIdx == a_message.Text.Length)
                return false;
            if (a_message.Text[a_startIdx] != m_charToCheck)
                return false;
            a_nextIdx = a_startIdx + 1;
            return true;
        }

        public override void Reset()
        {
        }

        protected override bool TryParse(string a_line)
        {
            if (!base.TryParse(a_line))
                return false;

            var first = a_line.IndexOf('"');
            var last = a_line.LastIndexOf('"');
            if (1 != last - first - 1)
                return false;

            m_charToCheck = a_line.Substring(first + 1, last - first -1)[0];
            return true;
        }
    }
}
