using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ToolTray
{
    public class TLine
    {
        public Point? StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point? MousePosition { get; set; }

        public Line line;

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Point StartPoint
        {
            get
            {
                if (line != null)
                    return new Point(line.X1, line.Y1);
                return new Point();
            }
        }

        public Point EndPoint
        {
            get
            {
                if (line != null)
                    return new Point(line.X2, line.Y2);
                return new Point();
            }
        }


        public Grid Parentcanvas;

        public TLine(Point point)
        {
            this.StartPosition = point;
            this.MousePosition = point;
            line = new Line();

            line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            line.StrokeThickness = 10;

            line.X1 = point.X;
            line.X2 = point.X;
            line.Y1 = point.Y;
            line.Y1 = point.Y;
            line.Tag = this;
        }

        public void ChangeLine(Point point)
        {
            line.X2 = point.X;
            line.Y2 = point.Y;
        }

        public void StartResize(object sender, EventArgs e)
        {
            if (sender == null) return;
            Point point = (Point)sender;
            line.X1 = line.X1 + point.X;
            line.Y1 = line.Y1 + point.Y;
        }

        public void EndResize(object sender, EventArgs e)
        {
            if (sender == null) return;
            Point point = (Point)sender;
            line.X2 = line.X2 + point.X;
            line.Y2 = line.Y2 + point.Y;
        }

        public void MoveLine(object sender,EventArgs e)
        {
            if (sender == null) return;
            Point point = (Point)sender;
            Point start = new Point(line.X1, line.Y1);
            Point end = new Point(line.X2, line.Y2);
            start.Offset(point.X, point.Y);
            end.Offset(point.X, point.Y);
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;

            //line.X1 = line.X1 + point.X;
            //line.Y1 = line.Y1 + point.Y;
            //line.X2 = line.X2 + point.X;
            //line.Y2 = line.Y2 + point.Y;
        }

        public Grid NewCanvas()
        {
            this.ChangeLine();
            this.Parentcanvas = new Grid();
            this.Parentcanvas.Width = this.Width + 1;
            this.Parentcanvas.Height = this.Height + 1;
            this.Parentcanvas.Children.Add(line);
            return Parentcanvas;
        }
        private void ChangeLine()
        {
            this.Width = Math.Abs(this.StartPoint.X - this.EndPoint.X);
            this.Height = Math.Abs(this.StartPoint.Y - this.EndPoint.Y);
            //Point point;
            if (line.Y1 < line.Y2)
            {
               /// point = StartPoint;
                if (line.X1 > line.X2)
                    this.StartPosition = new Point(line.X1 - Width, line.Y2 - Height);
                else
                    this.StartPosition = StartPoint;
            }
            else
            {
                if (line.X1 < line.X2)
                {
                    ///point = StartPoint;
                    this.StartPosition = new Point(line.X1 - Width, line.Y2 - Height);
                }
                else
                {
                    ///point = EndPoint;
                    this.StartPosition = EndPoint;
                }
            }
            line.X1 = line.X1 - StartPosition.Value.X;
            line.X2 = line.X2 - StartPosition.Value.X;
            line.Y1 = line.Y1 - StartPosition.Value.Y;
            line.Y2 = line.Y2 - StartPosition.Value.Y;
        }
    }
}
