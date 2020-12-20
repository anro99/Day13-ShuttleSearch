using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door19
{
    internal class Message
    {
        public Message(string a_text)
        {
            Text = a_text;
        }

        public string Text { get; private set; }
    }
}
