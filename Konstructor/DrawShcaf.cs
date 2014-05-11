using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Konstructor.Shcaf;

namespace Konstructor
{
    class DrawShcaf
    {
        public DrawShcaf() { }

        private Element _element = new Element();

        public Element newElement = new Element();

        public int pointforRedraw = 0;
        public int pointforStoimost = 0;

        public int wp;//ширина шкафа
        public int hp;//высота шкафа

        int w = 0;//ширина шкафа
        int h = 0;//высота шкафа
        int _x = 0;
        int _y = 0;

        private int Gup = 0;//для рисования полосок у глубины
        private int Gdown = 0;//для рисования полосок у глубины

        public int countStenka = 0;
        public int countZadnya = 0;
        public int countYashik = 0;
        public int countVeshalka = 0;

        private bool TryDraw = true;

        Pen myPen = new Pen(Color.Chocolate, 1);

        private void SizeShcah(BaseShcaf _baseShcaf)
        {
            w = _baseShcaf.Width / 4;
            h = _baseShcaf.Height / 4;

            if (h > hp - 50)//если высота шкафа больше hp = pictureBox1.Height;
            {
                h = hp - 200;
            }

            if (w > wp - 50)//wp = pictureBox1.Width;
            {
                w = wp - 200;
            }

            _x = wp / 2- w / 2;//координаты шкафа
            _y = hp / 2 - h / 2;
        }

        //главная функция отрисовки всего шкафа
        public void Draw(Shcaf.BaseShcaf _baseShcaf, PaintEventArgs e, Bitmap bit, bool getimage, Label label)
        {
            SizeShcah(_baseShcaf);

            _baseShcaf.listE = _baseShcaf.listE.OrderBy(x => x.level).ToList();

            Redraw(_baseShcaf);

            Razmer(_baseShcaf);
            Counting(_baseShcaf);
            Stoimost(_baseShcaf);

            //отрисовка размерной сетки
            foreach (var b in _baseShcaf.listS)
            {
                if (b.ListPen != null)
                {
                    foreach (var pen in b.ListPen)
                    {
                        e.Graphics.DrawLine(pen.MyPen, pen.Start, pen.End);
                    }
                }
            }
            //отрисовка цифр размера
            if (_baseShcaf.ListText != null)
            {
                foreach (var b in _baseShcaf.ListText)
                {
                    e.Graphics.DrawString(b.SizeText, new Font("Arial", 7), Brushes.Black, b.Start);
                }
            }

            //отрисовка стоимости
            if (_baseShcaf.CountList != null)
            {
                foreach (var b in _baseShcaf.CountList)
                {
                   label.Text = b.SizeText.ToString();
                }
            }

            //отрисовка элементов
            foreach (var b in _baseShcaf.listE)
            {
                if (b.FonImg != null)
                {
                    e.Graphics.DrawImage(b.FonImg, b.FonRec);
                }
                e.Graphics.DrawImage(b.Img, b.Rec);

                if (b.ListPen.Count != 0)
                {
                    foreach (var pen in b.ListPen)
                    {
                        e.Graphics.DrawLine(pen.MyPen, pen.Start, pen.End);
                    }

                }
            }

            //отрисовка виртуального элемента
            if (getimage == true)
            {
                Rectangle recNew;
                newElement.Img = bit;
                newElement.Rec = new Rectangle(newElement.Rec.X, newElement.Rec.Y, 50, 50);
                switch (newElement.Id)
                {
                    case IdElement.Vesh:
                        {
                            newElement.Rec = new Rectangle(newElement.Rec.X, newElement.Rec.Y, bit.Width, bit.Height);
                            break;
                        }
                    case IdElement.Yschik:
                        {
                            newElement.Rec = new Rectangle(newElement.Rec.X, newElement.Rec.Y, bit.Width, bit.Height);
                            break;
                        }
                    case IdElement.Polka:
                        {
                            newElement.Rec = new Rectangle(newElement.Rec.X, newElement.Rec.Y, bit.Width, 7);
                            break;
                        }
                }
                recNew = GetXY(_baseShcaf);
                TryDraw = TryOnDraw(recNew, _baseShcaf);
                if (newElement.Rec.X > _x && newElement.Rec.Y > _y && newElement.Rec.X < _x + w &&
                    newElement.Rec.Y < _y + h)
                {

                    if (TryDraw)
                    {
                        e.Graphics.DrawImage(Image.FromFile("Element/FonNewElement.png"), recNew);
                    }
                    else
                    {
                        e.Graphics.DrawImage(Image.FromFile("Element/FonNewElementNo.png"), recNew);

                    }
                }
                e.Graphics.DrawImage(newElement.Img, newElement.Rec);
            }

        }

