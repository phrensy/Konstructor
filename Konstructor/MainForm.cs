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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void bKonstructor_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }

        private void bDB_Click(object sender, EventArgs e)
        {
            DB d = new DB();
            d.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormsAndDS.Docs d = new FormsAndDS.Docs();
            d.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormsAndDS.email em = new FormsAndDS.email();
            em.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormsAndDS.Otchet ot = new FormsAndDS.Otchet();
            ot.ShowDialog();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label2.Text = "Распечатка товарных накладных, договоров продажи, а также счетов на оплату(pdf и writer).";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void bKonstructor_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label2.Text = "Конструктор шкафов-купе, позволяющий пользователю нарисовать требуемую конструкцию и рассчитать ее стоимость в зависимости от выбранных материалов и фурнитуры.";
        }

        private void bKonstructor_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void bDB_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label2.Text = "Ведёт учет клиентов, товаров и комплектующих на складе, заказов на производство, поставщиков тех или иных комплектующих, а также историю операций купли-продажи.";
        }

        private void bDB_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label2.Text = "Формирует статистические отчеты о результативности работы фирмы за определенный период с разрезом  по месяцам.";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label2.Text = "Отправка документов на электронную почту.";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }
    }
}
