﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace graphiceditor.ToolsDots
{
    public class DrawToolDots
    {
        /// <summary>
        /// 图形源
        /// </summary>
        public Shape Source { get; private set; }

        /// <summary>
        /// 点的大小
        /// </summary>
        public double DotSize { get; private set; }


        /// <summary>
        /// 点列表
        /// </summary>
        public DrawToolDotsCollection DotsList { get; private set; }

        public DrawToolDots()
        {
            this.DotsList = new DrawToolDotsCollection();
        }

        public DrawToolDots(Shape s) :this()
        {
            this.SetSource(s);
        }


        public void SetSource(Shape shape)
        {
            this.Source = shape;
            this.DotSize = 9;
            this.DotsList.Clear();

            if(shape !=null)
            {
                var sett = shape.Style.Setters.OfType<Setter>().Where(ss => ss.Property == Polyline.StrokeThicknessProperty);
                if (sett != null && sett.Count() > 0)
                    this.DotSize = 9 + (double)sett.First().Value;
                if (shape is Line)
                    this.SetLineSource(shape as Line);
            }
        }

        private void SetLineSource(Line l)
        {
            Point p1 = new Point(l.X1, l.Y1);
            Point p2 = new Point(l.X2, l.Y2);
            List<Point> points = new List<Point>() { p1, p2 };
            this.DotsList.AddPoints(this, points);
        }
    }
}
