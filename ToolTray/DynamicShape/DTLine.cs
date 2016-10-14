using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;


namespace ToolTray
{
    public class TLine : IAdroner, IDynamicShape
    {
        #region 属性

        public Point StartPosition { get; set; }

        public Point LocalPosition { get; set; }

        private Point MousePosition { get; set; }

        public Line line { get; set; }

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

        public Canvas ParentCanvas { get; set; }

        public LineAdorner lineAdroner { get; set; }

        #endregion

        #region 构造函数

        public TLine(Point point, Canvas parentcanvas)
        {
            this.StartPosition = point;
            this.MousePosition = point;

            line = new Line();
            line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
            line.StrokeThickness = 8;

            line.X1 = point.X;
            line.X2 = point.X;
            line.Y1 = point.Y;
            line.Y1 = point.Y;
            line.Tag = this;

            this.ParentCanvas = parentcanvas;
            this.ParentCanvas.Children.Add(this.line);
        }

        #endregion

        #region 动态变化

        public IDynamicShape GetNewShape(Point point, Canvas canvas)
        {
            return this;
        }

        public void GraphicDistortion(Point point)
        {
            line.X2 = point.X;
            line.Y2 = point.Y;
        }

        public void GraphicDetermine()
        {
            this.Width = Math.Abs(this.StartPoint.X - this.EndPoint.X);
            this.Height = Math.Abs(this.StartPoint.Y - this.EndPoint.Y);
            var layer = AdornerLayer.GetAdornerLayer(this.ParentCanvas);
            lineAdroner = new LineAdorner(this.line, this.StartPoint, this.EndPoint);
            lineAdroner.ElementEndChanged += this.EndResize;
            lineAdroner.ElementStartChanged += this.StartResize;
            lineAdroner.ElementMove += this.MoveLine;
            layer.Add(lineAdroner);
            this.AdronerHidden();
        }
        
        #endregion

        #region 装饰器

        public void AdronerVisble()
        {
            this.lineAdroner.Visibility = Visibility.Visible;
        }

        public void AdronerHidden()
        {
            this.lineAdroner.Visibility = Visibility.Hidden;
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

        public void MoveLine(object sender, EventArgs e)
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
        #endregion

    }
}
