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
        public Form1()
        {
            InitializeComponent();
        }

        private void m_btnDoor13a_Click(object sender, EventArgs e)
        {
            var door = new Door13.Door13(m_edtInput, m_edtResult);
            door.Question1();
        }

        private void m_btnDoor13b_Click(object sender, EventArgs e)
        {
            var door = new Door13.Door13(m_edtInput, m_edtResult);
            door.Question2();
        }

        private void m_btnDoor15a_Click(object sender, EventArgs e)
        {
            var door = new Door15.Door15(m_edtInput, m_edtResult);
            door.Question(2020);
        }

        private void m_btnDoor15b_Click(object sender, EventArgs e)
        {
            var door = new Door15.Door15(m_edtInput, m_edtResult);
            door.Question(30000000);
        }
    }
}
