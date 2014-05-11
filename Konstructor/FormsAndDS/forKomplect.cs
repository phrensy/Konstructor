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
    public partial class forKomplect : Form
    {
        public forKomplect()
        {
            InitializeComponent();
        }

        private void forKomplect_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kBDDataSet.Komplect);

        }

        private void forKomplect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                komplectBindingSource.EndEdit();
            }
            else
                komplectBindingSource.CancelEdit();
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try { Convert.ToInt32(textBox3.Text); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
