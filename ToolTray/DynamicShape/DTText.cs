using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Documents;

namespace ToolTray
{
    public class TText : IAdroner, IDynamicShape
    {
        #region 属性
        public Point StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point MousePosition { get; set; }

        public Path TextRegion { get; set; }

        public TextBox textBox { get; set; }

        public TextBlock textBlock { get; set; }

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Grid Container { get; set; }

        public Canvas ParentCanvas { get; set; }

        public TextAdroner TextAdroner { get; set; }
        #endregion

        #region 构造函数

        public TText(Point point, Canvas parentcanvas)
        {
            this.StartPosition = point;
            this.MousePosition = point;

            this.textBlock = new TextBlock()
            {
                Background = Brushes.Transparent,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 30,
                Visibility = Visibility.Hidden
            };
            this.textBox = new TextBox()
            {
                Background = Brushes.Transparent,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 30,
                Visibility = Visibility.Hidden
            };

            PolyLineSegment polyLineSegment = new PolyLineSegment();
            polyLineSegment.Points = new PointCollection(new Point[] { point, point, point, point });
            PathFigure pathFigure = new PathFigure();
            pathFigure.IsClosed = true;
            pathFigure.StartPoint = point;
            pathFigure.Segments.Add(polyLineSegment);

            PathGeometry pathGemetry = new PathGeometry();
            pathGemetry.Figures.Add(pathFigure);

            TextRegion = new Path();
            TextRegion.StrokeDashArray = new DoubleCollection() { 2, 3 };//虚线框
            TextRegion.Stroke = Brushes.Black;
            TextRegion.StrokeThickness = 1;
            TextRegion.Data = pathGemetry;

            this.ParentCanvas = parentcanvas;
            this.ParentCanvas.Children.Add(this.TextRegion);
        }

        #endregion

        #region 动态变化

        public IDynamicShape GetNewShape(Point point, Canvas canvas)
        {
            return this;
        }

        public void GraphicDistortion(Point point)
        {
            PolyLineSegment line = this.TextRegion.GetSegment();
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
            this.ParentCanvas.Children.Remove(this.TextRegion);
            this.ChangeText();
            this.Container = new Grid();
            this.Container.Width = this.Width;
            this.Container.Height = this.Height;

            if (this.Width > 10 && this.Height > 10)
            {//Ensure the size 
                this.Container.Children.Add(textBlock);
                this.Container.Children.Add(textBox);
                textBox.Visibility = Visibility.Visible;
                this.ParentCanvas.Children.Add(this.Container);
                Canvas.SetTop(this.Container, this.StartPosition.Y);
                Canvas.SetLeft(this.Container, this.StartPosition.X);
                var layer = AdornerLayer.GetAdornerLayer(this.ParentCanvas);
                TextAdroner = new TextAdroner(this.Container);
                TextAdroner.EditStatus += WriteableStatus;
                layer.Add(TextAdroner);
            }
        }

        private void ChangeText()
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
        }

        public void ReadOnlyStatus()
        {
            string str = this.textBox.Text;
            this.textBlock.Text = str;
            this.textBox.Visibility = Visibility.Hidden;
            this.textBlock.Visibility = Visibility.Visible;
        }

        public void WriteableStatus(object sender, EventArgs e)
        {
            string str = this.textBlock.Text;
            this.textBox.Text = str;
            this.textBox.Visibility = Visibility.Visible;
            this.textBlock.Visibility = Visibility.Hidden;
        }
        #endregion

        #region 装饰器

        public void AdronerVisble()
        {
            this.TextAdroner.Visibility = Visibility.Visible;
        }

        public void AdronerHidden()
        {
            this.TextAdroner.Visibility = Visibility.Hidden;
        }

        #endregion
    }
}
