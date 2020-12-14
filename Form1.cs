using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Day13_ShuttleSearch
{
    public partial class Form1 : Form
    {
        private int m_timestamp;
        private List<Bus> m_busses = new List<Bus>();


        public Form1()
        {
            InitializeComponent();
        }

        private void m_btnQuestion1_Click(object sender, EventArgs e)
        {
            m_edtResult.Text = "";
            if (!TryParseInput())
            {
                MessageBox.Show("Can't parse");
                return;
            }

            Bus nextBus = null;
            long nextDeparture = 0;
            foreach(var bus in m_busses)
            {
                var depart = bus.NextDeparture(m_timestamp);
                if ((nextBus == null) || (nextDeparture > depart))
                {
                    nextBus = bus;
                    nextDeparture = depart;
                }
            }

            if (nextBus == null)
                m_edtResult.Text = "No bus found";
            else
                m_edtResult.Text = $"Next departure with bus {nextBus.ID} in {nextDeparture} min; Result = {nextBus.ID * nextDeparture}";
        }

        private void m_btnQuestion2_Click(object sender, EventArgs e)
        {
            m_edtResult.Text = "";
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
                m_edtResult.Text = $"Result for the first {nbBusses} busses: {minimalTime}";
                m_edtResult.Refresh();

                timeIncrement = KleinsterGemeinsammerNennerDerIds(nbBusses);
                nbBusses++;
            }
            while (nbBusses <= m_busses.Count);


            m_edtResult.Text = $"First occurance at {minimalTime}";
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

            var lines = m_edtInput.Text.Split(Environment.NewLine);
            if (lines.Length != 2)
                return false;
            if (!int.TryParse(lines[0], out m_timestamp))
                return false;

            var strBusses = lines[1].Split(",");
            int position = 0;
            foreach(var strBus in strBusses)
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
