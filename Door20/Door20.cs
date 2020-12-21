using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door20
{
    class Door20 : Door
    {
        private Picture m_picture = new Picture();

        public Door20(TextBox a_input, TextBox a_output)
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

            var done = m_picture.ArrangeTiles();
            long result = -1;
            if (done)
                result = m_picture.GetResultNumber();
            Result.Text = $"{m_picture.NumberOfTiles} Tiles arranged: {done} Result: {result}";
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
            for (int idx = 0; idx < lines.Length; )
            {
                if (lines[idx].Length == 0)
                {
                    idx++;
                    continue;
                }
                var tileLines = new List<string>();
                for (var i = 0; i < Tile.Size + 1 && idx < lines.Length; i++, idx++)
                {
                    tileLines.Add(lines[idx]);
                }
                if (!Tile.TryParse(tileLines, out var tile))
                    return false;
                m_picture.Add(tile);
            }

            return true;
        }

    }
}
