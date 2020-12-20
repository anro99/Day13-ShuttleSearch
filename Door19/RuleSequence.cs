using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal class RuleSequence : Rule
    {
        protected List<int> m_ruleNumbers = new List<int>();
        private List<(Rule Rule, bool Checked, int NextIdx, bool Done)> m_rules = new List<(Rule Rule, bool Checked, int NextIdx, bool Done)>();
        private bool m_final = false;

        private RuleSequence(RuleSystem a_system) : base(a_system)
        {
        }

        private RuleSequence(RuleSequence a_other) : base(a_other)
        {
            m_ruleNumbers.AddRange(a_other.m_ruleNumbers);
        }

        public static Rule Create(string a_line, RuleSystem a_system)
        {
            var rule = new RuleSequence(a_system);
            if (!rule.TryParse(a_line))
                return null;
            return rule;
        }

        public override Rule Clone()
        {
            return new RuleSequence(this);
        }

        public override void StartCheck(int a_nbOfCharsToMatch)
        {
            m_final = true;
            a_nbOfCharsToMatch -= m_ruleNumbers.Count - 1;
            if (a_nbOfCharsToMatch <= 0)
                return;

            m_rules = new List<(Rule Rule, bool Checked, int NextIdx, bool Done)>();
            foreach (var ruleNumber in m_ruleNumbers)
            {
                var rule = System.GetRule(ruleNumber);
                if (null == rule)
                    return;
                m_rules.Add((rule, false, 0, false));
                rule.StartCheck(a_nbOfCharsToMatch);
            }
            m_final = false;
        }

        public override bool CheckMessage(Message a_message, int a_startIdx, out int a_nextIdx, out bool a_done)
        {
            a_nextIdx = -1;
            a_done = true;
            if (m_final)
                return false;

            bool done = false;
            bool passed;

            var ret = CheckNextPart(a_message, a_startIdx, out a_nextIdx, out a_done, 0);
            if (ret)
            {
                if (!a_done)
                {
                    for (int idx = m_rules.Count - 1; idx >= 0; idx--)
                    {
                        if (!m_rules[idx].Done)
                        {
                            m_rules[idx] = (m_rules[idx].Rule, false, 0, false);
                            break;
                        }
                        else
                        {
                            m_rules[idx].Rule.Reset();
                            m_rules[idx] = (m_rules[idx].Rule, false, 0, false);
                        }
                    }
                }
            }
            else
            {
                Reset();
            }
            return ret;
        }

        public override void Reset()
        {
            for (var idx = 0; idx < m_rules.Count; idx++)
            {
                m_rules[idx] = (m_rules[idx].Rule, false, 0, false);
                m_rules[idx].Rule.Reset();
            }
        }

        private bool CheckNextPart(Message a_message, int a_startIdx, out int a_nextIdx, out bool a_thisPartDone, int a_partIdx)
        {
            a_thisPartDone = true;
            bool init = true;
            do
            {
                if (!init || !m_rules[a_partIdx].Checked)
                {
                    if (!m_rules[a_partIdx].Rule.CheckMessage(a_message, a_startIdx, out a_nextIdx, out a_thisPartDone))
                        return false;
                    else
                        m_rules[a_partIdx] = (m_rules[a_partIdx].Rule, true, a_nextIdx, a_thisPartDone);
                }
                else
                {
                    a_nextIdx = m_rules[a_partIdx].NextIdx;
                    a_thisPartDone = m_rules[a_partIdx].Done;
                }

                if (a_partIdx == m_rules.Count - 1)
                    return true;

                if (!init)
                    m_rules[a_partIdx + 1].Rule.Reset();
                var nextPartValid = CheckNextPart(a_message, a_nextIdx, out a_nextIdx, out var nextPartDone, a_partIdx + 1);
                if (nextPartValid)
                {
                    a_thisPartDone = a_thisPartDone && nextPartDone;
                    return true;
                }
                init = false;
            }
            while (!a_thisPartDone);
            return false;
        }

        protected override bool TryParse(string a_line)
        {
            if (!base.TryParse(a_line))
                return false;

            if (-1 != a_line.IndexOf('|'))
                return false;
            if (-1 != a_line.IndexOf('"'))
                return false;

            var strNumbers = a_line.Substring(a_line.IndexOf(':') + 1).Trim().Split(' ');
            foreach(var strNumber in strNumbers)
            {
                if (!int.TryParse(strNumber, out var number))
                    return false;
                m_ruleNumbers.Add(number);
            }

            return true;
        }

    }
}
