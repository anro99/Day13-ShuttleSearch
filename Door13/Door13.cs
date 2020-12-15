using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Day13_ShuttleSearch.Door13
{
    internal class Door13 : Door
    {
        private int m_timestamp;
        private List<Bus> m_busses = new List<Bus>();

        public Door13(TextBox a_input, TextBox a_output)
            : base(a_input, a_output)
        {
        }

        public void Question1()
        {
            Result.Text = "";
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            Bus nextBus = null;
            long nextDeparture = 0;
            foreach (var bus in m_busses)
            {
                var depart = bus.NextDeparture(m_timestamp);
                if ((nextBus == null) || (nextDeparture > depart))
                {
                    nextBus = bus;
                    nextDeparture = depart;
                }
            }

            if (nextBus == null)
                Result.Text = "No bus found";
            else
                Result.Text = $"Next departure with bus {nextBus.ID} in {nextDeparture} min; Result = {nextBus.ID * nextDeparture}";
        }

        public void Question2()
        {
            Result.Text = "";
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }


            int nbBusses = 2;
            long timeIncrement = KleinsterGemeinsammerNennerDerIds(1);
            long minimalTime = 0;
            do
            {
                var time = minimalTime;
                while (!CheckMatchQuestion2(time, nbBusses))
                    time += timeIncrement;
                minimalTime = time;
                Result.Text = $"Result for the first {nbBusses} busses: {minimalTime}";
                Result.Refresh();

                timeIncrement = KleinsterGemeinsammerNennerDerIds(nbBusses);
                nbBusses++;
            }
            while (nbBusses <= m_busses.Count);


            Result.Text = $"First occurance at {minimalTime}";
        }

        private bool CheckMatchQuestion2(long a_time, int a_nbBusses)
        {
            for (int i = 0; i < a_nbBusses; i++)
            {
                var bus = m_busses[i];
                if (0 != bus.NextDeparture(a_time + bus.Position))
                    return false;
            }
            return true;
        }

        private long KleinsterGemeinsammerNennerDerIds(int a_nbBusses)
        {
            long result = 1;
            for (int i = 0; i < a_nbBusses; i++)
            {
                result = result * m_busses[i].ID;
            }
            return result;
        }


        private bool TryParseInput()
        {
            m_timestamp = 0;
            m_busses.Clear();

            var lines = Input.Text.Split(Environment.NewLine);
            if (lines.Length != 2)
                return false;
            if (!int.TryParse(lines[0], out m_timestamp))
                return false;

            var strBusses = lines[1].Split(",");
            int position = 0;
            foreach (var strBus in strBusses)
            {
                if (strBus == "x")
                {

                }
                else
                {
                    if (!int.TryParse(strBus, out var busId))
                        return false;
                    m_busses.Add(new Bus(busId, position));
                }
                position++;
            }

            return true;
        }

    }
}
