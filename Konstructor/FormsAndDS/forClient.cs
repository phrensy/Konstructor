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
    public partial class forClient : Form
    {
        public forClient()
        {
            InitializeComponent();
        }

        private void Add_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSetClient.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kursDBDataSetClient.Client);

        }

        private void Add_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
                clientBindingSource.EndEdit();
            else
                clientBindingSource.CancelEdit();
        }
    }
}
