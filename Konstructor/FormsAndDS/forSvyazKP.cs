using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Konstructor.FormsAndDS
{
    public partial class forSvyazKP : Form
    {
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\KURS\BD\KBD.MDF;
                                    Integrated Security=True;
                                     Connect Timeout=30";
        public forSvyazKP()
        {
            InitializeComponent();
        }

        private void forSvyazKP_Load(object sender, EventArgs e)
        {

        }

        public void addSvyazKP()
        {
            int idPost = idPostav();
            int idKomplect = idKompl();
            int Price = Convert.ToInt32(textBox1.Text);

            string queryString = "INSERT INTO SvyazKP(idPost,idKomplect,Price) VALUES (@idPost,@idKomplect,@Price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    command.Parameters.Add("@idPost", SqlDbType.Int, 50);
                    command.Parameters["@idPost"].Value = idPost;

                    command.Parameters.Add("@idKomplect", SqlDbType.Int, 50);
                    command.Parameters["@idKomplect"].Value = idKomplect;

                    command.Parameters.Add("@Price", SqlDbType.Int, 50);
                    command.Parameters["@Price"].Value = Price;
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    // MessageBox.Show("Добавлено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        public int idPostav()
        {
            int id = 0;
            string queryString = "SELECT Id From Postavshik where Name=N'" + comboBox1.Text + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return id;
        }

        public int idKompl()
        {
            int id = 0;
            string queryString = "SELECT Id From Komplect where Name=N'" + comboBox2.Text + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return id;
        }

        private void forSvyazKP_Load_1(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kBD_DataSet.Komplect);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBD_DataSet.Postavshik);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addSvyazKP();
        }
    }
}
