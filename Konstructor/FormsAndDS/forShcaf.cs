using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konstructor
{
    public partial class forShcaf : Form
    {
        public forShcaf()
        {
            InitializeComponent();
        }

        private void forShcaf_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSet1.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kursDBDataSet1.Shcaf);

        }

        private void forShcaf_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
                shcafBindingSource.EndEdit();
            else
                shcafBindingSource.CancelEdit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try { Convert.ToInt32(textBox1.Text); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try { Convert.ToInt32(textBox2.Text); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
