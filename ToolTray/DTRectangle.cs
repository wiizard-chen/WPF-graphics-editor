using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolTray
{
    public class TRectangle
    {
        public Point? StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point? MousePosition;

        public Path rectangle { get; set; }

        public Rectangle trect { get; set; }

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Grid Parentcanvas;

        public TRectangle(Point point)
        {
            this.StartPosition = point;
            this.MousePosition = point;

            PolyLineSegment polyLineSegment = new PolyLineSegment();
            polyLineSegment.Points = new PointCollection(new Point[] { point, point, point, point });

            PathFigure pathFigure = new PathFigure();
            pathFigure.IsClosed = true;
            pathFigure.StartPoint = point;
            pathFigure.Segments.Add(polyLineSegment);

            PathGeometry pathGemetry = new PathGeometry();
            pathGemetry.Figures.Add(pathFigure);

            rectangle = new Path();
            rectangle.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            rectangle.StrokeThickness = 4;
            rectangle.Tag = this;
            rectangle.Data = pathGemetry;
            // this.trect = new Rectangle() { Width = 0, Height = 0 };
        }


        public void ChangeRectangle(Point point)
        {
            PolyLineSegment line = this.rectangle.GetSegment();
            double w = point.X - this.MousePosition.Value.X;
            double h = point.Y - this.MousePosition.Value.Y;
            Point p1 = line.Points[1];
            Point p2 = line.Points[2];
            Point p3 = line.Points[3];
            p1.Offset(w, 0);
            p2.Offset(w, h);
            p3.Offset(0, h);
            line.Points[1] = p1;
            line.Points[2] = p2;
            line.Points[3] = p3;
            this.MousePosition = point;
        }



        public Grid NewCanvas()
        {
            this.ChangeRectangle();

            this.Parentcanvas = new Grid();
            this.Parentcanvas.Width = this.Width;
            this.Parentcanvas.Height = this.Height;
            this.Parentcanvas.Children.Add(trect);

            return Parentcanvas;
        }

        private void ChangeRectangle()
        {
            PolyLineSegment line = this.rectangle.GetSegment();
            this.Width = Math.Abs(line.Points[0].X - line.Points[2].X);
            this.Height = Math.Abs(line.Points[0].Y - line.Points[2].Y);
            if (line.Points[0].X < line.Points[2].X)
            {
                if (line.Points[0].Y < line.Points[2].Y)
                    this.StartPosition = line.Points[0];//ok
                else
                    this.StartPosition = line.Points[3];
            }
            else
            {
                if (line.Points[0].Y < line.Points[2].Y)
                    this.StartPosition = line.Points[1];//ok
                else
                    this.StartPosition = line.Points[2];
            }
            trect = new Rectangle();
            trect.Stroke = Brushes.LightSteelBlue;
            trect.StrokeThickness = 4;
            trect.Tag = this;
            trect.Stretch = Stretch.Fill;

            //List<Point> list = new List<Point>();
            //for (int i = 0; i < line.Points.Count; i++)
            //{
            //    Point p = line.Points[i];
            //    p.Offset(-this.StartPosition.Value.X, -this.StartPosition.Value.Y);
            //    list.Add(p);
            //}

            //PolyLineSegment polyLineSegment = new PolyLineSegment();
            //polyLineSegment.Points = new PointCollection(list);

            //PathFigure pathFigure = new PathFigure();
            //pathFigure.IsClosed = true;
            //pathFigure.StartPoint = list.First();
            //pathFigure.Segments.Add(polyLineSegment);

            //PathGeometry pathGemetry = new PathGeometry();
            //pathGemetry.Figures.Add(pathFigure);

            //rectangle = new Path();
            //rectangle.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            //rectangle.StrokeThickness = 4;
            //rectangle.Tag = this;
            //rectangle.Stretch = Stretch.Fill;
            //rectangle.Data = pathGemetry;
        }
    }
}
