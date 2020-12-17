using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door16
{
    internal class Rule
    {
        private List<(int Lower, int Upper)> m_limits = new List<(int Lower, int Upper)>();

        public static Rule Create(string a_line)
        {
            var strParts = a_line.Split(':');
            if (strParts.Length != 2)
                return null;

            var name = strParts[0].Trim();
            strParts = strParts[1].Split("or");
            if (!TryParseBorder(strParts[0], out var limit1))
                return null;
            if (!TryParseBorder(strParts[1], out var limit2))
                return null;

            return new Rule() { Name = name, m_limits = new List<(int Lower, int Upper)>() { limit1, limit2 } };
        }

        private static bool TryParseBorder(string a_border, out (int Lower, int Upper) a_limit)
        {
            a_limit = (0, 0);
            var strBorders = a_border.Split('-');
            if (strBorders.Length != 2)
                return false;
            if (!int.TryParse(strBorders[0], out var lower))
                return false;
            if (!int.TryParse(strBorders[1], out var upper))
                return false;
            a_limit = (lower, upper);
            return true;
        }

        public string Name { get; private set; }
        public bool Check(int a_value)
        {
            foreach(var limit in m_limits)
            {
                if (a_value >= limit.Lower && a_value <= limit.Upper)
                    return true;
            }
            return false;
        }
    }
}
