using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace graphiceditor.Tools
{
    public class TRectangle : DrawTool
    {
        private Path rectangle;
        private TRectangle T_rectangle;

        public double Width { get; set; }
        public double Height { get; set; }
        public Point StartPosition { get; set; }

        public TRectangle(Window Window, Canvas Workspace, Border CanvasBorder, Canvas Canvas) : base(Window, Workspace, CanvasBorder, Canvas)
        {
            this.ToolType = ToolsType.TRectangle;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.MousePosition = Mouse.GetPosition(this.Canvas);
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition.HasValue)
            {
                Point pos = e.GetPosition(this.Canvas);
                if (this.rectangle == null)
                {
                    PolyLineSegment polyLineSegment = new PolyLineSegment();
                    polyLineSegment.Points = new PointCollection(new Point[] { pos, pos, pos, pos });

                    PathFigure pathFigure = new PathFigure();
                    pathFigure.IsClosed = true;
                    pathFigure.StartPoint = pos;
                    pathFigure.Segments.Add(polyLineSegment);

                    PathGeometry pathGemetry = new PathGeometry();
                    pathGemetry.Figures.Add(pathFigure);

                    rectangle = new Path();
                    rectangle.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                    rectangle.StrokeThickness = 8;
                    rectangle.Tag = ToolsType.TRectangle;
                    rectangle.Data = pathGemetry;
                    this.Canvas.Children.Add(rectangle);
                }
                else
                {
                    PolyLineSegment line = this.rectangle.GetSegment();
                    double w = pos.X - this.MousePosition.Value.X;
                    double h = pos.Y - this.MousePosition.Value.Y;
                    Point p1 = line.Points[1];
                    Point p2 = line.Points[2];
                    Point p3 = line.Points[3];

                    p1.Offset(w, 0);
                    p2.Offset(w, h);
                    p3.Offset(0, h);

                    line.Points[1] = p1;
                    line.Points[2] = p2;
                    line.Points[3] = p3;
                    this.MousePosition = pos;
                }
            }
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (rectangle != null)
                {
                    T_rectangle = new TRectangle(Window, WorkSpace, CanvasBorder, Canvas);
                    T_rectangle.rectangle = this.rectangle;
                    T_rectangle.Element = this.rectangle;
                    this.Tools.Add(T_rectangle);
                    //this.Canvas.Children.Remove(this.rectangle);

                    //this.loadrect = new Canvas();
                    //this.loadrect.Width = this.rectangle.Width;
                    //this.loadrect.Height = this.rectangle.Height;
                    //this.Canvas.Children.Add(this.loadrect);
                    //this.loadrect.Children.Add(this.rectangle);
                    PolyLineSegment line = this.rectangle.GetSegment();
                    this.Width =Math.Abs( line.Points[0].X - line.Points[2].X);
                    this.Height =Math.Abs( line.Points[0].Y - line.Points[2].Y);
                    if (line.Points[0].X > line.Points[2].X || line.Points[0].Y > line.Points[2].Y)
                    {
                        this.StartPosition = line.Points[2];
                    }
                    else
                    {
                        this.StartPosition = line.Points[0];
                    }
                    this.rectangle = null;
                }
            }
        }

        public void MoveRectangle(Point move)
        {
            PolyLineSegment line = rectangle.GetSegment();
            for (int i = 0; i < line.Points.Count; i++)
            {
                Point pt = line.Points[i];
                pt.Offset(move.X, move.Y);
                if (i > 0)
                    line.Points[i] = pt;
                else
                    rectangle.SetTopLeft(pt);
            }
        }
    }
}
