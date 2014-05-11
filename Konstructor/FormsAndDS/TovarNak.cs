using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Konstructor.FormsAndDS
{
    public partial class TovarNak : Form
    {
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;Integrated Security=True;Connect Timeout=30";
        public TovarNak()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "pdf|*.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(@saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();

                BaseFont bf = BaseFont.CreateFont(Environment.CurrentDirectory.ToString()+"\\721032.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);


                iTextSharp.text.Phrase j = new Phrase("Организация 'Наша фирма'",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a1 = new Paragraph(j);
                a1.Alignment = Element.ALIGN_LEFT;
                a1.Add(Environment.NewLine);
                doc.Add(a1);

                DateTime thisDay = DateTime.Today;
                iTextSharp.text.Phrase j2 = new Phrase(thisDay.ToString("d"),
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.UNDERLINE, new BaseColor(Color.Black)));
                Paragraph a2 = new Paragraph(j2);
                a2.Alignment = Element.ALIGN_RIGHT;
                a2.Add(Environment.NewLine);
                doc.Add(a2);

                Random r = new Random();
                iTextSharp.text.Phrase j3 = new Phrase("Накладная #" + r.Next(1, 20),
                        new iTextSharp.text.Font(bf, 18,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a3 = new Paragraph(j3);
                a3.Alignment = Element.ALIGN_CENTER;
                a3.Add(Environment.NewLine);
                doc.Add(a3);

                iTextSharp.text.Phrase j4 = new Phrase("От кого 'Климов С.А.'",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a4 = new Paragraph(j4);
                a4.Alignment = Element.ALIGN_LEFT;
                a4.Add(Environment.NewLine);
                doc.Add(a4);

                iTextSharp.text.Phrase j5 = new Phrase("Кому " + comboBox1.Text,
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a5 = new Paragraph(j5);
                a5.Alignment = Element.ALIGN_LEFT;
                a5.Add(Environment.NewLine);
                doc.Add(a5);

                iTextSharp.text.Phrase j6 = new Phrase("Основание " + textBox1.Text,
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a6 = new Paragraph(j6);
                a6.Alignment = Element.ALIGN_LEFT;
                a6.Add(Environment.NewLine);
                a6.Add(Environment.NewLine);
                doc.Add(a6);

                PdfPTable table = new PdfPTable(5);
                PdfPCell c21 = new PdfPCell(new Phrase("Наименование товара",
                        new iTextSharp.text.Font(bf, 14,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c21.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c21);

                PdfPCell c22 = new PdfPCell(new Phrase("Ед. изм.",
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c22.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c22);

                PdfPCell c23 = new PdfPCell(new Phrase("Количество",
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c23.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c23);

                PdfPCell c24 = new PdfPCell(new Phrase("Цена",
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c24.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c24);

                PdfPCell c25 = new PdfPCell(new Phrase("Сумма",
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c25.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c25);

                PdfPCell c31 = new PdfPCell(new Phrase(comboBox2.Text,
                        new iTextSharp.text.Font(bf, 14,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c21.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c31);

                PdfPCell c32 = new PdfPCell(new Phrase("Шт.",
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c22.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c32);

                PdfPCell c33 = new PdfPCell(new Phrase(textBox2.Text,
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c23.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c33);

                //прописать цену
                int cena = cenaOFkomplect();
                PdfPCell c34 = new PdfPCell(new Phrase(Convert.ToString(cena),
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c24.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c34);

                //прописать сумму
                int sum = sumOFkomplect();
                PdfPCell c35 = new PdfPCell(new Phrase(Convert.ToString(sum),
                    new iTextSharp.text.Font(bf, 14,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                c25.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(c35);

                int[] widths = { 10, 3, 6, 4, 5 };
                table.SetWidths(widths);
                doc.Add(table);

                iTextSharp.text.Phrase j7 = new Phrase("Сдал _________________",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a7 = new Paragraph(j7);
                a7.Alignment = Element.ALIGN_CENTER;
                a7.Add(Environment.NewLine);
                doc.Add(a7);

                iTextSharp.text.Phrase j8 = new Phrase("Принял _________________",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a8 = new Paragraph(j8);
                a8.Alignment = Element.ALIGN_CENTER;
                a8.Add(Environment.NewLine);
                doc.Add(a8);

                doc.Close();

                //Process proc = new Process();
                //proc.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe";
                //proc.StartInfo.Arguments = String.Format(@"/p /h {0}", saveFileDialog1.FileName);
                //proc.StartInfo.UseShellExecute = false;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.Start();
            }
        }

        private void TovarNak_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Komplect". При необходимости она может быть перемещена или удалена.
            this.komplectTableAdapter.Fill(this.kBDDataSet.Komplect);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Postavshik". При необходимости она может быть перемещена или удалена.
            this.postavshikTableAdapter.Fill(this.kBDDataSet.Postavshik);
            //checkedListBox1.Items = kBDDataSet.Komplect.Rows;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = "";
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Выберите папку, где будут сохранены файлы.";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath;
                    System.IO.Directory.CreateDirectory(path);
                    path += "\\";
                    OO.ODT odt = new OO.ODT(path);
                    DateTime thisDay = DateTime.Today;
                    Random r = new Random();
                    odt.AddElement("Организация 'Наша фирма'");
                    odt.AddElement(thisDay.ToString("d"));
                    odt.AddElement("");
                    odt.AddZag("Накладная #" + r.Next(1, 20));
                    odt.AddElement("");
                    odt.AddElement("От кого 'Климов С.А.'");
                    odt.AddElement("Кому " + comboBox1.SelectedText);
                    odt.AddElement("");
                    odt.AddElement("Основание " + textBox1.Text);
                    string result = odt.SaveFile();
                    MessageBox.Show("Файлы успешно сохранены!");
                }
            }
        }

        public int sumOFkomplect()
        {
            int sum=0;
            string mater = "";

            string queryString = "SELECT Material FROM Komplect WHERE Name=N'" + comboBox2.Text + "'";
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
                            mater = Convert.ToString(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (mater == "МДФ")
                sum = Convert.ToInt32(textBox2.Text) * 280;
            if (mater == "ДСП")
                sum = Convert.ToInt32(textBox2.Text) * 150;
            if (mater == "ДВП")
                sum = Convert.ToInt32(textBox2.Text) * 50;
            if (mater == "Хром")
                sum = Convert.ToInt32(textBox2.Text) * 94;
               
            return sum;
        }

        public int cenaOFkomplect()
        {
            int cena = 0;
            string mater = "";

            string queryString = "SELECT Material FROM Komplect WHERE Name=N'" + comboBox2.Text + "'";
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
                            mater = Convert.ToString(reader[0]);
                        }
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (mater == "МДФ")
                cena = 280;
            if (mater == "ДСП")
                cena = 150;
            if (mater == "ДВП")
                cena = 50;
            if (mater == "Хром")
                cena = 94;

            return cena;
        }
    }
}
