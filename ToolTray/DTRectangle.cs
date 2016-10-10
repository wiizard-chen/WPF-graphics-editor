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
        //public Rectangle rectangle;

        public Path rectangle { get; set; }

        public Canvas Parentcanvas;

        public TRectangle(Point point)
        {
            this.StartPosition = point;
            this.MousePosition = point;
            //rectangle = new Rectangle() { Width = 0, Height = 0 };
            //rectangle.Tag = this;

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
            rectangle.StrokeThickness = 8;
            rectangle.Tag = this;
            rectangle.Data = pathGemetry;
        }

        public void ChangeRectangle(Point point)
        {
            //this.rectangle.Width = Math.Abs(this.StartPosition.Value.X - point.X);
            //this.rectangle.Height = Math.Abs(this.StartPosition.Value.Y - point.Y);
            //return true;

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

        public Canvas NewCanvas()
        {
            this.Parentcanvas = new Canvas();
            this.Parentcanvas.Width = rectangle.Width;
            this.Parentcanvas.Height = rectangle.Height;
            this.Parentcanvas.Children.Add(rectangle);
            return Parentcanvas;
        }
    }
}
