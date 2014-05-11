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
    public partial class forZakupka : Form
    {
        List<string> spisokMdf;
        List<string> spisokDVP;
        List<string> spisokDSP;
        List<string> spisokVesh;
        List<int> idKompl;
        List<int> idPost;
        List<string> namePost;
        int idshcafa;

        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;
                                    Integrated Security=True;
                                     Connect Timeout=30";

        public forZakupka(List<string> mdf, List<string> dsp, List<string> dvp, List<string> vesh, List<int> idKompl, int idSH)
        {
            InitializeComponent();
            spisokMdf = mdf;
            spisokDSP = dsp;
            spisokDVP = dvp;
            spisokVesh = vesh;
            this.idKompl = idKompl;
            idshcafa = idSH;
        }

        private void forZakupka_Load(object sender, EventArgs e)
        {
            idPost = new List<int>();
            namePost = new List<string>();
            labelMDF.Text = "";
            labelDVP.Text = "";
            labelDSP.Text = "";
            labelVesh.Text = "";
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBDDataSet.Postavshik);
            foreach (var c in spisokMdf)
                labelMDF.Text += c;
            foreach (var c in spisokDVP)
                labelDVP.Text += c;
            foreach (var c in spisokDSP)
                labelDSP.Text = c;
            foreach (var c in spisokVesh)
                labelVesh.Text += c;
            comboBoxMDF.Visible = false;
            comboBoxDVP.Visible = false;
            comboBoxDSP.Visible = false;
            comboBoxVesh.Visible = false;
            foreach (var c in idKompl)
                ListIdPost(c);
            foreach (var c in idPost)
                ReturnNamePost(c);
            if (spisokMdf.Count != 0)
            {
                comboBoxMDF.Items.Clear();
                foreach (var c in namePost)
                {
                    comboBoxMDF.Visible = true;

                    comboBoxMDF.Items.Add(c);
                }
            }
            if (spisokDSP.Count != 0)
            {
                foreach (var c in namePost)
                {
                    comboBoxDSP.Visible = true;
                    comboBoxDSP.Items.Add(c);
                }
            }
            if (spisokDVP.Count != 0)
            {
                foreach (var c in namePost)
                {
                    comboBoxDVP.Visible = true;
                    comboBoxDVP.Items.Add(c);
                }
            }
            if (spisokVesh.Count != 0)
            {
                foreach (var c in namePost)
                {
                    comboBoxVesh.Visible = true;
                    comboBoxVesh.Items.Add(c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (int c in idKompl)
            {
                addZakupka(c);
            }

            this.Close();
        }

        public void addZakupka(int idKomplfromSpisok)
        {
            int idKompl = 0;
            int idShcafa = 0;
            int idPost = 0;

            idKompl = idKomplfromSpisok;
            idShcafa = idshcafa;
            idPost = idPostavshika();

            string queryString = "INSERT INTO Zakupka(idKomplect,idShcafa,idPost) VALUES (@idKompl,@idShcafa,@idPost)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    command.Parameters.Add("@idKompl", SqlDbType.Int, 50);
                    command.Parameters["@idKompl"].Value = idKompl;

                    command.Parameters.Add("@idShcafa", SqlDbType.Int, 50);
                    command.Parameters["@idShcafa"].Value = idShcafa;

                    command.Parameters.Add("@idPost", SqlDbType.Int, 50);
                    command.Parameters["@idPost"].Value = idPost;

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

        public int idPostavshika()
        {
            int idPostav = 0;
            string queryString = "";

            if (comboBoxMDF.Items.Count != 0)
                queryString = "SELECT Id FROM Postavshik WHERE Name=N'" + comboBoxMDF.Text + "'";
            if (comboBoxDSP.Items.Count != 0)
                queryString = "SELECT Id FROM Postavshik WHERE Name=N'" + comboBoxDSP.Text + "'";
            if (comboBoxDVP.Items.Count != 0)
                queryString = "SELECT Id FROM Postavshik WHERE Name=N'" + comboBoxDVP.Text + "'";
            if (comboBoxVesh.Items.Count != 0)
                queryString = "SELECT Id FROM Postavshik WHERE Name=N'" + comboBoxVesh.Text + "'";
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
                            idPostav = Convert.ToInt32(reader["Id"]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return idPostav;

        }

        public void ListIdPost(int idKompl)
        {
            string queryString = "SELECT IdPost FROM SvyazKP WHERE idKomplect='" + idKompl + "'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        idPost.Add(Convert.ToInt32(reader["IdPost"]));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        public void ReturnNamePost(int id)
        {
            string queryString = "";

            queryString = "SELECT Postavshik.Name FROM Postavshik WHERE Postavshik.Id='" + id + "'";

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
                            namePost.Add(Convert.ToString(reader[0]));
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
