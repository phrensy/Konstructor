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

        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=G:\университет\2 курс\ООП\Konstructor\Konstructor\bin\Debug\BD\KBD.mdf;
                                    Integrated Security=True;
                                     Connect Timeout=30";
        List<string> ListOfClients = new List<string>();
        List<string> ListOfPost = new List<string>();
        List<int> ListOfId = new List<int>();
        string MAIL;

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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SendMail("smtp.mail.ru", "ewpuk@mail.ru", "gfhjkmjngjxns89", MAIL, textBox2.Text, textBox3.Text, openFileDialog1.FileName);
                MessageBox.Show("Письмо успешно отправлено!");
            }
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
        string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
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
                            info = Convert.ToString(reader["Adress"]) +" "+ Convert.ToString(reader["Phone"]+" "+Convert.ToString(reader["Email"]));
                            MAIL = Convert.ToString(reader["Email"]);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return info;
        }

        public void ToListPost()
        {
            ListOfPost.Clear();
            ListOfId.Clear();
            string queryString = "SELECT Id, Name FROM Postavshik";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        ListOfId.Add(Convert.ToInt32(reader["Id"]));
                        ListOfPost.Add(Convert.ToString(reader["Name"]));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            

        }

        public void ToListClient()
        {
            ListOfClients.Clear();
            ListOfId.Clear();
            string queryString = "SELECT Id, F, I, O FROM Client";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        ListOfId.Add(Convert.ToInt32(reader["Id"]));
                        ListOfClients.Add(Convert.ToString(reader["F"])+" "+Convert.ToString(reader["I"])+" "+Convert.ToString(reader["O"]));
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

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
            }
        }
    }
}
