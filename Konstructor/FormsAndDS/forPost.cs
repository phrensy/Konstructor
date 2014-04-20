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
    public partial class forPost : Form
    {
        public forPost()
        {
            InitializeComponent();
        }

        private void forPost_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kursDBDataSet.Postavshik);

        }

        private void forPost_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
                postavshikBindingSource.EndEdit();
            else
                postavshikBindingSource.CancelEdit();
        }
    }
}
