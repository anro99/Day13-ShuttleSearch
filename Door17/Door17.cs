using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door17
{
    class Door17 : Door
    {
        private Grid m_grid3 = new Grid(3);
        private Grid m_grid4 = new Grid(4);

        public Door17(TextBox a_input, TextBox a_output)
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

            for (int turn = 1; turn <= 6; turn++)
                m_grid3 = m_grid3.NextCycle();

            Result.Text = $"Number of active elements = {m_grid3.NumberOfActiveElements()}";
        }

        internal void Question2()
        {
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            for (int turn = 1; turn <= 6; turn++)
                m_grid4 = m_grid4.NextCycle();

            Result.Text = $"Number of active elements = {m_grid4.NumberOfActiveElements()}";
        }


        private bool TryParseInput()
        {
            var lines = Input.Text.Split(Environment.NewLine);
            int y = 0;
            foreach(var line in lines)
            {
                var chars = line.ToCharArray();
                var x = 0;
                foreach(var sign in chars)
                {
                    if (sign == '#')
                    {
                        m_grid3.SetElementActive(new Coordinate(new int[] { x, y, 0 }));
                        m_grid4.SetElementActive(new Coordinate(new int[] { x, y, 0, 0 }));
                    }
                    x++;
                }
                y++;
            }

            return true;
        }
    }
}
