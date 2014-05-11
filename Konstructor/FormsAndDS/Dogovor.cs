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
    public partial class Dogovor : Form
    {
        double predopl = 0.0;
        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Kurs\BD\KBD.mdf;Integrated Security=True;Connect Timeout=30";
        public Dogovor()
        {
            InitializeComponent();
        }

        private void Dogovor_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Client". При необходимости она может быть перемещена или удалена.
            this.clientTableAdapter.Fill(this.kBDDataSet.Client);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Shcaf". При необходимости она может быть перемещена или удалена.
            this.shcafTableAdapter.Fill(this.kBDDataSet.Shcaf);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "kBDDataSet.Prodaza". При необходимости она может быть перемещена или удалена.
            //this.prodazaTableAdapter.Fill(this.kBDDataSet.Prodaza);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == null)
                    predopl = 0.0;///цене товара
                else
                {
                    predopl = Convert.ToDouble(textBox1.Text);
                }


            }
            catch
            {

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "pdf|*.pdf";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var doc = new Document();
                PdfWriter.GetInstance(doc, new FileStream(@saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();

                BaseFont bf = BaseFont.CreateFont(Environment.CurrentDirectory.ToString() + "\\721032.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                Random r = new Random();
                iTextSharp.text.Phrase ph1 = new Phrase("Договор продажи шкафа №" + r.Next(1, 20),
                        new iTextSharp.text.Font(bf, 18,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par1 = new Paragraph(ph1);
                par1.Alignment = Element.ALIGN_CENTER;
                par1.Add(Environment.NewLine);
                doc.Add(par1);

                iTextSharp.text.Phrase ph2 = new Phrase("г. Ульяновск",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par2 = new Paragraph(ph2);
                par2.Alignment = Element.ALIGN_LEFT;
                par2.Add(Environment.NewLine);
                doc.Add(par2);

                DateTime thisDay = DateTime.Today;
                iTextSharp.text.Phrase ph3 = new Phrase(thisDay.ToString("d"),
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par3 = new Paragraph(ph3);
                par3.Alignment = Element.ALIGN_RIGHT;
                par3.Add(Environment.NewLine);
                doc.Add(par3);

                iTextSharp.text.Phrase ph4 = new Phrase("именуемое в дальнейшем Продавец, в лице директора Климов С. А. , действующего на основании Устава, с одной стороны, и "+FIOClienta()+", именуемый  в дальнейшем Покупатель, с другой стороны, заключили настоящий договор о нижеследующем: ",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par4 = new Paragraph(ph4);
                par4.Alignment = Element.ALIGN_LEFT;
                par4.Add(Environment.NewLine);
                doc.Add(par4);

                iTextSharp.text.Phrase ph5 = new Phrase("1.	ПРЕДМЕТ ДОГОВОРА.",
                        new iTextSharp.text.Font(bf, 14,
                            iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par5 = new Paragraph(ph5);
                par5.Alignment = Element.ALIGN_CENTER;
                par5.Add(Environment.NewLine);
                doc.Add(par5);

                iTextSharp.text.Phrase ph6 = new Phrase("       1.1 Продавец обязуется передать в собственность Покупателя мебель (далее «Товар») согласно товарного чека, являющегося неотъемлемой частью настоящего Договора, а Покупатель обязуется принять Товар и оплатить  его стоимость." + "\n" + "1.2. Стоимость товара составляет (" +CenaDogovora()+ ") руб. (далее «Стоимость Товара»).",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par6 = new Paragraph(ph6);
                par6.Alignment = Element.ALIGN_LEFT;
                par6.Add(Environment.NewLine);
                doc.Add(par6);

                iTextSharp.text.Phrase ph7 = new Phrase("2.	ОБЯЗАННОСТИ ПРОДАВЦА.",
                        new iTextSharp.text.Font(bf, 14,
                            iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par7 = new Paragraph(ph7);
                par7.Alignment = Element.ALIGN_CENTER;
                par7.Add(Environment.NewLine);
                doc.Add(par7);

                iTextSharp.text.Phrase ph8 = new Phrase("       2.1 Продавец обязуется поставить Покупателю товар согласно товарного чека в течение 15 (пятнадцати) рабочих дней с момента внесения Покупателем предоплаты. Датой исполнения заказа считается дата извещения Продавцом Покупателя о прибытии Товара на склад Продавца. Извещение производится по телефонному номеру, оставленному Покупателем, в течение рабочего дня с 9.00 до 18.00 ежедневно. Ответственность за правильность оставленного телефонного номера несет Покупатель." +
"\n" + "     2.2 В случае задержки поставки Товара Продавец оплачивает Покупателю пеню в размере 0,1 % от суммы предоплаты за каждый день задержки, путем уменьшения стоимости Товара." +
"\n" + "     2.3 В случае невозможности выполнения Продавцом своих обязательств перед Покупателем, Продавец возвращает Покупателю внесенную ранее предоплату в течение 3 (трех) дней с момента подачи заявления об отказе от поставки Товара." +
"\n" + "     2.4 Продавец предоставляет Покупателю гарантию на Товар согласно действующему законодательству." +
" Гарантия предоставляется при условии выполнения Покупателем всех норм и правил эксплуатации Товара и его использования только по назначению. Гарантия осуществляется путем ремонта поврежденного изделия или его замены (в случае невозможности ремонта).",
                        new iTextSharp.text.Font(bf, 12,
                            iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par8 = new Paragraph(ph8);
                par8.Alignment = Element.ALIGN_LEFT;
                par8.Add(Environment.NewLine);
                doc.Add(par8);

                iTextSharp.text.Phrase ph9 = new Phrase("3.	ОБЯЗАННОСТИ ПОКУПАТЕЛЯ.",
                       new iTextSharp.text.Font(bf, 14,
                           iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par9 = new Paragraph(ph9);
                par9.Alignment = Element.ALIGN_CENTER;
                par9.Add(Environment.NewLine);
                doc.Add(par9);

                double predopl = Convert.ToDouble(textBox1.Text);
                double ostatok = Convert.ToDouble(CenaDogovora()) - predopl;
                iTextSharp.text.Phrase ph10 = new Phrase("3.1 В момент подписания настоящего Договора Покупатель вносит предоплату в размере ( " + predopl.ToString() + ") руб. 00коп." +
"\n" + "       3.2 Покупатель вносит оставшуюся часть платежа после получения «Товара» в момент подписания акта приема-передачи, что составляет ("+ ostatok +")руб. 00коп." +
"\n" + "       3.3 В случае расторжения настоящего Договора Покупателем в одностороннем порядке до факта извещения о выполнении его заказа или при отказе от Товара при надлежащем качестве, Продавец удерживает с Покупателя в счет понесенных издержек по поставке товара 50 % от стоимости товара надлежащего качества.",
                       new iTextSharp.text.Font(bf, 12,
                           iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par10 = new Paragraph(ph10);
                par10.Alignment = Element.ALIGN_LEFT;
                par10.Add(Environment.NewLine);
                doc.Add(par10);

                iTextSharp.text.Phrase ph11 = new Phrase("4.	ПОРЯДОК ПРИЕМА-ПЕРЕДАЧИ ТОВАРА.",
                      new iTextSharp.text.Font(bf, 14,
                          iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par11 = new Paragraph(ph11);
                par11.Alignment = Element.ALIGN_CENTER;
                par11.Add(Environment.NewLine);
                doc.Add(par11);

                iTextSharp.text.Phrase ph12 = new Phrase("4.1 Прием-передача Товара осуществляется при наличии представителей Продавца и Покупателя." +
"\n" + "       4.2 Покупатель приобретает право собственности на Товар и несет все связанные с этим риски с момента передачи товара Продавцом Покупателю." +
"\n" + "       4.3 Датой передачи Товара считается дата подписания Акта приема-передачи товара (совершение надписи в акте приема-передачи товара) уполномоченными представителями Сторон настоящего Договора." +
"\n" + "       4.4 В случае, если при приемке Товара какая-либо из позиций, указанных в товарном чеке оказалась с механическими повреждениями и/или не соответствует или отсутствует (не поставлена), в Акте приема-передачи Товара делается соответствующая запись (отметка) о необходимости замены или поставки Товара в оговоренные сроки. Соответственно проставляются дата и подписи представителей Сторон." +
"\n" + "       В случае выявления при приемке некачественного Товара Покупатель имеет право немедленно предъявить продавцу претензию в письменной форме." +
"\n" + "       4.5 После подписания Акта приема-передачи Товара Покупатель не имеет права предъявить Продавцу претензию по количеству Товара, а также по повреждениям, не указанным в Акте сдачи- приемки." +
"\n" + "       4.6 Качество товара должно соответствовать стандартам, установленным производителем." +
"\n" + "       4.7 Продавец обязуется рассмотреть претензии Покупателя, и дать на них ответ в течение 10 (десяти) рабочих дней." +
"\n" + "       4.8 Удовлетворение обоснованных претензий Покупателя, производится за счет Продавца в течение 20 (двадцати) дней с момента предъявления Покупателем претензии.",
                       new iTextSharp.text.Font(bf, 12,
                           iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par12 = new Paragraph(ph12);
                par12.Alignment = Element.ALIGN_LEFT;
                par12.Add(Environment.NewLine);
                doc.Add(par12);


                iTextSharp.text.Phrase ph13 = new Phrase("\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "Продавец_____________________        Покупатель_____________________",
                      new iTextSharp.text.Font(bf, 12,
                          iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par13 = new Paragraph(ph13);
                par13.Alignment = Element.ALIGN_CENTER;
                par13.Add(Environment.NewLine);
                doc.Add(par13);

                iTextSharp.text.Phrase ph14 = new Phrase("5.	ФОРС-МАЖОРНЫЕ ОБСТОЯТЕЛЬСТВА.",
                     new iTextSharp.text.Font(bf, 14,
                         iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par14 = new Paragraph(ph14);
                par14.Alignment = Element.ALIGN_CENTER;
                par14.Add(Environment.NewLine);
                doc.Add(par14);


                iTextSharp.text.Phrase ph15 = new Phrase("5.1 Стороны освобождаются от выполнения обязательств по настоящему Договору в случае наступления обстоятельств, возникших помимо воли и желания Сторон и которые нельзя предвидеть или избежать (форс-мажорные обстоятельства), включая объявленную или фактическую войну, гражданские волнения, эпидемии, блокаду, эмбарго, землетрясения, наводнения, пожары и другие стихийные бедствия, изменения действующего законодательства, акты и действия органов государственной власти и управления федерального и местного значения и т. д." +
"\n" + "       5.2 В случае наступления форс-мажорных обстоятельств пострадавшая Сторона обязана в кратчайшие сроки предупредить другую Сторону о невозможности исполнения своих обязательств по настоящему Договору." +
"\n" + "       5.3 При этом ни одна Сторона не может требовать возмещения каких-либо убытков или потерь, связанных с невыполнением Договора.",
                      new iTextSharp.text.Font(bf, 12,
                          iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par15 = new Paragraph(ph15);
                par15.Alignment = Element.ALIGN_LEFT;
                par15.Add(Environment.NewLine);
                doc.Add(par15);

                iTextSharp.text.Phrase ph16 = new Phrase("6.	ПРОЧИЕ УСЛОВИЯ.",
                     new iTextSharp.text.Font(bf, 14,
                         iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par16 = new Paragraph(ph16);
                par16.Alignment = Element.ALIGN_CENTER;
                par16.Add(Environment.NewLine);
                doc.Add(par16);


                iTextSharp.text.Phrase ph17 = new Phrase("6.1 Любые изменения и дополнения к настоящему Договору будут действительны только в том случае, если они совершены в письменной форме и подписаны представителями обеих Сторон." +
"\n" + "       6.2 В случаях, не предусмотренных настоящим Договором, Стороны руководствуются действующим гражданским законодательством." +
"\n" + "       6.3 При возникновении в процессе исполнения настоящего Договора спора, который Стороны не смогут урегулировать путем переговоров, спор будет разрешаться в суде по месту нахождения Продавца." +
"\n" + "       6.4 Настоящий Договор вступает в силу с момента его подписания и действует до полного исполнения Сторонами своих обязательств по нему." +
"\n" + "       6.5 Настоящий Договор составлен в двух экземплярах, по одному для каждой из Сторон.",
                      new iTextSharp.text.Font(bf, 12,
                          iTextSharp.text.Font.NORMAL, new BaseColor(Color.Black)));
                Paragraph par17 = new Paragraph(ph17);
                par17.Alignment = Element.ALIGN_LEFT;
                par17.Add(Environment.NewLine);
                doc.Add(par17);

                


                iTextSharp.text.Phrase ph20 = new Phrase("\n" + "\n" + "ПОДПИСИ СТОРОН:",
                     new iTextSharp.text.Font(bf, 14,
                         iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par20 = new Paragraph(ph20);
                par20.Alignment = Element.ALIGN_CENTER;
                par20.Add(Environment.NewLine);
                doc.Add(par20);

                iTextSharp.text.Phrase ph19 = new Phrase("\n" + "ИСПОЛНИТЕЛЬ:                     ЗАКАЗЧИК:                     ",
                     new iTextSharp.text.Font(bf, 14,
                         iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par19 = new Paragraph(ph19);
                par19.Alignment = Element.ALIGN_CENTER;
                par19.Add(Environment.NewLine);
                doc.Add(par19);

                iTextSharp.text.Phrase ph21 = new Phrase("\n" + "______________                    ______________                    ",
                     new iTextSharp.text.Font(bf, 14,
                         iTextSharp.text.Font.BOLD, new BaseColor(Color.Black)));
                Paragraph par21 = new Paragraph(ph21);
                par21.Alignment = Element.ALIGN_CENTER;
                par21.Add(Environment.NewLine);
                doc.Add(par21);


                doc.Close();

                //Process proc = new Process();
                //proc.StartInfo.FileName = @"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe";
                //proc.StartInfo.Arguments = String.Format(@"/p /h {0}", saveFileDialog1.FileName);
                //proc.StartInfo.UseShellExecute = false;
                //proc.StartInfo.CreateNoWindow = true;
                //proc.Start();
            }
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
                    odt.AddZag("Договор продажи шкафа №" + r.Next(1, 20));
                    odt.AddElement("г. Ульяновск");
                    odt.AddElement(thisDay.ToString("d"));
                    odt.AddElement("");
                    odt.AddElement("именуемое в дальнейшем Продавец, в лице директора Климов С. А. , действующего на основании Устава, с одной стороны, и " + FIOClienta() + ", именуемый  в дальнейшем Покупатель, с другой стороны, заключили настоящий договор о нижеследующем: ");
                    odt.AddElement("");
                    odt.AddZag("1.ПРЕДМЕТ ДОГОВОРА.");
                    odt.AddElement("       1.1 Продавец обязуется передать в собственность Покупателя мебель (далее «Товар») согласно товарного чека, являющегося неотъемлемой частью настоящего Договора, а Покупатель обязуется принять Товар и оплатить  его стоимость." + "\n" + "1.2. Стоимость товара составляет  (" + CenaDogovora() + ") руб. 00коп. (далее «Стоимость Товара»).");
                    odt.AddElement("");
                    odt.AddZag("2.ОБЯЗАННОСТИ ПРОДАВЦА.");
                    odt.AddElement("       2.1 Продавец обязуется поставить Покупателю товар согласно товарного чека в течение 15 (пятнадцати) рабочих дней с момента внесения Покупателем предоплаты. Датой исполнения заказа считается дата извещения Продавцом Покупателя о прибытии Товара на склад Продавца. Извещение производится по телефонному номеру, оставленному Покупателем, в течение рабочего дня с 9.00 до 18.00 ежедневно. Ответственность за правильность оставленного телефонного номера несет Покупатель.");
odt.AddElement("2.2 В случае задержки поставки Товара Продавец оплачивает Покупателю пеню в размере 0,1 % от суммы предоплаты за каждый день задержки, путем уменьшения стоимости Товара.");
odt.AddElement("2.3 В случае невозможности выполнения Продавцом своих обязательств перед Покупателем, Продавец возвращает Покупателю внесенную ранее предоплату в течение 3 (трех) дней с момента подачи заявления об отказе от поставки Товара.");
odt.AddElement("2.4 Продавец предоставляет Покупателю гарантию на Товар согласно действующему законодательству.");
odt.AddElement("Гарантия предоставляется при условии выполнения Покупателем всех норм и правил эксплуатации Товара и его использования только по назначению. Гарантия осуществляется путем ремонта поврежденного изделия или его замены (в случае невозможности ремонта).");
                    odt.AddElement("");
                    odt.AddZag("3.ОБЯЗАННОСТИ ПОКУПАТЕЛЯ.");
                    double predopl = Convert.ToDouble(textBox1.Text);
                    double ostatok = Convert.ToDouble(CenaDogovora()) - predopl;
                    odt.AddElement("3.1 В момент подписания настоящего Договора Покупатель вносит предоплату в размере ( " + predopl.ToString() + ") руб. 00коп.");
                    odt.AddElement("3.2 Покупатель вносит оставшуюся часть платежа после получения «Товара» в момент подписания акта приема-передачи, что составляет   (" + ostatok + ") руб. 00коп.");
odt.AddElement("3.3 В случае расторжения настоящего Договора Покупателем в одностороннем порядке до факта извещения о выполнении его заказа или при отказе от Товара при надлежащем качестве, Продавец удерживает с Покупателя в счет понесенных издержек по поставке товара 50 % от стоимости товара надлежащего качества.");
                    odt.AddElement("");
                    odt.AddZag("4.ПОРЯДОК ПРИЕМА-ПЕРЕДАЧИ ТОВАРА.");
                    odt.AddElement("4.1 Прием-передача Товара осуществляется при наличии представителей Продавца и Покупателя.");
odt.AddElement("4.2 Покупатель приобретает право собственности на Товар и несет все связанные с этим риски с момента передачи товара Продавцом Покупателю.");
odt.AddElement("4.3 Датой передачи Товара считается дата подписания Акта приема-передачи товара (совершение надписи в акте приема-передачи товара) уполномоченными представителями Сторон настоящего Договора.");
odt.AddElement("4.4 В случае, если при приемке Товара какая-либо из позиций, указанных в товарном чеке оказалась с механическими повреждениями и/или не соответствует или отсутствует (не поставлена), в Акте приема-передачи Товара делается соответствующая запись (отметка) о необходимости замены или поставки Товара в оговоренные сроки. Соответственно проставляются дата и подписи представителей Сторон.");
odt.AddElement("В случае выявления при приемке некачественного Товара Покупатель имеет право немедленно предъявить продавцу претензию в письменной форме.");
odt.AddElement("4.5 После подписания Акта приема-передачи Товара Покупатель не имеет права предъявить Продавцу претензию по количеству Товара, а также по повреждениям, не указанным в Акте сдачи- приемки.");
odt.AddElement("4.6 Качество товара должно соответствовать стандартам, установленным производителем.");
odt.AddElement("4.7 Продавец обязуется рассмотреть претензии Покупателя, и дать на них ответ в течение 10 (десяти) рабочих дней.");
odt.AddElement("4.8 Удовлетворение обоснованных претензий Покупателя, производится за счет Продавца в течение 20 (двадцати) дней с момента предъявления Покупателем претензии.");
                    odt.AddElement("");
                    odt.AddElement("\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "Продавец_____________________        Покупатель_____________________");
                    odt.AddElement("");
                    odt.AddZag("5.ФОРС-МАЖОРНЫЕ ОБСТОЯТЕЛЬСТВА.");
                    odt.AddElement("5.1 Стороны освобождаются от выполнения обязательств по настоящему Договору в случае наступления обстоятельств, возникших помимо воли и желания Сторон и которые нельзя предвидеть или избежать (форс-мажорные обстоятельства), включая объявленную или фактическую войну, гражданские волнения, эпидемии, блокаду, эмбарго, землетрясения, наводнения, пожары и другие стихийные бедствия, изменения действующего законодательства, акты и действия органов государственной власти и управления федерального и местного значения и т. д.");
odt.AddElement("5.2 В случае наступления форс-мажорных обстоятельств пострадавшая Сторона обязана в кратчайшие сроки предупредить другую Сторону о невозможности исполнения своих обязательств по настоящему Договору.");
odt.AddElement("5.3 При этом ни одна Сторона не может требовать возмещения каких-либо убытков или потерь, связанных с невыполнением Договора.");
                    odt.AddElement("");
                    odt.AddZag("6.ПРОЧИЕ УСЛОВИЯ.");
                    odt.AddElement("6.1 Любые изменения и дополнения к настоящему Договору будут действительны только в том случае, если они совершены в письменной форме и подписаны представителями обеих Сторон.");
odt.AddElement("6.2 В случаях, не предусмотренных настоящим Договором, Стороны руководствуются действующим гражданским законодательством.");
odt.AddElement("6.3 При возникновении в процессе исполнения настоящего Договора спора, который Стороны не смогут урегулировать путем переговоров, спор будет разрешаться в суде по месту нахождения Продавца.");
odt.AddElement("6.4 Настоящий Договор вступает в силу с момента его подписания и действует до полного исполнения Сторонами своих обязательств по нему.");
odt.AddElement("6.5 Настоящий Договор составлен в двух экземплярах, по одному для каждой из Сторон.");
                    odt.AddElement("");
                    odt.AddZag("7.АДРЕСА И РЕКВИЗИТЫ СТОРОН.");
                    odt.AddElement("");
                    odt.AddElement("ИСПОЛНИТЕЛЬ:                     ЗАКАЗЧИК:                     ");
                    odt.AddElement("");
                    odt.AddElement("______________                    ______________                    ");
                    string result = odt.SaveFile();
                    MessageBox.Show("Файлы успешно сохранены!");
                }
            }
        }

        public string FIOClienta()
        {
            string fio = "";
            string name = "";
            string otch = "";

            string queryString = "SELECT F,I,O FROM Client WHERE F=N'" + comboBox2.Text + "'";

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
        
        public string CenaDogovora()
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
    }
}
