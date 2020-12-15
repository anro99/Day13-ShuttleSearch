using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Day13_ShuttleSearch;

namespace Day13_ShuttleSearch.Door15
{
    internal class Door15 : Door
    {
        private List<int> m_startNumbers = new List<int>();
        private Dictionary<int, Number> m_calledNumbers = new Dictionary<int, Number>();

        public Door15(TextBox a_input, TextBox a_output)
            : base(a_input, a_output)
        {
        }

        public void Question(int a_lastRound)
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            Number lastCalledNumber = null;
            for (int i = 0; i < a_lastRound; i++)
            {
                if (i < m_startNumbers.Count)
                {
                    int num = m_startNumbers[i];
                    if (m_calledNumbers.TryGetValue(num, out lastCalledNumber))
                    {
                        lastCalledNumber.SetCalledTurn(i);
                    }
                    else
                    {
                        lastCalledNumber = new Number(num);
                        lastCalledNumber.SetCalledTurn(i);
                        m_calledNumbers.Add(num, lastCalledNumber);
                    }
                }
                else
                {
                    var num = lastCalledNumber.GetNumberToCall();
                    if (m_calledNumbers.TryGetValue(num, out lastCalledNumber))
                    {
                        lastCalledNumber.SetCalledTurn(i);
                    }
                    else
                    {
                        lastCalledNumber = new Number(num);
                        lastCalledNumber.SetCalledTurn(i);
                        m_calledNumbers.Add(num, lastCalledNumber);
                    }
                }
            }
            Result.Text = $"Last called number = {lastCalledNumber.Value}";
        }

        private bool TryParseInput()
        {
            m_startNumbers.Clear();

            var strNumbers = Input.Text.Split(',');
            foreach(var strnum in strNumbers)
            {
                if (!int.TryParse(strnum, out var num))
                    return false;
                m_startNumbers.Add(num);
            }

            return true;
        }
    }
}
