using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Konstructor.FormsAndDS
{
    public partial class Otchet : Form
    {
        bool flag = true;
        /// <summary>
        /// вторая версия
        /// </summary>
        List<int> Lmonth = addMonth();
        List<int> Lkolvo = addKolvo();
        int kolvoOfmonth;
        int max = 1;


        string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\KURS\BD\KBD.MDF;
                                    Integrated Security=True;
                                     Connect Timeout=30";

        public Otchet()
        {
            InitializeComponent();
        }
        static List<int> addMonth()
        {
            List<int> lmonth = new List<int>();
            for (int i = 1; i < 13; i++)
                lmonth.Add(i);
            return lmonth;
        }
        static List<int> addKolvo()
        {
            List<int> lkolvo = new List<int>();
            for (int i = 0; i < 1; i++)
                lkolvo.Add(i);
            return lkolvo;
        }

        public int KolvoProd(int m1, int y1)
        {
            int count = 0;
            string queryString = "SELECT COUNT(Id) FROM Prodaza WHERE DATEPART(\"MONTH\",Prodaza.DataOfSell)='" + m1 + "' AND DATEPART(\"YEAR\",Prodaza.DataOfSell)='" + y1 + "'";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        count = Convert.ToInt32(reader[0]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return count;

        }

        public void drawKoord(PaintEventArgs e)
        {
            period();
            int h = pictureBox1.Height;
            int w = pictureBox1.Width;
            //int max= 
            string[] nameOFmonth = new string[13] { "нулевой", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь  ", "Октябрь", "Ноябрь", "Декабрь" };

            const int shag_ot_y = 90;

            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(70, 20), new Point(70, h - 50));
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(70, h - 50), new Point(w - 50, h - 50));
            //стрелки: нижняя, верхняя
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(w - 60, h - 45), new Point(w - 50, h - 50));
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(w - 60, h - 55), new Point(w - 50, h - 50));
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(70, 20), new Point(65, 30));
            e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(70, 20), new Point(75, 30));
            e.Graphics.DrawString("месяца", new Font("Arial", 8), Brushes.Black, new Point(w - 50, h - 40));
            e.Graphics.DrawString("Кол-во \nпродаж", new Font("Arial", 8), Brushes.Black, new Point(10, 15));

            int del = (w - 200) / Lmonth.Count;
            int shag = 0;
            for (int imonth = 0; imonth < Lmonth.Count; imonth++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(shag_ot_y + shag, h - 55), new Point(shag_ot_y + shag, h - 45));
                e.Graphics.DrawString(nameOFmonth[Lmonth[imonth]], new Font("Arial", 7), Brushes.Black, new Point(shag_ot_y + shag - 5, h - 40));
                shag += del;
            }

            //кол-во продаж
            int delFORkolvo = (h - 100) / max;
            shag = delFORkolvo;
            for (int pr = 0; pr < max; pr++)
            {
                int kolvo = pr + 1;
                e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(65, h - 50 - shag), new Point(75, h - 50 - shag));
                e.Graphics.DrawString(kolvo.ToString(), new Font("Arial", 8), Brushes.Black, new Point(55, h - 50 - shag));
                shag += delFORkolvo;
            }

            //точки
            int sh = 0;
            int sh1 = 0;
            int next = 1;
            for (int i = 0; i < Lkolvo.Count; i++)
            {
                e.Graphics.FillEllipse(Brushes.Red, shag_ot_y + sh - 3, h - 50 - Lkolvo[i] * delFORkolvo, 6, 6);
                sh1 += del;
                if (next == Lkolvo.Count)
                    break;
                else
                {
                    e.Graphics.DrawLine(new Pen(Brushes.Red), new Point(shag_ot_y + sh, h - 50 - Lkolvo[i] * delFORkolvo),
                        new Point(shag_ot_y + sh1 + 1, h - 50 - Lkolvo[next] * delFORkolvo));
                    sh += del;
                    next++;
                }
            }




        }

        public void period()
        {
            Lmonth.Clear();
            Lkolvo.Clear();
            kolvoOfmonth = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            int m1 = dateTimePicker1.Value.Month;
            int m2 = dateTimePicker2.Value.Month;
            int y1 = dateTimePicker1.Value.Year;
            int y2 = dateTimePicker2.Value.Year;

            int countOfmonth = 12;
            if (y1 == y2)
                countOfmonth = m2;

            while (y1 <= y2)
            {
                while (m1 <= countOfmonth)
                {
                    Lmonth.Add(m1);
                    Lkolvo.Add(KolvoProd(m1, y1));
                    m1++;
                }
                y1++;
                m1 = 1;
                if (y1 == y2)
                    countOfmonth = m2;
            }
            max = Lkolvo.Max();
            if (max == 0)
                max = 1;
        }

        private void Otchet_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int m1 = dateTimePicker1.Value.Month;
            int m2 = dateTimePicker2.Value.Month;
            int y1 = dateTimePicker1.Value.Year;
            int y2 = dateTimePicker2.Value.Year;
            int d1 = dateTimePicker1.Value.Day;
            int d2 = dateTimePicker2.Value.Day;

            if ((y1 > y2) || ((y1 == y2) && (m1 > m2)) || ((y1 == y2) && (m1 == m2) && (d1 > d2)))
            { MessageBox.Show("Первая дата больше второй"); Lmonth = addMonth(); Lkolvo = addKolvo(); return; }
            //if((y1==y2)&&(m1==m2)&&(d1==d2))
            //{ MessageBox.Show("Одинаковые даты"); Lmonth = addMonth(); Lkolvo = addKolvo(); return; }
            period();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (flag == true)
            {
                drawKoord(e);
               // pictureBox1.Refresh();
                Invalidate();
                Update();
            }
            
        }
       
        public void saveEX()
        {
            saveFileDialog1.Filter = "Файлы Excel |*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                flag = false;
                //создание xmls
                //           Excel.Application excelapp = new Excel.Application();
                //           excelapp.SheetsInNewWorkbook = 3;
                //           excelapp.Workbooks.Add(Type.Missing);
                //           excelapp.DisplayAlerts = true;
                //           excelapp.Visible = true;
                //           Excel.Workbooks excelappworkbooks = excelapp.Workbooks;
                //           Excel.Workbook excelappworkbook = excelappworkbooks[1];
                //           excelappworkbook.SaveAs(@saveFileDialog1,  //object Filename
                //Excel.XlFileFormat.xlExcel12,//xlExcel9795,          //object FileFormat
                //Type.Missing,                       //object Password 
                //Type.Missing,                       //object WriteResPassword  
                //Type.Missing,                       //object ReadOnlyRecommended
                //Type.Missing,                       //object CreateBackup
                //Excel.XlSaveAsAccessMode.xlNoChange,//XlSaveAsAccessMode AccessMode
                //Type.Missing,                       //object ConflictResolution
                //Type.Missing,                       //object AddToMru 
                //Type.Missing,                       //object TextCodepage
                //Type.Missing,                       //object TextVisualLayout
                //Type.Missing);
                //           excelapp.Quit();
                //           //}
                //           MessageBox.Show("");

                Excel.Application app = new Excel.Application();
                System.Diagnostics.Process excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL").Last();
                app.Workbooks.Open(@"D:\Kurs\Konstructor\Konstructor\bin\Debug\book");
                Excel.Workbook book = app.ActiveWorkbook;
                Excel.Sheets excelsheets = book.Worksheets;
                Excel.Worksheet sheet = (Excel.Worksheet)book.Worksheets[1];
                Excel.Range excelcells1 = sheet.get_Range("A1", "B1");
                excelcells1 = sheet.get_Range("A1", "B1").Cells;
                excelcells1 = sheet.get_Range("A1", "B1").Rows;

                string sheet2 = Convert.ToString(Lmonth.Count+1);
                Excel.Range excelcells2 = sheet.get_Range("A2", "B" + sheet2);
                excelcells2 = sheet.get_Range("A2", "B" + sheet2).Cells;
                excelcells2 = sheet.get_Range("A2", "B" + sheet2).Rows;

                Excel.Range excelcells = sheet.get_Range("A1", "B" + sheet2);
                excelcells.Borders.ColorIndex = 1;
                excelcells.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlInsideHorizontal].Weight = Excel.XlBorderWeight.xlThin;
                excelcells.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlInsideVertical].Weight = Excel.XlBorderWeight.xlThin;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                excelcells.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;

                string[] titles = { "Месяца", "Количество продаж" };
                for (int i = 0; i < 2; i++)
                {
                    excelcells1 = (Excel.Range)sheet.Cells[1, i + 1];
                    excelcells1.Value2 = titles[i];
                    excelcells1.Font.Size = 14;
                    excelcells1.Font.Italic = false;
                    excelcells1.Font.Bold = true;
                    excelcells1.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells1.VerticalAlignment = Excel.Constants.xlCenter;
                    excelcells1.Borders.ColorIndex = 1;
                    excelcells1.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    excelcells1.Borders.Weight = Excel.XlBorderWeight.xlThick;

                }
                string[] months = {"нулевой", "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
                for (int i = 0; i < Lmonth.Count; i++)
                {
                    excelcells2 = (Excel.Range)sheet.Cells[i + 2, 1];
                    excelcells2.Value2 = months[Lmonth[i]];
                    excelcells2.Font.Size = 12;
                    excelcells2.Font.Italic = false;
                    excelcells2.Font.Bold = false;
                    excelcells2.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells2.VerticalAlignment = Excel.Constants.xlCenter;
                }
                for (int i = 0; i < Lkolvo.Count; i++)
                {
                    excelcells2 = (Excel.Range)sheet.Cells[i + 2, 2];
                    excelcells2.Value2 = Lkolvo[i];
                    excelcells2.Font.Size = 12;
                    excelcells2.Font.Italic = false;
                    excelcells2.Font.Bold = false;
                    excelcells2.HorizontalAlignment = Excel.Constants.xlCenter;
                    excelcells2.VerticalAlignment = Excel.Constants.xlCenter;
                }
                
                sheet.get_Range("A1", "B" + sheet2).EntireColumn.AutoFit();
                sheet.get_Range("A1", "B" + sheet2).EntireRow.AutoFit();

                excelcells2 = sheet.get_Range("A2", "B" + sheet2);
                excelcells2.Select();

                Excel.Chart excelchart = (Excel.Chart)app.Charts.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                app.ActiveChart.ChartType = Excel.XlChartType.xlLine;

                app.ActiveChart.HasTitle = true;
                app.ActiveChart.ChartTitle.Text = "Отчет по продажам";
                app.ActiveChart.ChartTitle.Font.Size = 13;
                app.ActiveChart.ChartTitle.Font.Color = 0;
                app.ActiveChart.ChartTitle.Shadow = true;
                app.ActiveChart.ChartTitle.Border.LineStyle = Excel.Constants.xlSolid;

                //((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                //    Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                //((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                //    Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Категории";
                //((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlValue,
                //    Excel.XlAxisGroup.xlPrimary)).HasTitle = true;
                //((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlValue,
                //    Excel.XlAxisGroup.xlPrimary)).AxisTitle.Text = "Значения";
                //((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlSeriesAxis,
                //    Excel.XlAxisGroup.xlPrimary)).HasTitle = false;

                ((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                        Excel.XlAxisGroup.xlPrimary)).HasMajorGridlines = true;
                ((Excel.Axis)app.ActiveChart.Axes(Excel.XlAxisType.xlCategory,
                        Excel.XlAxisGroup.xlPrimary)).HasMinorGridlines = false;

                app.ActiveChart.HasLegend = true;
                app.ActiveChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionLeft;
                ((Excel.LegendEntry)app.ActiveChart.Legend.LegendEntries(1)).Font.Size = 12;
                //((Excel.LegendEntry)app.ActiveChart.Legend.LegendEntries(2)).Font.Size = 13;

                //Excel.SeriesCollection seriesCollection =(Excel.SeriesCollection)app.ActiveChart.SeriesCollection(Type.Missing);
                //Excel.Series series = seriesCollection.Item(1);
                //series.Name = "Первый ряд";

                app.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, "Лист1");
                excelsheets = book.Worksheets;
                sheet = (Excel.Worksheet)excelsheets.get_Item(1);
                sheet.Shapes.Item(1).IncrementLeft(-70);
                sheet.Shapes.Item(1).IncrementTop((float)0.5);
                sheet.Shapes.Item(1).Height = 450;
                sheet.Shapes.Item(1).Width = 700;

                app.ActiveWorkbook.SaveAs(saveFileDialog1.FileName);
                book.Close();
                app.Quit();
                excelProc.Kill();
                flag = true;                
                MessageBox.Show("Сохранено!");
                Invalidate();
                Update();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          //  pictureBox1.Visible = false;
            saveEX();           

        }

    }
}
