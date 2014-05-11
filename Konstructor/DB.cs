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
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;Integrated Security=True;Connect Timeout=30";
        public DB()
        {
            InitializeComponent();
        }

        private void DB_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Prodaza". При необходимости она может быть перемещена или удалена.
            this.prodazaTableAdapter.Fill(this.kBD_DataSet.Prodaza);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.SvyazKP". При необходимости она может быть перемещена или удалена.
            this.svyazKPTableAdapter.Fill(this.kBD_DataSet.SvyazKP);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kBD_DataSet.Shcaf);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBD_DataSet.Postavshik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kBD_DataSet.Komplect);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBD_DataSet.Client);

        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                FormsAndDS.forClient f1 = new FormsAndDS.forClient();
                clientBindingSource.AddNew();
                f1.clientBindingSource.DataSource = clientBindingSource;
                f1.clientBindingSource.Position = clientBindingSource.Position;
                if (f1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    clientTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 1)
            {
                FormsAndDS.forKomplect f0 = new FormsAndDS.forKomplect();
                komplectBindingSource.AddNew();
                f0.komplectBindingSource.DataSource = komplectBindingSource;
                f0.komplectBindingSource.Position = komplectBindingSource.Position;
                if (f0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    komplectTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 2)
            {
                FormsAndDS.forPost f2 = new FormsAndDS.forPost();
                postavshikBindingSource.AddNew();
                f2.postavshikBindingSource.DataSource = postavshikBindingSource;
                f2.postavshikBindingSource.Position = postavshikBindingSource.Position;
                if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    postavshikTableAdapter.Update(this.kBD_DataSet);
            }
            //if (nPage == 3)
            //{
            //    FormsAndDS.forShcaf f4 = new FormsAndDS.forShcaf();
            //    shcafBindingSource.AddNew();
            //    f4.shcafBindingSource.DataSource = shcafBindingSource;
            //    f4.shcafBindingSource.Position = shcafBindingSource.Position;
            //    if (f4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        shcafTableAdapter.Update(this.kBD_DataSet);
            //}
            if (nPage == 5)
            {
                FormsAndDS.forSvyazKP f = new FormsAndDS.forSvyazKP();
                dataGridView6.DataSource = GetSvKP();
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    f.Close();
                    dataGridView6.Refresh();
                    dataGridView6.DataSource = GetSvKP();
                }
            }
        }

        private void bChange_Click_1(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                FormsAndDS.forClient f1 = new FormsAndDS.forClient();
                f1.clientBindingSource.DataSource = clientBindingSource;
                f1.clientBindingSource.Position = clientBindingSource.Position;
                if (f1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    clientTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 1)
            {
                FormsAndDS.forKomplect f0 = new FormsAndDS.forKomplect();
                f0.komplectBindingSource.DataSource = komplectBindingSource;
                f0.komplectBindingSource.Position = komplectBindingSource.Position;
                if (f0.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    komplectTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 2)
            {
                FormsAndDS.forPost f2 = new FormsAndDS.forPost();
                f2.postavshikBindingSource.DataSource = postavshikBindingSource;
                f2.postavshikBindingSource.Position = postavshikBindingSource.Position;
                if (f2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    postavshikTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 3)
            {
                FormsAndDS.forShcaf f4 = new FormsAndDS.forShcaf();
                f4.shcafBindingSource.DataSource = shcafBindingSource;
                f4.shcafBindingSource.Position = shcafBindingSource.Position;
                if (f4.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    shcafTableAdapter.Update(this.kBD_DataSet);
            }
        }

        private void tabControl1_Selected_1(object sender, TabControlEventArgs e)
        {
            bChange.Enabled = true;
            bAdd.Enabled = true;
            nPage = e.TabPageIndex;
            if (e.TabPageIndex == 4)
            {
                dataGridView5.DataSource = GetProd();
                bAdd.Enabled = false;
                bChange.Enabled = false;
            }
            if (e.TabPageIndex == 3)
            {
                bAdd.Enabled = false;
            }
            if (e.TabPageIndex == 5)//связь
            {
                dataGridView6.DataSource = GetSvKP();
                bChange.Enabled = false;
            }
            
        }

        private void bDel_Click_1(object sender, EventArgs e)
        {
            if (nPage == 0)
            {
                clientBindingSource.RemoveCurrent();
                clientBindingSource.EndEdit();
                clientTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 1)
            {
                komplectBindingSource.RemoveCurrent();
                komplectBindingSource.EndEdit();
                komplectTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 2)
            {
                postavshikBindingSource.RemoveCurrent();
                postavshikBindingSource.EndEdit();
                postavshikTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 3)
            {
                shcafBindingSource.RemoveCurrent();
                shcafBindingSource.EndEdit();
                shcafTableAdapter.Update(this.kBD_DataSet);
            }
            if (nPage == 4)
            {
                delet();
            }
            if (nPage == 5)
            {
                deletsv();
            }
        }

        DataTable GetSvKP()
        {
            DataTable dt = new DataTable();

            string queryString = @"select SvyazKP.Id as 'Id', Postavshik.Name as 'Поставщик', Komplect.Name as 'Комплектующее', SvyazKP.Price as 'Цена' from SvyazKP, Postavshik, Komplect where Postavshik.Id=SvyazKP.idPost AND Komplect.Id=SvyazKP.idKomplect";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return dt;
        }

        DataTable GetProd()
        {
            DataTable dt = new DataTable();

            string queryString = @"select Prodaza.Id as 'Id', Shcaf.Name as 'Шкаф', Client.F as 'Фамилия клиента', Client.I as 'Имя', 
Client.O as 'Отчество', Prodaza.DataOfSell as 'Дата продажи' , Prodaza.PriceOfSell as 'Цена продажи, руб.' from Prodaza, Shcaf, Client where Prodaza.idClient=Client.Id and Prodaza.idShafa=Shcaf.Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.HasRows)
                            dt.Load(reader);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return dt;
        }

        private void delet()
        {
            int id = 0;

            try
            {
                if (dataGridView5.RowCount > 0)
                {
                    id = Convert.ToInt32(dataGridView5[0, dataGridView5.CurrentRow.Index].Value.ToString());

                    string queryString = @"DELETE FROM Prodaza WHERE Id =" + id;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        //MessageBox.Show("Удалено");
                        dataGridView5.DataSource = GetProd();
                        dataGridView5.Refresh();
                    }
                }
                else MessageBox.Show("В таблице строк больше нет", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
 "Произошла ошибка при удалении строк. Подробное описание ошибки:" + ex.Message, "Ошибка удаления строк");
            }

        }
        private void deletsv()
        {
            int id = 0;

            try
            {
                if (dataGridView6.RowCount > 0)
                {
                    id = Convert.ToInt32(dataGridView6[0, dataGridView6.CurrentRow.Index].Value.ToString());

                    string queryString = @"DELETE FROM SvyazKP WHERE Id =" + id;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand(queryString, connection);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        //MessageBox.Show("Удалено");
                        dataGridView6.DataSource = GetSvKP();
                        dataGridView6.Refresh();
                    }
                }
                else MessageBox.Show("В таблице строк больше нет", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
 "Произошла ошибка при удалении строк. Подробное описание ошибки:" + ex.Message, "Ошибка удаления строк");
            }

        }
    }
}
