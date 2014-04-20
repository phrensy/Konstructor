using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace Konstructor
{
    public partial class Form1 : Form
    {
        Shcaf.BaseShcaf _shcaf;
        DrawShcaf _drawshcaf = new DrawShcaf();
        public bool GetImage = false; // начали рисовать новый элемент
        public Bitmap bit; // рисунок нового элемента
        public Bitmap allbit;//весь рисунок
        public Form1()
        {
            InitializeComponent();

            _shcaf = new Shcaf.BaseShcaf((int)WidthValue.Value, (int)HightValue.Value,(int)DepthValue.Value);
            _shcaf.listE = new List<Shcaf.Element>();
            
            _drawshcaf.wp = pictureBox1.Width;
            _drawshcaf.hp = pictureBox1.Height;
            _drawshcaf.DrawMainShcaf(_shcaf);
            label11.Text = "";

            pictureBox1.BackColor = Color.White;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _shcaf.Width = (int)WidthValue.Value;
            _shcaf.Height = (int)HightValue.Value;
            _shcaf.Depth = (int)DepthValue.Value;
            _shcaf.listE.Clear();
            _shcaf.listS.Clear();
            _shcaf.ListText.Clear();
            _drawshcaf.wp = pictureBox1.Width;
            _drawshcaf.hp = pictureBox1.Height;
            //_drawshcaf.pointforRedraw = 0;
            //ChooseColorToolStripMenuItem.Text = "Выбор цвета";
            _drawshcaf.DrawMainShcaf(_shcaf);
            pictureBox1.Refresh();
            if (File.Exists("111.png"))
            {
                File.Delete("111.png");
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            _drawshcaf.Draw(_shcaf, e, bit, GetImage,label11);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (GetImage == true)
            {
                _drawshcaf.DrawNewElement(_shcaf);
                GetImage = false;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (GetImage == true)
            {
                _drawshcaf.newElement.Rec.X = e.X;
                _drawshcaf.newElement.Rec.Y = e.Y;
                pictureBox1.Refresh();
            }

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            GetImage = false;
            pictureBox1.Refresh();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetImage = true;
                bit = new Bitmap(pictureBox2.Image);
                _drawshcaf.newElement.Id = Shcaf.IdElement.Polka;
            }

        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetImage = true;
                bit = new Bitmap(pictureBox3.Image);
                _drawshcaf.newElement.Id = Shcaf.IdElement.Stena;
            }

        }
        
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetImage = true;
                bit = new Bitmap(pictureBox4.BackgroundImage);
                _drawshcaf.newElement.Id = Shcaf.IdElement.Yschik;
            }
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetImage = true;
                bit = new Bitmap(pictureBox5.BackgroundImage);
                _drawshcaf.newElement.Id = Shcaf.IdElement.Del;
            }
        }

        private void pictureBox6_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                GetImage = true;
                bit = new Bitmap(pictureBox6.BackgroundImage);
                _drawshcaf.newElement.Id = Shcaf.IdElement.Vesh;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Drawing.Rectangle r = pictureBox1.RectangleToScreen(pictureBox1.ClientRectangle);
            Bitmap b = new Bitmap(r.Width - 5, r.Height - 5);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(r.Location, new Point(0, 0), r.Size);
            b.Save("111.png");

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "pdf|*.pdf";
            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PdfDocument doc = new PdfDocument();

                // Set font encoding to unicode
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
            XFont font = new XFont("Times New Roman", 12, XFontStyle.Regular, options);
            XImage img = XImage.FromFile("111.png");
            PdfPage page = doc.AddPage();

                XGraphics xgr = XGraphics.FromPdfPage(page);
                
                xgr.DrawImage(img,30,80,538,242);
                xgr.DrawString("Спроектированный шкаф: ", font, XBrushes.Black,new XRect(20, 50, page.Width - 200, 600), XStringFormats.TopCenter);

                doc.Save(@saveFileDialog1.FileName + ".pdf");
                doc.Close();
            }
           // File.Delete("111.png");
        }

        private void DybSvetlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseColorToolStripMenuItem.Text = "Дуб светлый";
            _drawshcaf.pointforRedraw = 1;
            pictureBox1.Refresh();
        }

        private void VengeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseColorToolStripMenuItem.Text = "Венге";
           _drawshcaf.pointforRedraw = 2;
            pictureBox1.Refresh();
        }

        private void KlenNaturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseColorToolStripMenuItem.Text = "Клён натуральный";
            _drawshcaf.pointforRedraw = 0;
            pictureBox1.Refresh();
        }

        private void MDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseMaterialToolStripMenuItem.Text = "МДФ";
            _drawshcaf.pointforStoimost = 0;
            pictureBox1.Refresh();
        }

        private void DSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseMaterialToolStripMenuItem.Text = "ДСП";
            _drawshcaf.pointforStoimost = 1;
            pictureBox1.Refresh();
        }

        private void DVPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChooseMaterialToolStripMenuItem.Text = "ДВП";
            _drawshcaf.pointforStoimost = 2;
            pictureBox1.Refresh();
        }

       

        


    }
}
