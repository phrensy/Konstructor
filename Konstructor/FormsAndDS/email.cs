using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Konstructor.FormsAndDS
{
    public partial class email : Form
    {
<<<<<<< HEAD
        List<string> LofClients = new List<string>();
        Attachment attach;
        static MailMessage mail = new MailMessage();
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\KURS\BD\KBD.MDF;
                                    Integrated Security=True;
                                     Connect Timeout=30";
=======

        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=G:\университет\2 курс\ООП\Konstructor\Konstructor\bin\Debug\BD\KBD.mdf;
                                    Integrated Security=True;
                                     Connect Timeout=30";
        List<string> ListOfClients = new List<string>();
        List<string> ListOfPost = new List<string>();
        List<int> ListOfId = new List<int>();
        string MAIL;

>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
        public email()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == "" ||textBox3.Text == "" )
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
<<<<<<< HEAD
                SendMail("smtp.mail.ru", "ewpuk@mail.ru", "gfhjkmjngjxns89", comboBox2.Text, textBox2.Text, textBox3.Text);
                MessageBox.Show("Письмо успешно отправлено!");
=======
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SendMail("smtp.mail.ru", "ewpuk@mail.ru", "gfhjkmjngjxns89", MAIL, textBox2.Text, textBox3.Text, openFileDialog1.FileName);
                MessageBox.Show("Письмо успешно отправлено!");
            }
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
        }
        /// <summary>
        /// Отправка письма на почтовый ящик C# mail send
        /// </summary>
        /// <param name="smtpServer">Имя SMTP-сервера</param>
        /// <param name="from">Адрес отправителя</param>
        /// <param name="password">пароль к почтовому ящику отправителя</param>
        /// <param name="mailto">Адрес получателя</param>
        /// <param name="caption">Тема письма</param>
        /// <param name="message">Сообщение</param>
        /// <param name="attachFile">Присоединенный файл</param>
        public static void SendMail(string smtpServer, string from, string password,
<<<<<<< HEAD
        string mailto, string caption, string message /*,string attachFile = null*/)
        {
            try
            {               
=======
        string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
<<<<<<< HEAD
                //if (!string.IsNullOrEmpty(attachFile))
                //    mail.Attachments.Add(new Attachment(attachFile));
=======
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show("Mail.Send: " + e.Message);
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (comboBox1.SelectedIndex==0)
            {
                comboBox2.DataSource = clientBindingSource;
                comboBox2.DisplayMember = Name;
            }
            if (comboBox1.SelectedIndex==1)
            {
                comboBox2.DataSource = postavshikBindingSource;
                comboBox2.DisplayMember = Name;
            }
            
        }
        public string FIOClienta()
        {
            string fio = "";
            string name = "";
            string otch = "";
            string adress = "";
            string phone = "";

            string queryString = "SELECT F,I,O,Adress,Phone FROM Client WHERE Email=N'" + comboBox2.Text + "'";
=======
            comboBox2.Items.Clear();
            if (comboBox1.SelectedIndex==0)
            {
                //список клиентов
                ToListClient();
                foreach (var c in ListOfClients)
                    comboBox2.Items.Add(c);
            }
            if (comboBox1.SelectedIndex==1)
            {
                //список поставщиков
                ToListPost();
                foreach(var c in ListOfPost)
                    comboBox2.Items.Add(c);
            }
            
        }

        public string Info(int id, int point)
        {
            string info = "";
            string queryString="";
            if(point==0)
                queryString = "SELECT Adress,Phone,Email FROM Client WHERE Id='" + id + "'";
            else if(point==1)
                queryString = "SELECT Adress,Phone,Email FROM Postavshik WHERE Id='" + id + "'";
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73

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
<<<<<<< HEAD
                            fio = Convert.ToString(reader["F"]) + " ";
                            name = Convert.ToString(reader["I"]);
                            otch = Convert.ToString(reader["O"]);
                            adress = Convert.ToString(reader["Adress"]);
                            phone = Convert.ToString(reader["Phone"]);
=======
                            info = Convert.ToString(reader["Adress"]) +" "+ Convert.ToString(reader["Phone"]+" "+Convert.ToString(reader["Email"]));
                            MAIL = Convert.ToString(reader["Email"]);
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
<<<<<<< HEAD

            fio += name[0] + ". " + otch[0] + "., ул. " + adress+", тел. "+phone;

            return fio;
        }

        public string Post()
        {
            string name = "";
            string adress = "";
            string phone = "";
            string d = "";

            string queryString = "SELECT Name,Adress,Phone,DaysToDel FROM Postavshik WHERE Email=N'" + comboBox2.Text + "'";
=======
            return info;
        }

        public void ToListPost()
        {
            ListOfPost.Clear();
            ListOfId.Clear();
            string queryString = "SELECT Id, Name FROM Postavshik";
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

<<<<<<< HEAD
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            name = Convert.ToString(reader["Name"]);
                            adress = Convert.ToString(reader["Adress"]);
                            phone = Convert.ToString(reader["Phone"]);
                            d = Convert.ToString(reader["DaysToDel"]);
                        }
                    }
=======
                    while (reader.Read())
                    {

                        ListOfId.Add(Convert.ToInt32(reader["Id"]));
                        ListOfPost.Add(Convert.ToString(reader["Name"]));
                    }

>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
<<<<<<< HEAD
            }

            name += ", ул. "+adress + ", тел. " + phone+", дней до поставки"+d;

            return name;
        }

        private void email_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBD_DataSet.Postavshik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBD_DataSet.Client);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = FIOClienta();
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label2.Text = Post();
            }
        }
        
        public void ListofClient()
        {
            string queryString = "";

            queryString = "SELECT F,I,O FROM Client";
=======

            }
            

        }

        public void ToListClient()
        {
            ListOfClients.Clear();
            ListOfId.Clear();
            string queryString = "SELECT Id, F, I, O FROM Client";
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
<<<<<<< HEAD
                        LofClients.Add(Convert.ToString(reader["F"]) + Convert.ToString(reader["I"]) + Convert.ToString(reader["O"]));
=======

                        ListOfId.Add(Convert.ToInt32(reader["Id"]));
                        ListOfClients.Add(Convert.ToString(reader["F"])+" "+Convert.ToString(reader["I"])+" "+Convert.ToString(reader["O"]));
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

<<<<<<< HEAD

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                attach = new Attachment(openFileDialog1.FileName);
                mail.Attachments.Add(attach);
                label6.Text += openFileDialog1.FileName+"/r/n";
=======
        }

        private void email_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBD_DataSet.Postavshik);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBD_DataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBD_DataSet.Client);

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = Info(ListOfId[comboBox2.SelectedIndex],0);
                textBox1.Text=MAIL;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                label2.Text = Info(ListOfId[comboBox2.SelectedIndex], 1);
                textBox1.Text = MAIL;
>>>>>>> 404954a7a597636b0c2ce1150877fcaeed269e73
            }
        }
    }
}
