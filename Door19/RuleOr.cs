using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal class RuleOr : Rule
    {
        private List<Rule> m_rules = new List<Rule>();
        private int m_nextOrPart = 0;

        private RuleOr(RuleSystem a_system) : base(a_system)
        {
        }

        private RuleOr(RuleOr a_other) : base(a_other)
        {
            foreach(var rule in a_other.m_rules)
            {
                m_rules.Add(rule.Clone());
            }
            m_nextOrPart = a_other.m_nextOrPart;
        }

        public static Rule Create(string a_line, RuleSystem a_system)
        {
            var rule = new RuleOr(a_system);
            if (!rule.TryParse(a_line))
                return null;
            return rule;
        }

        public override Rule Clone()
        {
            return new RuleOr(this);
        }

        public override void StartCheck(int a_nbOfCharsToMatch)
        {
            foreach (var rule in m_rules)
                rule.StartCheck(a_nbOfCharsToMatch);
        }


        public override bool CheckMessage(Message a_message, int a_startIdx, out int a_nextIdx, out bool a_done)
        {
            for( ; m_nextOrPart < m_rules.Count; m_nextOrPart++)
            {
                if (m_rules[m_nextOrPart].CheckMessage(a_message, a_startIdx, out a_nextIdx, out var done))
                {
                    if (done)
                    {
                        a_done = m_nextOrPart == m_rules.Count - 1;
                        m_nextOrPart++;
                    }
                    else
                    {
                        a_done = false;
                    }
                    return true;
                }
            }
            a_done = true;
            a_nextIdx = -1;
            Reset();
            return false;
        }

        public override void Reset()
        {
            m_nextOrPart = 0;
            foreach (var rule in m_rules)
                rule.Reset();
        }


        protected override bool TryParse(string a_line)
        {
            if (!base.TryParse(a_line))
                return false;

            if (-1 == a_line.IndexOf('|'))
                return false;

            var strParts = a_line.Substring(a_line.IndexOf(':') + 1).Split('|');
            foreach (var strPart in strParts)
            {
                var tempLine = "100000:" + strPart.Trim();
                var part = RuleSequence.Create(tempLine, System);
                if (null == part)
                    return false;
                m_rules.Add(part);
            }

            return true;
        }

    }
}
