using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolTray
{
    public class TArrow : IAdroner, IDynamicShape, ISaveTheSize
    {
        #region 属性

        public Point StartPosition { get; set; }

        public Point EndPosition { get; set; }

        public Point LocalPosition { get; set; }

        public Point MousePostion { get; set; }

        public Double Width { get; set; }

        public Double Height { get; set; }

        public Canvas ParentCanvas { get; set; }

        public Path ArrowLine { get; set; }

        public GeometryGroup LineGroup { get; set; }

        public PathFigure ArrowFigure { get; set; }

        public PathGeometry pathGeometry { get; set; }

        public LineGeometry connectorGeometry { get; set; }

        public LineSegment seg1 { get; set; }

        public LineSegment seg2 { get; set; }

        public LineSegment seg3 { get; set; }

        public ArrowAdorner arrowAdroner { get; set; }
        #endregion

        #region 构造函数

        public TArrow(Point point, Canvas parentcanvas)
        {
            StartPosition = point;
            Point p1 = point;
            Point p2 = new Point(p1.X + 20, p1.Y + 20);
            this.pathGeometry = new PathGeometry();
            this.ArrowFigure = new PathFigure();
            this.LineGroup = new GeometryGroup();

            Point p = new Point(p1.X + ((p2.X - p1.X) / 1.00005), p1.Y + ((p2.Y - p1.Y) / 1.00005));
            ArrowFigure.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);

            seg1 = new LineSegment();
            seg1.Point = lpoint;
            ArrowFigure.Segments.Add(seg1);

            seg2 = new LineSegment();
            seg2.Point = rpoint;
            ArrowFigure.Segments.Add(seg2);

            seg3 = new LineSegment();
            seg3.Point = p;
            ArrowFigure.Segments.Add(seg3);
            pathGeometry.Figures.Add(ArrowFigure);

            RotateTransform transform = new RotateTransform();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pathGeometry.Transform = transform;
            LineGroup.Children.Add(pathGeometry);

            this.connectorGeometry = new LineGeometry();
            connectorGeometry.StartPoint = p1;
            connectorGeometry.EndPoint = p2;
            LineGroup.Children.Add(connectorGeometry);
            this.ArrowLine = new Path();
            this.ArrowLine.Data = LineGroup;
            this.ArrowLine.StrokeThickness = 2;
            this.ArrowLine.Stroke = this.ArrowLine.Fill = Brushes.Red;
            this.ArrowLine.Tag = this;

            this.ParentCanvas = parentcanvas;
            this.ParentCanvas.Children.Add(ArrowLine);

        }

        #endregion

        #region 动态变化

        public IDynamicShape GetNewShape(Point point, Canvas canvas)
        {
            return this;
        }



        public void GraphicDistortion(Point point)
        {
            Point p1 = this.StartPosition;
            this.EndPosition = point;

            this.connectorGeometry.EndPoint = point;
            Point p = new Point(p1.X + ((EndPosition.X - p1.X) / 1.00005), p1.Y + ((EndPosition.Y - p1.Y) / 1.00005));
            this.ArrowFigure.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);

            RotateTransform transform = new RotateTransform();
            double theta = Math.Atan2((EndPosition.Y - p1.Y), (EndPosition.X - p1.X)) * 180 / Math.PI;
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            this.pathGeometry.Transform = transform;

            seg1.Point = lpoint;
            seg2.Point = rpoint;
            seg3.Point = p;
        }

        public void GraphicDetermine()
        {
            var layer = AdornerLayer.GetAdornerLayer(this.ParentCanvas);
            arrowAdroner = new ArrowAdorner(this.ArrowLine, this.StartPosition, this.EndPosition);
            //arrowAdroner.ElementEndChanged += this.EndResize;
            //arrowAdroner.ElementStartChanged += this.StartResize;
            arrowAdroner.ElementMove += this.MoveLine;
            layer.Add(arrowAdroner);
            this.AdronerHidden();
        }

        #endregion


        #region 装饰器

        public void AdronerVisble()
        {
            this.arrowAdroner.Visibility = Visibility.Visible;
        }

        public void AdronerHidden()
        {
            this.arrowAdroner.Visibility = Visibility.Hidden;
        }



        public void MoveLine(object sender, EventArgs e)
        {
            if (sender is Point)
            {
                Point point = (Point)sender;


                Point start = new Point(this.StartPosition.X, this.StartPosition.Y);
                Point end = new Point(this.EndPosition.X, this.EndPosition.Y);
                start.Offset(point.X, point.Y);
                end.Offset(point.X, point.Y);
                this.StartPosition = start;
                this.EndPosition = end;
                this.connectorGeometry.StartPoint = this.StartPosition;
                this.connectorGeometry.EndPoint = this.EndPosition;

                Point p = new Point(start.X + ((end.X - start.X) / 1.00005), start.Y + ((end.Y - start.Y) / 1.00005));
                ArrowFigure.StartPoint = p;

                Point lpoint = new Point(p.X + 6, p.Y + 15);
                Point rpoint = new Point(p.X - 6, p.Y + 15);
                seg1.Point = lpoint;
                seg2.Point = rpoint;
                seg3.Point = p;

                RotateTransform transform = new RotateTransform();
                double theta = Math.Atan2((end.Y - start.Y), (end.X - start.X)) * 180 / Math.PI;
                transform.Angle = theta + 90;
                transform.CenterX = p.X;
                transform.CenterY = p.Y;
                pathGeometry.Transform = transform;
            }

        }

        public void SaveTheSize(Canvas canvas, double ratio)
        {
            Point p1 = new Point(this.StartPosition.X / ratio, this.StartPosition.Y / ratio);
            Point p2 = new Point(this.EndPosition.X / ratio, this.EndPosition.Y / ratio);
            var pg = new PathGeometry();
            var af = new PathFigure();
            var lg = new GeometryGroup();

            Point p = new Point(p1.X + ((p2.X - p1.X) / 1.00005), p1.Y + ((p2.Y - p1.Y) / 1.00005));
            af.StartPoint = p;

            Point lpoint = new Point(p.X + 6, p.Y + 15);
            Point rpoint = new Point(p.X - 6, p.Y + 15);

            var seg11 = new LineSegment();
            seg11.Point = lpoint;
            af.Segments.Add(seg11);

            var seg22 = new LineSegment();
            seg22.Point = rpoint;
            af.Segments.Add(seg22);

            var seg33 = new LineSegment();
            seg33.Point = p;
            af.Segments.Add(seg33);
            pg.Figures.Add(ArrowFigure);

            RotateTransform transform = new RotateTransform();
            double theta = Math.Atan2((p2.Y - p1.Y), (p2.X - p1.X)) * 180 / Math.PI;
            transform.Angle = theta + 90;
            transform.CenterX = p.X;
            transform.CenterY = p.Y;
            pg.Transform = transform;
            lg.Children.Add(pathGeometry);

            var cg = new LineGeometry();
            cg.StartPoint = p1;
            cg.EndPoint = p2;
            lg.Children.Add(cg);

            var al = new Path();
            al.Data = lg;
            al.StrokeThickness = 2;
            al.Stroke = al.Fill = Brushes.Red;
            al.Tag = this;
            canvas.Children.Add(al);
        }
        #endregion
    }
}
