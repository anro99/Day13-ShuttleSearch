using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door16
{
    class Door16 : Door
    {
        private List<Rule> m_rules = new List<Rule>();
        private Ticket m_ownTicket = null;
        private List<Ticket> m_nearbyTickets = new List<Ticket>();
        private List<Ticket> m_validTickets = new List<Ticket>();
        private int m_numberOfValues;

        public Door16(TextBox a_input, TextBox a_output)
            : base(a_input, a_output)
        {
        }

        internal void Question1()
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            int sumOfInvalidValues = 0;
            foreach(var ticket in m_nearbyTickets)
            {
                sumOfInvalidValues += ticket.CalculateSumOfInvalidValues(m_rules);
            }

            Result.Text = $"Sum of invalid values = {sumOfInvalidValues}";
        }

        internal void Question2()
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            foreach (var ticket in m_nearbyTickets)
            {
                if (ticket.IsValidTicket(m_rules))
                    m_validTickets.Add(ticket);
            }
            m_validTickets.Add(m_ownTicket);

            List<(Rule Rule, Dictionary<Rule,bool> ValidForValue)> rulePositionList = new List<(Rule Rule, Dictionary<Rule, bool> ValidForValue)>();
            int valueIdx;
            for (valueIdx = 0; valueIdx < m_numberOfValues; valueIdx++)
                rulePositionList.Add((null, new Dictionary<Rule, bool>()));

            if (!AssignRuleToPosition(rulePositionList, 0))
            {
                Result.Text = "no solution found";
                return;
            }

            ulong result = 1;
            for (valueIdx = 0; valueIdx < m_numberOfValues; valueIdx++)
            {
                if (rulePositionList[valueIdx].Rule.Name.StartsWith("departure"))
                {
                    result = (ulong)(m_ownTicket.GetValue(valueIdx)) * result;
                }
            }

            Result.Text = $"Result = {result}";
        }

        private bool ValidForAllValidTickets(List<(Rule Rule, Dictionary<Rule, bool> ValidForValue)> a_rulePositionList, Rule a_rule, int a_valueIdx)
        {
            if (a_rulePositionList[a_valueIdx].ValidForValue.TryGetValue(a_rule, out var valid))
                return valid;
            foreach (var ticket in m_validTickets)
            {
                if (!ticket.IsValueValid(a_valueIdx, a_rule))
                {
                    a_rulePositionList[a_valueIdx].ValidForValue.Add(a_rule, false);
                    return false;
                }
            }
            a_rulePositionList[a_valueIdx].ValidForValue.Add(a_rule, true);
            return true;
        }

        private bool AssignRuleToPosition(List<(Rule Rule, Dictionary<Rule, bool> ValidForValue)> a_rulePositionList, int a_ruleIdx)
        {
            if (a_ruleIdx == m_rules.Count)
                return true;

            for (int valueIdx = 0; valueIdx < m_numberOfValues; valueIdx++)
            {
                if (a_rulePositionList[valueIdx].Rule == null)
                {
                    if (ValidForAllValidTickets(a_rulePositionList, m_rules[a_ruleIdx], valueIdx))
                    {
                        a_rulePositionList[valueIdx] = (m_rules[a_ruleIdx], a_rulePositionList[valueIdx].ValidForValue);
                        if (AssignRuleToPosition(a_rulePositionList, a_ruleIdx + 1))
                            return true;
                        else
                            a_rulePositionList[valueIdx] = (null, a_rulePositionList[valueIdx].ValidForValue);
                    }
                }
            }
            return false;
        }



        private bool TryParseInput()
        {
            var lines = Input.Text.Split(Environment.NewLine);
            int i = 0;
            for (; i < lines.Length; i++)
            {
                var rule = Rule.Create(lines[i]);
                if (rule == null)
                    break;
                m_rules.Add(rule);
            }
            m_numberOfValues = m_rules.Count;

            while (lines[i] != "your ticket:")
                i++;
            i++;
            m_ownTicket = Ticket.Create(lines[i]);

            while (lines[i] != "nearby tickets:")
                i++;
            i++;
            for (; i < lines.Length; i++)
            {
                var ticket = Ticket.Create(lines[i]);
                if (null != ticket)
                    m_nearbyTickets.Add(ticket);
            }
            return true;
        }
    }
}
