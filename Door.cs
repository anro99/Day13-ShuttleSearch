using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door13
{
    internal class Door
    {
        public Door(TextBox a_input, TextBox a_result)
        {
            Result = a_result;
            Input = a_input;
        }

        public TextBox Result { get; private set; }
        public TextBox Input { get; private set; }
    }
}