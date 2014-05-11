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
    public partial class forClient : Form
    {
        string[] abc = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "й", "ц", "у", "к", "е", "н", "г", "ш", "щ", "з", "х", "ъ", "ф", "ы", "в", "а", "п", "р", "о", "л", "д", "ж", "э", "я", "ч", "с", "м", "и", "т", "ь", "б" };
        public forClient()
        {
            InitializeComponent();
        }

        private void forClient_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBDDataSet.Client);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBDDataSet.Client);

        }

        private void forClient_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                clientBindingSource.EndEdit();
            }
            else
                clientBindingSource.CancelEdit();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < abc.Length; i++)
            {
                if (textBox5.Text.Contains(abc[i]))
                {
                    MessageBox.Show("Нельзя вводить буквы!");
                    return;
                }
            }
        }

    }
}
