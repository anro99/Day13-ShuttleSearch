using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door15
{
    internal class Number
    {
        private List<int> m_calledPositions = new List<int>();

        public Number(int a_number)
        {
            Value = a_number;
        }

        public int Value { get; private set; }

        public void SetCalledTurn(int a_turn)
        {
            m_calledPositions.Add(a_turn);
        }

        public int GetNumberToCall()
        {
            if (m_calledPositions.Count == 1)
                return 0;
            else
                return m_calledPositions[m_calledPositions.Count - 1] - m_calledPositions[m_calledPositions.Count - 2];
        }
    }
}
