using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal class RuleSystem
    {
        private Dictionary<int, Rule> m_rules = new Dictionary<int, Rule>();

        public RuleSystem()
        {

        }

        public bool TryAddRule( string a_line)
        {
            Rule rule = RuleCheckChar.Create(a_line, this);
            if (null == rule)
                rule = RuleSequence.Create(a_line, this);
            if (null == rule)
                rule = RuleOr.Create(a_line, this);
            if (null == rule)
                return false;

            if (m_rules.ContainsKey(rule.Number))
                m_rules.Remove(rule.Number);
            m_rules.Add(rule.Number, rule);
            return true;
        }

        public Rule GetRule(int a_number)
        {
            if (m_rules.TryGetValue(a_number, out var rule))
                return rule.Clone();
            return null;
        }

        public bool CheckMessage(Message a_message, int a_ruleNumber)
        {
            var rule = GetRule(a_ruleNumber);
            if (rule == null)
                return false;
            rule.StartCheck(a_message.Text.Length);

            bool done = false;
            do
            {
                int lastIdx = 0;
                if (rule.CheckMessage(a_message, 0, out lastIdx, out done))
                {
                    if (lastIdx == a_message.Text.Length)
                        return true;
                }
            }
            while (!done);
            return false;
        }
    }
}
