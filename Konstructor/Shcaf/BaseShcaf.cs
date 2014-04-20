using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Konstructor.Shcaf
{
    class BaseShcaf
    {
        //ширина
        public int Width { get; set; }

        //высота
        public int Height { get; set; }

        //глубина
        public double Depth { get; set; }

        public List<Element> listE;

        public List<SizeShcaf> listS;

        public List<DrawText> ListText;

        public List<DrawText> CountList;

        public BaseShcaf(int width, int height,double depth)
        {
            Width = width;
            Height = height;
            Depth = depth;
            listE = new List<Element>();
            listS = new List<SizeShcaf>();
            ListText = new List<DrawText>();
            CountList = new List<DrawText>();
        }

        public BaseShcaf()
        {
            listE = new List<Element>();
            listS = new List<SizeShcaf>();
            ListText = new List<DrawText>();
            CountList = new List<DrawText>();
        }

    }
}
