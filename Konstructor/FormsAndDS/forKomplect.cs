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
    public partial class forKomplect : Form
    {
        public forKomplect()
        {
            InitializeComponent();
        }

        private void forPost_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSetKomplect.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kursDBDataSetKomplect.Komplect);

        }

        private void forPost_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
                komplectBindingSource.EndEdit();
            else
                komplectBindingSource.CancelEdit();
        }
    }
}
