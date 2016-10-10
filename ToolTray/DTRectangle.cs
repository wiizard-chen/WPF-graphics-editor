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

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Canvas Parentcanvas;

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
            rectangle.StrokeThickness = 8;
            rectangle.Tag = this;
            rectangle.Data = pathGemetry;
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



        public Canvas NewCanvas()
        {
            this.GetProperty();
            this.Parentcanvas = new Canvas();
            this.Parentcanvas.Width = this.Width+5;
            this.Parentcanvas.Height = this.Height+5;
            this.Parentcanvas.Children.Add(rectangle);

            return Parentcanvas;
        }

        private void GetProperty()
        {
            PolyLineSegment line = this.rectangle.GetSegment();
            this.Width = Math.Abs(line.Points[0].X - line.Points[2].X);
            this.Height = Math.Abs(line.Points[0].Y - line.Points[2].Y);
            if (line.Points[0].X < line.Points[2].X && line.Points[0].Y < line.Points[2].Y)
            {
                this.StartPosition = line.Points[2];
            }
            else
            {
                this.StartPosition = line.Points[0];
            }
        }
    }
}
