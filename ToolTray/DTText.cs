using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace ToolTray
{
    public class TText
    {
        public Point? StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point? MousePosition { get; set; }

        public TextBox textBox { get; set; }

        public TextBlock textBlock { get; set; }

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Grid Parentcanvas { get; set; }

        public Path TextRegion{ get; set; }

        public TText(Point point)
        {
            this.StartPosition = point;
            this.MousePosition = point;
            this.textBlock = new TextBlock();
            this.textBox = new TextBox();

            PolyLineSegment polyLineSegment = new PolyLineSegment();
            polyLineSegment.Points = new PointCollection(new Point[] { point, point, point, point });
            PathFigure pathFigure = new PathFigure();
            pathFigure.IsClosed = true;
            pathFigure.StartPoint = point;
            pathFigure.Segments.Add(polyLineSegment);
            PathGeometry pathGemetry = new PathGeometry();
            pathGemetry.Figures.Add(pathFigure);

            TextRegion = new Path();
            TextRegion.StrokeDashArray = new DoubleCollection() { 2, 3 };
            TextRegion.Stroke = Brushes.Black;
            TextRegion.StrokeThickness = 1;
            TextRegion.Data = pathGemetry;
        }

        public void ChangeSelect(Point point)
        {
            PolyLineSegment line = this.TextRegion.GetSegment();
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
            this.ChangeText();
            this.Parentcanvas = new Grid();
            this.Parentcanvas.Width = this.Width;
            this.Parentcanvas.Height = this.Height;
            this.Parentcanvas.Children.Add(textBlock);
            return Parentcanvas;
        }

        private  void ChangeText()
        {
            PolyLineSegment line = this.TextRegion.GetSegment();
            this.Width = Math.Abs(line.Points[0].X - line.Points[2].X);
            this.Height = Math.Abs(line.Points[0].Y - line.Points[2].Y);
            if (line.Points[0].X < line.Points[2].X)
            {
                if (line.Points[0].Y < line.Points[2].Y)
                    this.StartPosition = line.Points[0];
                else
                    this.StartPosition = line.Points[3];
            }
            else
            {
                if (line.Points[0].Y < line.Points[2].Y)
                    this.StartPosition = line.Points[1];
                else
                    this.StartPosition = line.Points[2];
            }
            this.textBlock.TextWrapping = TextWrapping.Wrap;
            this.textBlock.FontSize = 30;
            this.textBlock.Text = "what's wrong?!";
        }
    }
}
