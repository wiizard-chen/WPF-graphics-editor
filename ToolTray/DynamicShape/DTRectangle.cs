using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolTray
{
    public class TRectangle : IAdroner, IDynamicShape
    {
        #region 属性

        public Point StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point MousePosition { get; set; }

        public Path rectangle { get; set; }

        public Rectangle trect { get; set; }

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Grid Container { get; set; }

        public Canvas ParentCanvas { get; set; }

        public RectangleAdorner RectAdroner { get; set; }

        #endregion

        #region 构造函数

        public TRectangle(Point point, Canvas parentcanvas)
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

            this.ParentCanvas = parentcanvas;
            this.ParentCanvas.Children.Add(this.rectangle);

        }
        #endregion

        #region 动态变化

        public IDynamicShape GetNewShape(Point point, Canvas canvas)
        {
            return this;
        }

        public void GraphicDistortion(Point point)
        {
            PolyLineSegment line = this.rectangle.GetSegment();
            double w = point.X - this.MousePosition.X;
            double h = point.Y - this.MousePosition.Y;
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

        public void GraphicDetermine()
        {
            this.ParentCanvas.Children.Remove(this.rectangle);
            this.ChangeRectangle();
            this.Container = new Grid();
            this.Container.Width = this.Width;
            this.Container.Height = this.Height;
            if (this.Width > 10 && this.Height > 10)
            {
                this.Container.Children.Add(trect);
                this.Container.Tag = this;
                this.ParentCanvas.Children.Add(this.Container);
                Canvas.SetTop(this.Container, this.StartPosition.Y);
                Canvas.SetLeft(this.Container, this.StartPosition.X);
                var layer = AdornerLayer.GetAdornerLayer(this.ParentCanvas);
                RectAdroner = new RectangleAdorner(this.Container);
                layer.Add(RectAdroner);
                this.AdronerHidden();
            }
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
        }

        #endregion

        #region 装饰器

        public void AdronerVisble()
        {
            this.RectAdroner.Visibility = Visibility.Visible;
        }

        public void AdronerHidden()
        {
            this.RectAdroner.Visibility = Visibility.Hidden;
        }

        #endregion

    }
}
