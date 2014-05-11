using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Konstructor.FormsAndDS
{
    public partial class Docs : Form
    {
        public Docs()
        {
            InitializeComponent();
        }

        private void Docs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormsAndDS.TovarNak t = new TovarNak();
            t.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormsAndDS.Dogovor d = new Dogovor();
            d.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormsAndDS.SchetOpl s = new SchetOpl();
            s.ShowDialog();
        }
    }
}
