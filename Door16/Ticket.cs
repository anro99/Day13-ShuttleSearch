using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door16
{
    internal class Ticket
    {
        private List<int> m_values = new List<int>();

        public static Ticket Create(string a_line)
        {
            var strValues = a_line.Split(',');
            var result = new Ticket();
            foreach(var strValue in strValues)
            {
                if (!int.TryParse(strValue, out var value))
                    return null;

                result.m_values.Add(value);
            }
            return result;
        }

        public int CalculateSumOfInvalidValues(List<Rule> a_rules)
        {
            int result = 0;
            foreach(var value in m_values)
            {
                bool valid = false;
                foreach(var rule in a_rules)
                {
                    if (rule.Check(value))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                    result += value;
            }
            return result;
        }

        public bool IsValidTicket(List<Rule> a_rules)
        {
            int result = 0;
            foreach (var value in m_values)
            {
                bool valid = false;
                foreach (var rule in a_rules)
                {
                    if (rule.Check(value))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                    return false;
            }
            return true;
        }

        public bool IsValueValid(int a_position, Rule a_rule)
        {
            return a_rule.Check(m_values[a_position]);
        }

        public int GetValue(int a_position)
        {
            return m_values[a_position];
        }

    }
}
