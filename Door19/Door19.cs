using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door19
{
    class Door19 : Door
    {
        private RuleSystem m_system = new RuleSystem();
        private List<Message> m_messages = new List<Message>();

        public Door19(TextBox a_input, TextBox a_output)
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

            var sb = new StringBuilder();
            int nbValidMessages = 0;
            foreach (var message in m_messages)
            {
                if (m_system.CheckMessage(message, 0))
                {
                    sb.AppendLine(message.Text);
                    nbValidMessages++;
                }
            }
            sb.Append($"Number of valid messages: {nbValidMessages}");
            Result.Text = sb.ToString();
        }

        internal void Question2()
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            m_system.TryAddRule("8: 42 | 42 8");
            m_system.TryAddRule("11: 42 31 | 42 11 31");

            var sb = new StringBuilder();
            int nbValidMessages = 0;
            foreach (var message in m_messages)
            {
                if (m_system.CheckMessage(message, 0))
                {
                    sb.AppendLine(message.Text);
                    nbValidMessages++;
                }
            }
            sb.Append($"Number of valid messages: {nbValidMessages}");
            Result.Text = sb.ToString();
        }


        private bool TryParseInput()
        {
            var lines = Input.Text.Split(Environment.NewLine);
            int y = 0;
            foreach (var line in lines)
            {
                if (line.Length == 0)
                    continue;
                if (!m_system.TryAddRule(line))
                    m_messages.Add(new Message(line));
            }

            return true;
        }

    }
}
