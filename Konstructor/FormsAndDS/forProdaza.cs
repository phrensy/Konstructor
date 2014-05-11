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
    public partial class forProdaza : Form
    {
        string ss;
        float f;
        int material;
        int kolvo;
        public int countStenka;
        public int countZadnya;
        public int countYashik;
        public int countVeshalka;
        int time;
        List<int> idKompl = new List<int>();
        public int idshcafa;

        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;
                                    Integrated Security=True;
                                     Connect Timeout=30";

        public forProdaza(String s, int mater, int countS, int countZ, int countY, int countV)
        {
            InitializeComponent();
            ss = s;
            material = mater;
            countStenka = countS;
            countZadnya = countZ;
            countYashik = countY;
            countVeshalka = countV;

        }

        private void forProdaza_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBDDataSet.Client);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kBDDataSet.Shcaf);

            textBox1.Clear();
            butZak.Visible = false;
            double s = Convert.ToDouble(ss);
           // f = (float)s;
            f=(float)Math.Round(s, 2);
            textBox2.Text = Convert.ToString(f);
            textBox3.Text = "10";
            if (countStenka > (ProverkaKomplect() * 4) || countZadnya > (ProverkaKomplect() * 4)
                || countYashik > (ProverkaKomplect() * 4) || countVeshalka > (ProverkaKomplect() * 15))
            {
                label5.Text = "Товаров не хватает на складе. \nПридется подождать еще: " + timeToPost() + " дня";
                butZak.Visible = true;
                button1.Enabled = false;
                return; }

        }

        private void forProdaza_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                shcafBindingSource.EndEdit();
            }
            else
                shcafBindingSource.CancelEdit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            float f = (float)Convert.ToDouble(textBox2.Text);
            int n = Convert.ToInt32(textBox3.Text);
            DB d = new DB();
            d.shcafBindingSource.AddNew();
            shcafBindingSource.DataSource = d.shcafBindingSource;
            shcafBindingSource.Position = d.shcafBindingSource.Position;
            d.shcafTableAdapter.Update(this.kBDDataSet);
            textBox1.Text = s;
            textBox2.Text = f.ToString();
            textBox3.Text = n.ToString();
            
            addProdaza();

            if (countStenka > (ProverkaKomplect() * 4) || countZadnya > (ProverkaKomplect() * 4)
                || countYashik > (ProverkaKomplect() * 4) || countVeshalka > (ProverkaKomplect() * 15))
            { }
            else
            {
                addShcaf();
            }
        
        
        }

        public void addProdaza()
        {
            int idShafa = idLastShcaf();
            int idClient = idKlienta();
            // DateTime DataOfSell = new DateTime();
            DateTime thisDay = DateTime.Today;
            string DataOfSell = thisDay.ToString("d");
            float PriceOfSell = f;
            PriceOfSell = (float)Math.Round(f, 2);

            string queryString = "INSERT INTO Prodaza(idShafa,idClient,DataOfSell,PriceOfSell ) VALUES (@idShafa,@idClient,@DataOfSell,@PriceOfSell)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    command.Parameters.Add("@idShafa", SqlDbType.Int, 50);
                    command.Parameters["@idShafa"].Value = idShafa;

                    command.Parameters.Add("@idClient", SqlDbType.Int, 50);
                    command.Parameters["@idClient"].Value = idClient;

                    command.Parameters.Add("@DataOfSell", SqlDbType.Date, 50);
                    command.Parameters["@DataOfSell"].Value = DataOfSell;

                    command.Parameters.Add("@PriceOfSell", SqlDbType.Float, 53);
                    command.Parameters["@PriceOfSell"].Value = PriceOfSell;

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

        public void addShcaf()
        {
            string name = "";
            float price = 0;
            int timeTOconstr = 0;

            name = textBox1.Text;
            price = (float)Math.Round(f, 2);
            timeTOconstr = Convert.ToInt32(textBox3.Text) + timeToPost();

            string queryString = "INSERT INTO Shcaf(Name,Price,TimeToConstruct) VALUES (@name,@price,@timeTOconstr)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50);
                    command.Parameters["@name"].Value = name;

                    command.Parameters.Add("@price", SqlDbType.Float, 53);
                    command.Parameters["@price"].Value = price;

                    command.Parameters.Add("@timeTOconstr", SqlDbType.Int, 50);
                    command.Parameters["@timeTOconstr"].Value = timeTOconstr;

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

        public int ProverkaKomplect()
        {

            string queryString = "";
            if (material == 0)//мдф
                queryString = "SELECT Kolvo FROM Komplect WHERE Material=N'МДФ'";
            if (material == 1)//дсп
                queryString = "SELECT Kolvo FROM Komplect WHERE Material=N'ДСП'";
            if (material == 2)//двп
                queryString = "SELECT Kolvo FROM Komplect WHERE Material=N'ДВП'";
            if (material == 3)//веш
                queryString = "SELECT Kolvo FROM Komplect WHERE Material=N'Хром'";

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
                            kolvo = Convert.ToInt32(reader["Kolvo"]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return kolvo;
        }

        public int timeToPost()
        {
            string queryString = "SELECT MAX(DaysToDel) FROM Postavshik";
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
                            time = Convert.ToInt32(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return time;
        }

        public List<string> needTOkomplect(int mat)
        {
            List<string> str = new List<string>();
            if (material == 0)
            {
                if (mat == 0)
                {
                    if (countStenka > (ProverkaKomplect() * 4))
                    {
                        str.Add("Лист МДФ (4.47 кв м) на стенки\n");

                        idKomplect(0, idKompl);
                    }

                    //if (countZadnya > (ProverkaKomplect() * 4))
                    //{
                    //    str.Add("Лист МДФ (4.47 кв м) для задней стенки\n");
                    //    idKomplect(0, idKompl);
                    //}
                    //if (countYashik > (ProverkaKomplect() * 4))
                    //{
                    //    str.Add("Лист МДФ (4.47 кв м) для ящиков\n");
                    //    idKomplect(0, idKompl);
                    //}
                    return str;
                }
            }
            if (material == 1)
            {
                if (mat == 1)
                {
                    if (countStenka > (ProverkaKomplect() * 4))
                    {
                        str.Add("Лист ДСП (4.47 кв м) на стенки \n");
                        idKomplect(1, idKompl);
                    }
                    //if (countZadnya > (ProverkaKomplect() * 4))
                    //{
                    //     str.Add("Лист ДСП (4.47 кв м) для задней стенки\n");
                    //    idKomplect(1, idKompl);
                    //}
                    //if (countYashik > (ProverkaKomplect() * 4))
                    //{
                    //    str.Add("Лист ДСП (4.47 кв м) для ящиков\n");
                    //    idKomplect(1, idKompl);
                    //}
                    return str;
                }
            }
            if (material == 2)
            {
                if (mat == 2)
                {
                    if (countStenka > (ProverkaKomplect() * 4))
                    {
                        str.Add("Лист ДВП (4.47 кв м) на стенки\n");
                        idKomplect(2, idKompl);
                    }
                    //if (countZadnya > (ProverkaKomplect() * 4))
                    //{
                    //    str.Add("Лист ДВП (4.47 кв м) для задней стенки\n");
                    //    idKomplect(2, idKompl);
                    //}
                    //if (countYashik > (ProverkaKomplect() * 4))
                    //{
                    //    str.Add("Лист ДВП (4.47 кв м) для ящиков\n");
                    //    idKomplect(2, idKompl);
                    //}
                    return str;
                }
            }
            if (material == 3)
            {
                if (mat == 3)
                {
                    if (countVeshalka > (ProverkaKomplect() * 15))
                    {
                        str.Add("Труба хром для вешалки");
                        idKomplect(3, idKompl);
                        return str;
                    }
                }
            }

            return str;
        }

        private void butZak_Click(object sender, EventArgs e)
        {
            addShcaf();
            forZakupka z = new forZakupka(needTOkomplect(0), needTOkomplect(1), needTOkomplect(2), needTOkomplect(3), idKompl, idLastShcaf());
            z.ShowDialog();
            button1.Enabled = true;

        }

        public void idKomplect(int material, List<int> idKompl)
        {
            string queryString = "";

            if (material == 0)//мдф
                queryString = "SELECT Id FROM Komplect WHERE Material=N'МДФ'";
            if (material == 1)//дсп
                queryString = "SELECT Id FROM Komplect WHERE Material=N'ДСП'";
            if (material == 2)//двп
                queryString = "SELECT Id FROM Komplect WHERE Material=N'ДВП'";
            if (material == 3)//вешалка
                queryString = "SELECT Id FROM Komplect WHERE Material=N'Хром'";
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
                            idKompl.Add(Convert.ToInt32(reader[0]));
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

        public int idLastShcaf()
        {
            string queryString = "";

            queryString = "SELECT MAX(Id) FROM Shcaf";
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
                            idshcafa = Convert.ToInt32(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return idshcafa;
        }

        public int idKlienta()
        {
            int idKlienta = 0;
            string queryString = "";

            queryString = "SELECT Id FROM Client WHERE F=N'" + comboBox1.Text + "'";
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
                            idKlienta = Convert.ToInt32(reader["Id"]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return idKlienta;

        }
    }
}
