using System;
using System.Collections.Generic;
using System.Text;

namespace Day13_ShuttleSearch.Door23
{
    internal class Cup
    {
        public Cup(int a_number)
        {
            Number = a_number;
        }

        public int Number { get; private set; }

        public Cup Next { get; set; }
    }
}
