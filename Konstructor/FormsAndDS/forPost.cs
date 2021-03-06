﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Konstructor.FormsAndDS
{
    public partial class forPost : Form
    {
        public forPost()
        {
            InitializeComponent();
        }

        private void forPost_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBDDataSet.Postavshik);

        }

        private void forPost_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                postavshikBindingSource.EndEdit();
            }
            else
                postavshikBindingSource.CancelEdit();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try { Convert.ToInt32(textBox4.Text); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
