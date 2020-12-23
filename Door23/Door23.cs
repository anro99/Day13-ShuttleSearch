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
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            string turnResult;
            for( var turns = 0; turns < 100; turns++)
            {
                var pickupCups = m_ring.RemoveCups(3);
                m_ring.InsertCups(m_ring.FindNextLowerOrGreatest(m_ring.CurrentCup.Number), pickupCups);
                m_ring.StepToNext();
                turnResult = m_ring.RepresentationAsString();
            }
            Result.Text = m_ring.ResultString();
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

            m_ring = Ring.Create(lines[0]);
            return m_ring != null;
        }

    }
}
