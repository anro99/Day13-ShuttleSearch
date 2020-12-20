using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.DoorN
{
    class DoorN : Door
    {
        public DoorN(TextBox a_input, TextBox a_output)
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

        }

        internal void Question2()
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

        }


        private bool TryParseInput()
        {
            var lines = Input.Text.Split(Environment.NewLine);
            int y = 0;
            foreach (var line in lines)
            {
            }

            return true;
        }

    }
}
