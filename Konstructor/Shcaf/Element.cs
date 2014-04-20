using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Konstructor.Shcaf
{
    class Element
    {
        public int level = 0;//приоритет отрисовки едемента

        public IdElement Id;

        public Rectangle Rec;

        public Image Img;

        public Image FonImg;//фон полки типо 3д

        public Rectangle FonRec;//фон полки типо 3д

        public List<MyPenn> ListPen;

        public Element(Image img, Rectangle rec)
        {
            this.Rec = rec;
            this.Img = img;
            ListPen = new List<MyPenn>();
        }

        public Element()
        {
            ListPen = new List<MyPenn>();
        }

    }
}
