using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door23
{
    class Door23 : Door
    {
        private Ring m_ring = null;

        public Door23(TextBox a_input, TextBox a_output)
            : base(a_input, a_output)
        {
        }

        internal void Question1()
        {
            if (!TryParseInput(0))
            {
                MessageBox.Show("Can't parse");
                return;
            }

            m_ring.DoTheCrab(100, (a_trace)=>
                                {
                                    Result.Text += Environment.NewLine;
                                    Result.Text += a_trace;
                                    Result.Update();
                                }
                );
            Result.Text += Environment.NewLine;
            Result.Text += m_ring.ResultString();
        }

        internal void Question2()
        {
            if (!TryParseInput(1000000))
            {
                MessageBox.Show("Can't parse");
                return;
            }

            m_ring.DoTheCrab(10000000, null );
            Result.Text += Environment.NewLine;
            Result.Text += m_ring.MultiplayFollowersOfOne();
        }


        private bool TryParseInput(int a_fillUpTo)
        {
            var lines = Input.Text.Split(Environment.NewLine);
            int y = 0;

            m_ring = Ring.Create(lines[0], a_fillUpTo);
            return m_ring != null;
        }

    }
}