        //нарисавать новый элемент шкафа
        public void DrawNewElement(BaseShcaf _baseShcaf)
        {
            switch (newElement.Id)
            {
                case Shcaf.IdElement.Polka:
                    {
                        Image topPol = Image.FromFile("Element/RNewPolka.bmp");
                        _element = new Shcaf.Element(topPol, GetXY(_baseShcaf));
                        _element.Id = IdElement.Polka;
                        _element.FonRec = GetFonXY(_element);
                        _element.FonImg = Image.FromFile("BaseShkaf/NewPolka.bmp");
                        Okontovka(_element);
                        break;
                    }
                case Shcaf.IdElement.Stena:
                    {
                        Image rightStena = Image.FromFile("Element/LNewStena.bmp");
                        _element = new Shcaf.Element(rightStena, GetXY(_baseShcaf));
                        _element.Id = IdElement.Stena;
                        _element.FonRec = GetFonXY(_element);
                        _element.FonImg = Image.FromFile("BaseShkaf/NewStena.bmp");
                        Okontovka(_element);
                        break;
                    }
                case Shcaf.IdElement.Yschik:
                    {
                        Image ysch = Image.FromFile("Element/Yschik.png");
                        _element = new Element(ysch, GetXY(_baseShcaf));
                        _element.Id = IdElement.Yschik;
                        break;
                    }
                case Shcaf.IdElement.Vesh:
                    {
                        Image ves = Image.FromFile("Element/Vesh1.png");
                        _element = new Element(ves, GetXY(_baseShcaf));
                        _element.Id = IdElement.Vesh;
                        break;
                    }
                case Shcaf.IdElement.Del:
                    {
                        Delete(_baseShcaf);
                        _baseShcaf.listS.Clear();
                        _baseShcaf.ListText.Clear();
                        return;
                    }
            }
            TryDraw = TryOnDraw(GetXY(_baseShcaf), _baseShcaf);
            if (TryDraw)
            {

                _element.level = Prioritet(_baseShcaf, _element);
                if ((_element.Rec.X > _x) && (_element.Rec.Y > _y) && (_element.Rec.X < _x + _baseShcaf.Width / 4) && (_element.Rec.Y < _y + _baseShcaf.Height / 4))
                {
                    _baseShcaf.listE.Add(_element);
                    _baseShcaf.listS.Clear();
                    _baseShcaf.ListText.Clear();
                }
            }


        }

        private void Delete(BaseShcaf _baseShcaf)
        {
            int x = newElement.Rec.X;
            int y = newElement.Rec.Y;


            foreach (var b in _baseShcaf.listE.ToArray())//по возрастанию
            {
                if (b.level != 0)
                {
                    switch (b.Id)
                    {
                        case IdElement.Polka:
                            {
                               if (b.Rec.Left < x && b.Rec.Right > x && b.Rec.Top < y && b.Rec.Bottom > y)
                                {
                                    _baseShcaf.listE.Remove(b);
                                    return;
                                }
                                break;
                            }
                        case IdElement.Yschik:
                            {
                                if (b.Rec.Left < x && b.Rec.Right > x && b.Rec.Top < y && b.Rec.Bottom > y)
                                {
                                    _baseShcaf.listE.Remove(b);
                                    return;
                               }
                                break;
                            }
                        case IdElement.Vesh:
                            {
                               if (b.Rec.Left < x && b.Rec.Right > x && b.Rec.Top < y && b.Rec.Bottom > y)
                                {
                                    _baseShcaf.listE.Remove(b);
                                    return;
                                }
                                break;
                            }
                        case IdElement.Stena:
                            {
                                if (b.Rec.Left < x && b.Rec.Right > x && b.Rec.Top < y && b.Rec.Bottom > y)
                                {
                                    _baseShcaf.listE.Remove(b);
                                    return;
                               }
                                break;
                            }

                    }
                }
            }
        }

