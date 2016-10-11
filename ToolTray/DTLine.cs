using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        public void ResizeLine(object sender, EventArgs e)
        {
            if (sender == null) return;
            Point point = (Point)sender;
            line.X1 = point.X;
            line.Y1 = point.Y;
        }
    }
}
