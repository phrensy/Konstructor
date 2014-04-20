using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Konstructor.Shcaf
{
    class MyPenn
    {
        public Pen MyPen;

        public Point Start;

        public Point End;

        public MyPenn(Pen myPen, Point start, Point end)
        {
            MyPen = myPen;
            Start = start;
            End = end;
        }

        
    }
}