        //размерная сетка
        private void Razmer(BaseShcaf _baseShcaf)
        {
            BaseShcaf tempS = new BaseShcaf();

            BaseShcaf tempP = new BaseShcaf();

            foreach (var b in _baseShcaf.listE)
            {
                switch (b.Id)
                {
                    case IdElement.Stena:
                        {
                            tempS.listE.Add(b);
                            break;
                        }
                    case IdElement.Polka:
                        {
                            tempP.listE.Add(b);
                            break;
                        }
                }
            }

            tempS.listE = tempS.listE.OrderBy(x => x.Rec.X).ToList();
            tempP.listE = tempP.listE.OrderBy(x => x.Rec.Y).ToList();

            double constS = _baseShcaf.Width / (tempS.listE.Last().Rec.Left - tempS.listE.First().Rec.Left);//расчитали коофициент умножения
            double constP = _baseShcaf.Height / (tempP.listE.Last().Rec.Top - tempP.listE.First().Rec.Top);//расчитали коофициент умножения

            SizeShcaf ss = new SizeShcaf();

            int j = 0;

            for (int i = 0; i < tempS.listE.Count; i++)
            {
                if (i == tempS.listE.Count - 1) break;
                //левая палка снизу
                ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE[i].Rec.Left, tempS.listE.First().Rec.Bottom),
                                            new Point(tempS.listE[i].Rec.Left, tempS.listE.First().Rec.Bottom +30)));
                //правая палка снизу
                ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE[i + 1].Rec.Left, tempS.listE.First().Rec.Bottom),
                                new Point(tempS.listE[i + 1].Rec.Left, tempS.listE.First().Rec.Bottom + 30)));
                //палка между ними снизу
               ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE[i].Rec.Left, tempS.listE.First().Rec.Bottom + 20),
                                new Point(tempS.listE[i + 1].Rec.Left, tempS.listE.First().Rec.Bottom + 20)));
                //цифра размера
                int st = (int)((tempS.listE[i + 1].Rec.Left - tempS.listE[i].Rec.Left) * constS);//считаем длину между стенами
                j = j + st;
                if (i + 1 == tempS.listE.Count - 1) 
                    st = st + (_baseShcaf.Width - j);
                _baseShcaf.ListText.Add(new DrawText(new Point((tempS.listE[i].Rec.Left + tempS.listE[i + 1].Rec.Left) / 2 - 10, tempS.listE.First().Rec.Bottom + 5)
                                , Convert.ToString(st)));
            }
            j = 0;
            for (int i = 0; i < tempP.listE.Count; i++)
            {
                if (i == tempP.listE.Count - 1) break;
                //левая палка справа
                ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.First().Rec.Right, tempP.listE[i].Rec.Top),
                               new Point(tempP.listE.First().Rec.Right + 30, tempP.listE[i].Rec.Top)));
                //правая палка
                ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.First().Rec.Right - 1, tempP.listE[i + 1].Rec.Top),
                                new Point(tempP.listE.First().Rec.Right + 30, tempP.listE[i + 1].Rec.Top)));
                //палка между ними
                ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.First().Rec.Right + 20, tempP.listE[i].Rec.Top),
                                new Point(tempP.listE.First().Rec.Right + 20, tempP.listE[i + 1].Rec.Top)));

                int st = (int)((tempP.listE[i + 1].Rec.Top - tempP.listE[i].Rec.Top) * constP);//считаем длину между полками
                j = j + st;
                if (i + 1 == tempP.listE.Count - 1) 
                    st = st + (_baseShcaf.Height - j);
                //цифра размера
                _baseShcaf.ListText.Add(new DrawText(new Point(tempP.listE.First().Rec.Right + 25, (tempP.listE[i].Rec.Top + tempP.listE[i + 1].Rec.Bottom) / 2 - 10)
                                , Convert.ToString(st)));
            }

            //левая палка сверху
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE.First().Rec.Left, tempS.listE.First().Rec.Top),
                                        new Point(tempS.listE.First().Rec.Left, tempS.listE.First().Rec.Top - 30)));
            //правая палка
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE.Last().Rec.Right - 1, tempS.listE.Last().Rec.Top),
                            new Point(tempS.listE.Last().Rec.Right - 1, tempS.listE.Last().Rec.Top - 30)));
            //палка между ними
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempS.listE.First().Rec.Left, tempS.listE.First().Rec.Top - 20),
                            new Point(tempS.listE.Last().Rec.Right - 1, tempS.listE.Last().Rec.Top - 20)));

            //цифра размера
            _baseShcaf.ListText.Add(new DrawText(new Point((tempS.listE.First().Rec.Left + tempS.listE.Last().Rec.Right) / 2, tempS.listE.First().Rec.Top - 32)
                            , Convert.ToString(_baseShcaf.Width)));
            //левая палка справа
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.First().Rec.Left, tempP.listE.First().Rec.Top),
                           new Point(tempP.listE.First().Rec.Left - 30, tempP.listE.First().Rec.Top)));
            //правая палка
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.Last().Rec.Left - 1, tempP.listE.Last().Rec.Bottom),
                            new Point(tempP.listE.Last().Rec.Left - 31, tempP.listE.Last().Rec.Bottom)));
            //палка между ними
            ss.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(tempP.listE.First().Rec.Left - 20, tempP.listE.First().Rec.Top),
                            new Point(tempP.listE.Last().Rec.Left - 20, tempP.listE.Last().Rec.Bottom)));

            //цифра размера
            _baseShcaf.ListText.Add(new DrawText(new Point(tempP.listE.First().Rec.Left - 50, (tempP.listE.First().Rec.Top + tempP.listE.Last().Rec.Bottom) / 2)
                            , Convert.ToString(_baseShcaf.Height)));

            _baseShcaf.listS.Add(ss);
        }

        private bool TryOnDraw(Rectangle rec, BaseShcaf _baseShcaf)
        {
            foreach (var b in _baseShcaf.listE)
            {
                switch (newElement.Id)
                {
                    case IdElement.Polka:
                        {
                            if (b.Id != IdElement.Zadn)
                            {
                                if (newElement.Rec.Y >= b.Rec.Top - newElement.Rec.Height - 10 &&
                                    newElement.Rec.Y <= b.Rec.Bottom + 10 &&
                                    b.Rec.Left < newElement.Rec.X && b.Rec.Right > newElement.Rec.X)
                                    return false;
                            }
                            break;
                        }
                    case IdElement.Stena:
                        {
                            if (b.Id != IdElement.Zadn)
                            {
                                if (b.Rec.Top > rec.Top && b.Rec.Bottom < rec.Bottom && b.Rec.Left < rec.Left &&
                                    b.Rec.Right > rec.Right)
                                    return false;

                                if (newElement.Rec.X >= b.Rec.Left - b.Rec.Width - 10 && newElement.Rec.X <= b.Rec.Right + 10 &&
                                    newElement.Rec.Y > b.Rec.Top && newElement.Rec.Y < b.Rec.Bottom)
                                    return false;
                            }
                            break;
                        }
                    case IdElement.Yschik:
                        {
                            if (b.Id != IdElement.Zadn)
                            {
                                if (newElement.Rec.Y >= b.Rec.Top - newElement.Rec.Height && newElement.Rec.Y <= b.Rec.Bottom &&
                                    b.Rec.Left < newElement.Rec.X && b.Rec.Right > newElement.Rec.X)
                                    return false;

                            }
                            break;
                        }
                    case IdElement.Vesh:
                        {
                            if (b.Id != IdElement.Zadn)
                            {
                                if (newElement.Rec.Y >= b.Rec.Top - newElement.Rec.Height && newElement.Rec.Y <= b.Rec.Bottom &&
                                    b.Rec.Left < newElement.Rec.X && b.Rec.Right > newElement.Rec.X)
                                    return false;

                            }
                            break;
                        }
                }

            }
            return true;
        }

        private Rectangle GetXY(Shcaf.BaseShcaf _baseShcaf)
        {
            int minX = 1800;
            int maxX = 1800;

            Rectangle rec = new Rectangle();

            TryDraw = true;

            foreach (var b in _baseShcaf.listE)
            {
                if (b.Id == IdElement.Zadn)
                    continue;
                switch (newElement.Id)
                {
                    case Shcaf.IdElement.Polka:
                        {
                            if (b.Id == IdElement.Stena)
                            {

                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right <= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (minX >= newElement.Rec.X - b.Rec.X)
                                    {
                                        minX = newElement.Rec.X - b.Rec.X;
                                        Gup = b.FonRec.Right;
                                    }
                                }
                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right >= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (maxX >= b.Rec.Right - newElement.Rec.X)
                                    {
                                        maxX = b.Rec.Right - newElement.Rec.X;
                                        Gdown = b.FonRec.Left;
                                    }
                                }
                                rec.Width = minX + maxX - b.Img.Width * 2;
                                rec.X = newElement.Rec.X - minX + b.Img.Width;
                                rec.Y = newElement.Rec.Y;
                                rec.Height = b.Img.Width;
                            }
                            break;
                        }
                    case Shcaf.IdElement.Stena:
                        {
                            if (b.Id == IdElement.Polka)
                            {
                                if (b.Rec.Y <= newElement.Rec.Y && b.Rec.Right >= newElement.Rec.X &&
                                    b.Rec.X <= newElement.Rec.X)
                                {
                                    if (minX >= newElement.Rec.Y - b.Rec.Y)
                                    {
                                        minX = newElement.Rec.Y - b.Rec.Y;

                                        Gup = b.FonRec.Bottom;
                                    }
                                }
                                if (b.Rec.Y >= newElement.Rec.Y && b.Rec.Right >= newElement.Rec.X &&
                                    b.Rec.X <= newElement.Rec.X)
                                {
                                    if (maxX >= b.Rec.Bottom - newElement.Rec.Y)
                                    {
                                        maxX = b.Rec.Bottom - newElement.Rec.Y;
                                        Gdown = b.FonRec.Top;
                                    }
                                }
                                rec.Height = minX + maxX - b.Img.Height * 2 - 1;
                                rec.Width = b.Img.Height;
                                rec.X = newElement.Rec.X;
                                rec.Y = newElement.Rec.Y - minX + b.Img.Height + 1;
                            }
                            break;
                        }
                    case Shcaf.IdElement.Yschik:
                        {
                            if (b.Id == IdElement.Stena)
                            {

                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right <= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (minX >= newElement.Rec.X - b.Rec.X)
                                    {
                                        minX = newElement.Rec.X - b.Rec.X;
                                        Gup = b.FonRec.Right;
                                    }
                                }
                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right >= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (maxX >= b.Rec.Right - newElement.Rec.X)
                                    {
                                        maxX = b.Rec.Right - newElement.Rec.X;
                                        Gdown = b.FonRec.Left;
                                    }
                                }
                                rec.Width = minX + maxX - b.Img.Width * 2;
                                rec.X = newElement.Rec.X - minX + b.Img.Width;
                                rec.Y = newElement.Rec.Y;
                                rec.Height = newElement.Img.Height;
                            }
                            break;
                        }

                    case Shcaf.IdElement.Vesh:
                        {
                            if (b.Id == IdElement.Stena)
                            {

                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right <= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (minX >= newElement.Rec.X - b.Rec.X)
                                    {
                                        minX = newElement.Rec.X - b.Rec.X;
                                        Gup = b.FonRec.Right;
                                    }
                                }
                                if (b.Rec.Bottom >= newElement.Rec.Y && b.Rec.Right >= newElement.Rec.X &&
                                    b.Rec.Y <= newElement.Rec.Y)
                                {
                                    if (maxX >= b.Rec.Right - newElement.Rec.X)
                                    {
                                        maxX = b.Rec.Right - newElement.Rec.X;
                                        Gdown = b.FonRec.Left;
                                    }
                                }
                                rec.Width = minX + maxX - b.Img.Width * 2;
                                rec.X = newElement.Rec.X - minX + b.Img.Width;
                                rec.Y = newElement.Rec.Y;
                                rec.Height = newElement.Img.Height;
                            }
                            break;
                        }
                }

            }
            return rec;


        }

        private int Prioritet(Shcaf.BaseShcaf _baseShcaf, Element el)
        {
            int elW = 0;
            int elH = 0;
            int bW = 1;
            bool up = false;

            _baseShcaf.listE = _baseShcaf.listE.OrderByDescending(x => x.level).ToList();//по убыванию

            bW = _baseShcaf.listE.First().level + 1;

            switch (el.Id)
            {
                case IdElement.Stena:
                    {
                        if (el.Rec.X < _x + w / 2)
                            elW = el.FonRec.Right;
                        else
                        {
                            elW = el.FonRec.Left;
                        }
                        foreach (var b in _baseShcaf.listE)
                        {
                            switch (b.Id)
                            {
                                case IdElement.Stena:
                                    {
                                        if (b.Rec.Left < elW && b.FonRec.Right > elW && b.Rec.Left < _x + w / 2)
                                        {
                                            bW = b.level;
                                            b.level = b.level + 1;
                                            up = true;
                                            continue;
                                        }
                                        if (b.Rec.Right > elW && b.FonRec.Left < elW && b.Rec.Right > _x + w / 2)
                                        {
                                            bW = b.level;
                                            b.level = b.level + 1;
                                            up = true;
                                            continue;
                                        }
                                        break;
                                    }
                            }
                            if (up && b.level != 0)
                            {
                                b.level = b.level + 1;

                            }


                        }
                        break;
                    }
                case IdElement.Polka:
                    {
                        if (el.Rec.Y < _y + h / 2)
                            elH = el.FonRec.Bottom;
                        else
                        {
                            elH = el.FonRec.Top;
                        }
                        foreach (var b in _baseShcaf.listE)
                        {
                            switch (b.Id)
                            {
                                case IdElement.Polka:
                                    {
                                        if (b.Rec.Top < elH && b.FonRec.Bottom > elH && b.Rec.Top < _y + h / 2)
                                        {
                                            bW = b.level;
                                            b.level = b.level + 1;
                                            up = true;
                                            continue;
                                        }
                                        if (b.Rec.Bottom > elH && b.FonRec.Top < elH && b.Rec.Top > _y + h / 2)
                                        {
                                            bW = b.level;
                                            b.level = b.level + 1;
                                            up = true;
                                            continue;
                                        }
                                        break;
                                    }
                            }
                            if (up && b.level != 0)
                            {
                                b.level = b.level + 1;
                            }

                        }
                        break;
                    }
            }
            return bW;
        }

        private Rectangle GetFonXY(Element el)//получить размер углубления
        {

            Rectangle rec = new Rectangle();
            int i = 0;
            switch (el.Id)
            {
                case IdElement.Stena:
                    {

                        if (el.Rec.Left < _x + w / 2)
                        {
                            i = Glybina(w / 2, (_x + w / 2) - el.Rec.Left, 17);

                            rec = new Rectangle(el.Rec.Left, el.Rec.Top, 6 + i, el.Rec.Height);

                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(rec.Right, Gup),
                                        new Point(rec.Right, Gdown)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Right, el.Rec.Top),
                                        new Point(rec.Right, Gup)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Right, el.Rec.Bottom),
                                        new Point(rec.Right, Gdown)));
                        }

                        if (el.Rec.Left > _x + w / 2)
                        {
                            i = Glybina(w / 2, el.Rec.Left - (_x + w / 2), 17);
                            rec = new Rectangle(el.Rec.Left - (i), el.Rec.Top, 6 + i, el.Rec.Height);

                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(rec.Left, Gup),
                                        new Point(rec.Left, Gdown)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Top),
                                        new Point(rec.Left, Gup)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Bottom),
                                        new Point(rec.Left, Gdown)));
                        }
                        break;
                    }
                case IdElement.Polka:
                    {
                        if (el.Rec.Top < _y + h / 2)
                        {
                            i = Glybina(h / 2, (_y + h / 2) - el.Rec.Top, 13);

                            rec = new Rectangle(el.Rec.Left + 1, el.Rec.Top, el.Rec.Width - 1, 7 + i);

                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(Gup, rec.Bottom),
                                        new Point(Gdown, rec.Bottom)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Bottom),
                                        new Point(Gup, rec.Bottom)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Right, el.Rec.Bottom),
                                        new Point(Gdown, rec.Bottom)));
                        }

                        if (el.Rec.Top > _y + h / 2)
                        {
                            i = Glybina(h / 2, el.Rec.Top - (_y + h / 2), 13);
                            rec = new Rectangle(el.Rec.Left + 1, el.Rec.Top - i, el.Rec.Width - 1, 7 + i);

                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(Gup, rec.Top),
                                        new Point(Gdown, rec.Top)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Top),
                                        new Point(Gup, rec.Top)));
                            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Right, el.Rec.Top),
                                        new Point(Gdown, rec.Top)));
                        }
                        break;
                    }
            }

            return rec;
        }

        /// <summary>
        /// считает глубину
        /// </summary>
        /// <param name="w">длина промежутка на котором надо делить</param>
        /// <param name="d"> длина</param>
        /// <param name="c"> количество делений</param>
        /// <returns></returns>
        private int Glybina(int w, int d, int c)
        {
            int i = 0;

            int count = w / c;

            for (int j = 0; j < d; j = j + count)
            {
                i++;
            }

            return i;
        }

        private void Okontovka(Element el)
        {
            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Top),
                                     new Point(el.Rec.Right, el.Rec.Top)));
            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Top),
                         new Point(el.Rec.Left, el.Rec.Bottom)));
            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Left, el.Rec.Bottom),
                         new Point(el.Rec.Right, el.Rec.Bottom)));
            el.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(el.Rec.Right, el.Rec.Bottom),
                         new Point(el.Rec.Right, el.Rec.Top)));
        }

        //создаем рисунок для шкафа
        public BaseShcaf DrawMainShcaf(BaseShcaf _baseShcaf)
        {
            SizeShcah(_baseShcaf);
            //рисуем заднию стенку
            _element = new Element();
            _element.Img = Image.FromFile("BaseShkaf/NewZStena.bmp");
            _element.Rec = new Rectangle(_x, _y, w, h);
            _element.Id = IdElement.Zadn;
            _element.level = 0;
            _baseShcaf.listE.Add(_element);


            //рисуем левую стенку
            _element = new Element();
            _element.Img = Image.FromFile("BaseShkaf/LNewStena.bmp");
            _element.Rec = new Rectangle(_x, _y, _element.Img.Width, h);
            _element.FonImg = Image.FromFile("BaseShkaf/NewStena.bmp");
            _element.FonRec = new Rectangle(_x, _y, _element.FonImg.Width, h);
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x, _y), new Point(_x, _y + h - 1)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + _element.Img.Width - 1, _y + _element.Img.Width), new Point(_x + _element.Img.Width - 1, _y + h - _element.Img.Width)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + _element.FonImg.Width - 1, _y + 20), new Point(_x + _element.FonImg.Width - 1, _y + h - 20)));
            _element.Id = IdElement.Stena;
            _element.level = 0;
            _baseShcaf.listE.Add(_element);

            //рисуем правую стенку
            _element = new Shcaf.Element();
            _element.Img = Image.FromFile("BaseShkaf/LNewStena.bmp");
            _element.Rec = new Rectangle(_x + w - _element.Img.Width, _y, _element.Img.Width, h);
            _element.FonImg = Image.FromFile("BaseShkaf/NewStena.bmp");
            _element.FonRec = new Rectangle(_x + w - _element.FonImg.Width, _y, _element.FonImg.Width, h);
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - _element.Img.Width, _y + _element.Img.Width), new Point(_x + w - _element.Img.Width, _y + h - _element.Img.Width)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - 1, _y + _element.Img.Width), new Point(_x + w - 1, _y + h - 1)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - _element.FonImg.Width - 1, _y + 20), new Point(_x + w - _element.FonImg.Width - 1, _y + h - 20)));//лини по краям
            _element.Id = IdElement.Stena;
            _element.level = 0;
            _baseShcaf.listE.Add(_element);

            //рисуем верхнию полку
            _element = new Shcaf.Element();
            _element.Img = Image.FromFile("BaseShkaf/RNewPolka.bmp");
            _element.Rec = new Rectangle(_x, _y, w, _element.Img.Height);
            _element.FonImg = Image.FromFile("BaseShkaf/NewPolka.bmp");
            _element.FonRec = new Rectangle(_x + _element.Img.Height, _y, w - _element.Img.Height * 2, _element.FonImg.Height);
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x, _y), new Point(_x + w - 1, _y)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + _element.Img.Height, _y + _element.Img.Height), new Point(_x + w - _element.Img.Height, _y + _element.Img.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x, _y), new Point(_x, _y + _element.Img.Height)));
           _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - 1, _y), new Point(_x + w - 1, _y + _element.Img.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + 24, _y + _element.FonImg.Height), new Point(_x + w - 24, _y + _element.FonImg.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + 24, _y + _element.FonImg.Height), new Point(_x + _element.Img.Height, _y + _element.Img.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - 24, _y + _element.FonImg.Height), new Point(_x + w - _element.Img.Height, _y + _element.Img.Height)));
            _element.Id = IdElement.Polka;
            _element.level = 0;
            _baseShcaf.listE.Add(_element);

            //рисуем нижнию полку
            _element = new Shcaf.Element();
            _element.Img = Image.FromFile("BaseShkaf/RNewPolka.bmp");
            _element.Rec = new Rectangle(_x, _y + h - _element.Img.Height, w, _element.Img.Height);
            _element.FonImg = Image.FromFile("BaseShkaf/NewPolka.bmp");
            _element.FonRec = new Rectangle(_x + _element.Img.Height, _y + h - _element.FonImg.Height, w - _element.Img.Height * 2, _element.FonImg.Height);
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + _element.Img.Height - 1, _y + h - _element.Img.Height), new Point(_x + w - _element.Img.Height, _y + h - _element.Img.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x, _y + h), new Point(_x + w - 1, _y + h)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x, _y + h - _element.Img.Height), new Point(_x, _y + h)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - 1, _y + h - _element.Img.Height), new Point(_x + w - 1, _y + h)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + 24, _y + h - _element.FonImg.Height), new Point(_x + w - 24, _y + h - _element.FonImg.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + 24, _y + h - _element.FonImg.Height), new Point(_x + _element.Img.Height, _y + h - _element.Img.Height)));
            _element.ListPen.Add(new Shcaf.MyPenn(myPen, new Point(_x + w - 24, _y + h - _element.FonImg.Height), new Point(_x + w - _element.Img.Height, _y + h - _element.Img.Height)));
            _element.Id = IdElement.Polka;
            _element.level = 0;
            _baseShcaf.listE.Add(_element);


            return _baseShcaf;
        }

        private void Counting(BaseShcaf _baseShcaf)
        {
            _baseShcaf.CountList.Clear();
            MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, _x, _y, 0);
            foreach (var b in _baseShcaf.listE)
            {
                switch (b.Id)
                {
                    case IdElement.Zadn:
                        countZadnya = _baseShcaf.Width / 1000 * _baseShcaf.Height / 1000;
                        break;
                    case IdElement.Stena:
                        countStenka += Convert.ToInt32(_baseShcaf.Depth / 1000 * _baseShcaf.Height / 1000);
                        break;
                    case IdElement.Polka:
                        countStenka += Convert.ToInt32(_baseShcaf.Width / 1000 * _baseShcaf.Depth / 1000);
                        break;
                    case IdElement.Yschik:
                        if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                        {
                            countYashik += Convert.ToInt32(_baseShcaf.Depth / 1000 * Convert.ToInt32(_baseShcaf.ListText[0].SizeText) / 1000);
                        }
                        else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                        {
                            countYashik += Convert.ToInt32(_baseShcaf.Depth / 1000 * Convert.ToInt32(_baseShcaf.ListText[1].SizeText) / 1000);
                        }
                        break;
                    case IdElement.Vesh:
                        if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                        {
                            countVeshalka += Convert.ToInt32(_baseShcaf.ListText[0].SizeText) / 1000;

                        }
                        else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                        {
                            countVeshalka += Convert.ToInt32(_baseShcaf.ListText[1].SizeText) / 1000;
                        }
                        break;
                }
            }
            //_baseShcaf.CountList.Add(new DrawText(new Point(100, 100), Convert.ToString(countST)));
            //_baseShcaf.CountList.Add(new DrawText(new Point(100, 100), Convert.ToString(countP)));
            // _baseShcaf.CountList.Add(new DrawText(new Point(100, 100), Convert.ToString(countYA)));
            // _baseShcaf.CountList.Add(new DrawText(new Point(100, 100), Convert.ToString(countVE)));

        }

        private void Stoimost(BaseShcaf _baseShcaf)
        {
            _baseShcaf.CountList.Clear();
            double stoim = 0.0;
            MouseEventArgs e = new MouseEventArgs(MouseButtons.Left, 1, _x, _y, 0);
            foreach (var b in _baseShcaf.listE)
            {
                switch (b.Id)
                { 
                    case IdElement.Zadn: 
                        if (pointforStoimost == 0)// всегда из двп
                            stoim += (_baseShcaf.Width / 1000 * _baseShcaf.Height / 1000 * 50);
                        break;

                    case IdElement.Stena: 
                        if (pointforStoimost == 0)//mdf
                        {
                            stoim += (_baseShcaf.Depth / 1000 * _baseShcaf.Height / 1000 * 280);
                        }
                        else if (pointforStoimost == 1)//dsp
                        {
                            stoim += (_baseShcaf.Depth / 1000 * _baseShcaf.Height / 1000 * 150);
                        }
                        else if (pointforStoimost == 2)//dvp
                        {
                            stoim += (_baseShcaf.Depth / 1000 * _baseShcaf.Height / 1000 * 50);
                        }
                        break;

                    case IdElement.Polka:
                        if (pointforStoimost == 0)//mdf
                        {
                            stoim += (_baseShcaf.Width / 1000 * _baseShcaf.Depth / 1000 * 280) * 5;
                        }
                        else if (pointforStoimost == 1)//dsp
                        {
                            stoim += (_baseShcaf.Width / 1000 * _baseShcaf.Depth / 1000 * 150) * 5;
                        }
                        else if (pointforStoimost == 2)//dvp
                        {
                            stoim += (_baseShcaf.Width / 1000 * _baseShcaf.Depth / 1000 * 50) * 5;
                        }
                        break;
                        
                    
                        case IdElement.Yschik:
                            if (pointforStoimost == 0)//mdf
                            {
                                if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[0].SizeText) / 1000 * 280);
                                }
                                else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[1].SizeText) / 1000 * 280);
                                }
                            }
                            else if (pointforStoimost == 1)//dsp
                            {
                                if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[0].SizeText) / 1000 * 150);
                                }
                                else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[1].SizeText) / 1000 * 150);
                                }
                            }
                            else if (pointforStoimost == 2)//dvp
                            {
                                if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[0].SizeText) / 1000 * 50);
                                }
                                else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                                {
                                    stoim += (_baseShcaf.Depth / 1000 * Convert.ToDouble(_baseShcaf.ListText[1].SizeText) / 1000 * 50);
                                }
                            }
                            break;

                        case IdElement.Vesh:
                         
                            if (_x <= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                            {
                                stoim += Convert.ToDouble(_baseShcaf.ListText[0].SizeText) / 1000 * 154;
                                
                            }
                            else if (_x >= _baseShcaf.listE[_baseShcaf.listE.Count - 2].Rec.X && e.Button == MouseButtons.Left)
                            {
                               stoim += Convert.ToDouble(_baseShcaf.ListText[1].SizeText) / 1000 * 154;
                            }
                    
                            break;
                    }
                
            }
                _baseShcaf.CountList.Add(new DrawText(new Point(1159, 551), stoim.ToString("F")));
                
         
        
        }

        //перерисовка цвета шкафа
        private void Redraw(BaseShcaf _baseShcaf)
        {
            foreach (var c in _baseShcaf.listE)
            {
                if (c.Id == IdElement.Polka)
                {
                    if (pointforRedraw == 0)
                    {
                        c.Img = Image.FromFile("Element/RNewPolka.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewPolka.bmp");
                    }
                    else if (pointforRedraw == 1)
                    {
                        c.Img = Image.FromFile("Element/RNewPolkaDub.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewPolkaDub.bmp");
                    }
                    else if (pointforRedraw == 2)
                    {
                        c.Img = Image.FromFile("Element/RNewPolkaVenge.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewPolkaVenge.bmp");
                    }
                }
                else if (c.Id == IdElement.Stena)
                {
                    if (pointforRedraw == 0)
                    {
                        c.Img = Image.FromFile("Element/LNewStena.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewStena.bmp");
                    }
                    else if (pointforRedraw == 1)
                    {
                        c.Img = Image.FromFile("Element/LNewStenaDub.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewStenaDub.bmp");
                        
                    }
                    else if (pointforRedraw == 2)
                    {
                        c.Img = Image.FromFile("Element/LNewStenaVenge.bmp");
                        c.FonImg = Image.FromFile("BaseShkaf/NewStenaVenge.bmp");
                    }
                }
            
            }
        
        }

    }
}
