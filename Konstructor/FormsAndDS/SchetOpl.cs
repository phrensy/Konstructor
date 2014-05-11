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
    public partial class SchetOpl : Form
    {
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;Integrated Security=True;Connect Timeout=30";
        public SchetOpl()
        {
            InitializeComponent();
        }

        private void SchetOpl_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBDDataSet.Client);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kBDDataSet.Shcaf);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "pdf|*.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                zapros();

                var doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(@saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();

                BaseFont bf = BaseFont.CreateFont(Environment.CurrentDirectory.ToString() + "\\721032.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                iTextSharp.text.Phrase j = new Phrase("Продавец: Наша фирма" +
                    "\n" + "Адрес: Ульяновск, ул. Северный Венец, д 32" +
                    "\n" + "ИНН: 1234567890" +
                    "\n" + "Расчетный счет: 1234567890987654321" +
                    "\n" + "Кор. счет: 11223344556677889900" +
                    "\n" + "БИК: 123456789" +
                    "\n" + "Банк: ОАО БАНК \"Мой банк\" Г. УЛЬЯНОВСК" +
                    "\n" + "\n" +
                    "\n" + "Покупатель: " + FIOClienta()+
                    "\n" + "Адрес: " + comboBox2.Text,
                       new iTextSharp.text.Font(bf, 12,
                           iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a1 = new Paragraph(j);
                a1.Alignment = Element.ALIGN_LEFT;
                a1.Add(Environment.NewLine);
                doc.Add(a1);

                Random r = new Random();
                DateTime thisDay = DateTime.Today;
                iTextSharp.text.Phrase j2 = new Phrase("Счет № " + r.Next(1, 20) + " от " + thisDay.ToString("d") + "\n",
                        new iTextSharp.text.Font(bf, 18,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph a2 = new Paragraph(j2);
                a2.Alignment = Element.ALIGN_CENTER;
                a2.Add(Environment.NewLine);
                doc.Add(a2);

                PdfPTable table = new PdfPTable(6);

                PdfPCell nomer = new PdfPCell(new Phrase("№",
                              new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                nomer.Padding = 3;
                nomer.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(nomer);

                PdfPCell naim = new PdfPCell(new Phrase("Наименование",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                naim.Padding = 3;
                naim.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(naim);

                PdfPCell izm = new PdfPCell(new Phrase("Ед. изм",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                izm.Padding = 3;
                izm.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(izm);

                PdfPCell kol = new PdfPCell(new Phrase("Кол-во",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                kol.Padding = 3;
                kol.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(kol);

                PdfPCell cena = new PdfPCell(new Phrase("Цена",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                cena.Padding = 3;
                cena.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cena);

                PdfPCell sum = new PdfPCell(new Phrase("Сумма",
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD, new BaseColor(Color.Black))));
                sum.Padding = 3;
                sum.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(sum);

                PdfPCell nomer1 = new PdfPCell(new Phrase("1",
                              new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                nomer1.Padding = 3;
                nomer1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(nomer1);

                PdfPCell naim1 = new PdfPCell(new Phrase("Продажа шкафа-купе",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                naim1.Padding = 3;
                naim1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(naim1);

                PdfPCell izm1 = new PdfPCell(new Phrase("шт",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                izm1.Padding = 3;
                izm1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(izm1);

                PdfPCell kol1 = new PdfPCell(new Phrase("1",
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                kol1.Padding = 3;
                kol1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(kol1);


                PdfPCell cena1 = new PdfPCell(new Phrase(CenaTovara(),
                             new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                cena1.Padding = 3;
                cena1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cena1);

                PdfPCell sum1 = new PdfPCell(new Phrase(CenaTovara(),
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                sum1.Padding = 3;
                sum1.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(sum1);

                PdfPCell itog = new PdfPCell(new Phrase("Итого:",
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                itog.Padding = 3;
                itog.Colspan = 3;
                itog.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(itog);


                PdfPCell kol2 = new PdfPCell(new Phrase("1",
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                kol2.Padding = 3;
                kol2.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(kol2);

                PdfPCell cena2 = new PdfPCell(new Phrase(CenaTovara(),
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                cena2.Padding = 3;
                cena2.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cena2);

                PdfPCell cena3 = new PdfPCell(new Phrase(CenaTovara(),
                            new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black))));
                cena3.Padding = 3;
                cena3.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cena3);


                //ширина ячеек
                int[] widths = { 12, 36, 12, 12, 12, 12 };
                table.SetWidths(widths);
                doc.Add(table);


                iTextSharp.text.Phrase ph13 = new Phrase("\n" + "\n" + "\n" + "\n" + "Продавец \"Наша Фирма\"        (_____________________)",
                    new iTextSharp.text.Font(bf, 12,
                        iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par13 = new Paragraph(ph13);
                par13.Alignment = Element.ALIGN_CENTER;
                par13.Add(Environment.NewLine);
                doc.Add(par13);

                doc.Close();

                //Process proc = new Process();
                //proc.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe";
                //proc.StartInfo.Arguments = String.Format(@"/p /h {0}", saveFileDialog1.FileName);
                //proc.StartInfo.UseShellExecute = false;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.Start();
            }
        }

        public void zapros()
        {
            int id = 0;

            string queryString = "SELECT Id FROM Client WHERE Adress=N'" + comboBox2.Text + "'";

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
                            id = Convert.ToInt32(reader["Id"]);
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

        public string FIOClienta()
        {
            string fio = "";
            string name = "";
            string otch = "";

            string queryString = "SELECT F,I,O FROM Client WHERE Adress=N'" + comboBox2.Text + "'";

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
                            fio = Convert.ToString(reader["F"]) + " ";
                            name = Convert.ToString(reader["I"]);
                            otch = Convert.ToString(reader["O"]);

                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            fio += name[0] + ". " + otch[0] + ".";

            return fio;
        }

        public string CenaTovara()
        {
            float cena = 0;
            string itog;

            string queryString = "SELECT Price FROM Shcaf WHERE Name=N'" + comboBox1.Text + "'";

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
                            cena = (float)Convert.ToDouble(reader["Price"]);
                        }
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            itog = cena.ToString("F");
            return itog;
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
                    odt.AddElement("Продавец: Наша фирма");
                    odt.AddElement("Адрес: Ульяновск, ул. Северный Венец, д 32");
                    odt.AddElement("ИНН: 1234567890");
                    odt.AddElement("Расчетный счет: 1234567890987654321");
                    odt.AddElement("Кор. счет: 11223344556677889900");
                    odt.AddElement("БИК: 123456789");
                    odt.AddElement("Банк: ОАО БАНК 'Мой банк' Г. УЛЬЯНОВСК");
                    odt.AddElement("");
                    odt.AddElement("Покупатель: " + FIOClienta());
                    odt.AddElement("Адрес: " + comboBox2.Text);
                    odt.AddZag("Счет № " + r.Next(1, 20) + " от " + thisDay.ToString("d"));
                    odt.AddElement("");
                    odt.AddElement("");
                    odt.AddElement("Продавец \"Наша Фирма\"        (_____________________)");
                    string result = odt.SaveFile();
                    MessageBox.Show("Файлы успешно сохранены!");
                }
            }
        }
    }
}
