using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Konstructor
{
    public partial class DB : Form
    {
        int nPage=0;
        public DB()
        {
            InitializeComponent();
        }

        private void DB_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSet2.Prodaza". При необходимости она может быть перемещена или удалена.
            this.prodazaTableAdapter.Fill(this.kursDBDataSet2.Prodaza);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSet1.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kursDBDataSet1.Shcaf);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kursDBDataSet.Postavshik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSetKomplect.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kursDBDataSetKomplect.Komplect);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kursDBDataSetClient.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kursDBDataSetClient.Client);            
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                forClient f1 = new forClient();
                clientBindingSource.AddNew();
                f1.clientBindingSource.DataSource = clientBindingSource;
                f1.clientBindingSource.Position = clientBindingSource.Position;
                if (f1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    clientTableAdapter.Update(this.kursDBDataSetClient);
            }
            if (nPage == 1)
            {
                forKomplect f0 = new forKomplect();
                komplectBindingSource.AddNew();
                f0.komplectBindingSource.DataSource = komplectBindingSource;
                f0.komplectBindingSource.Position = komplectBindingSource.Position;
                if (f0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    komplectTableAdapter.Update(this.kursDBDataSetKomplect);
            }
            if (nPage == 2)
            {
                forPost f2 = new forPost();
                postavshikBindingSource.AddNew();
                f2.postavshikBindingSource.DataSource = postavshikBindingSource;
                f2.postavshikBindingSource.Position = postavshikBindingSource.Position;
                if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    postavshikTableAdapter.Update(this.kursDBDataSet);
            }
            if (nPage == 3)
            {
                forShcaf f4 = new forShcaf();
                shcafBindingSource.AddNew();
                f4.shcafBindingSource.DataSource = shcafBindingSource;
                f4.shcafBindingSource.Position = shcafBindingSource.Position;
                if (f4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    shcafTableAdapter.Update(this.kursDBDataSet1);
            }
        }

        private void bDel_Click(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                clientBindingSource.RemoveCurrent();
                clientBindingSource.EndEdit();
                clientTableAdapter.Update(this.kursDBDataSetClient);
            }
            if (nPage == 1)
            {
                komplectBindingSource.RemoveCurrent();
                komplectBindingSource.EndEdit();
                komplectTableAdapter.Update(this.kursDBDataSetKomplect);
            }
            if (nPage == 2)
            {
                postavshikBindingSource.RemoveCurrent();
                postavshikBindingSource.EndEdit();
                postavshikTableAdapter.Update(this.kursDBDataSet);
            }
            if (nPage == 3)
            {
                shcafBindingSource.RemoveCurrent();
                shcafBindingSource.EndEdit();
                shcafTableAdapter.Update(this.kursDBDataSet1);
            }
            if (nPage == 4)
            {
                prodazaBindingSource.RemoveCurrent();
                prodazaBindingSource.EndEdit();
                prodazaTableAdapter.Update(this.kursDBDataSet2);
            }
        }

        private void bChange_Click(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                forClient fc = new forClient();
                fc.clientBindingSource.DataSource = clientBindingSource;
                fc.clientBindingSource.Position = clientBindingSource.Position;
                if (fc.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    clientTableAdapter.Update(this.kursDBDataSetClient);
            }
            if (nPage == 1)
            {
                forKomplect f2 = new forKomplect();
                f2.komplectBindingSource.DataSource = komplectBindingSource;
                f2.komplectBindingSource.Position = komplectBindingSource.Position;
                if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    komplectTableAdapter.Update(this.kursDBDataSetKomplect);
            }
            if (nPage == 2)
            {
                forPost f2 = new forPost();
                f2.postavshikBindingSource.DataSource = postavshikBindingSource;
                f2.postavshikBindingSource.Position = postavshikBindingSource.Position;
                if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    postavshikTableAdapter.Update(this.kursDBDataSet);
            }
            if (nPage == 3)
            {
                forShcaf f4 = new forShcaf();
                f4.shcafBindingSource.DataSource = shcafBindingSource;
                f4.shcafBindingSource.Position = shcafBindingSource.Position;
                if (f4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    shcafTableAdapter.Update(this.kursDBDataSet1);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            bChange.Enabled = true;
            bAdd.Enabled = true;
            nPage = e.TabPageIndex;
            if (e.TabPageIndex == 4)
            {
                bAdd.Enabled = false;
                bChange.Enabled = false;
            }
        }
    }
}
