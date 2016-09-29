using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;


namespace shapeeditor
{
    public enum DrawToolType : byte
    {
        None = 0,
        Pointer,
        Rectangle,
        Polyline,
        Delete
    }

    public partial class DrawTool
    {
        private Window window;//主窗口
        private Canvas workspace;//工作地方
        private Border canvasBorder;//画布边界
        private Canvas canvas;//画布

        private ItemsControl dotsControl; //转变点


        private Point? mousePos = null; // rkoordinata鼠标点击

        private Brush currentBrush;//刷子
        private double currentLineWidth;//线粗

        public List<Shape> Selection { get; private set; }
        public DrawToolType ToolType { get; private set; }

        private Shape lastShape = null;
        private Path lasstSHapeAsRect
        {
            get
            {
                return this.lastShape as Path;
            }
        }
        private Polyline lastShapeAsLine
        {
            get
            {
                return this.lastShape as Polyline;
            }
        }

        public DrawTool(Window Window, Canvas Workspace, Border CanvasBorder, Canvas Canvas, DrawToolType type)
        {
            this.window = Window;
            this.workspace = Workspace;
            this.canvasBorder = CanvasBorder;
            this.canvas = Canvas;
            this.Selection = new List<Shape>();


            this.currentBrush = new SolidColorBrush(Colors.Black);
        }

        public void SetToolType(DrawToolType type)
        {
            
            switch (this.ToolType)
            {//去除以前的绑定事件
                case DrawToolType.None:
                    break;
                case DrawToolType.Pointer:
                    break;
                case DrawToolType.Rectangle:
                    break;
                case DrawToolType.Polyline:
                    break;
                case DrawToolType.Delete:
                    break;
                default:
                    break;
            }
            this.ToolType = type;
            switch (type)
            {//添加新的绑定事件
                case DrawToolType.None:
                    break;
                case DrawToolType.Pointer:
                    break;
                case DrawToolType.Rectangle:
                    break;
                case DrawToolType.Polyline:
                    Mouse.AddMouseDownHandler(this.canvas, this.polylineToolMouseDown);
                    Mouse.AddMouseUpHandler(this.window, this.polylineToolMouseUp);
                    Mouse.AddMouseMoveHandler(this.window, this.polylineToolMouseMove);
                    this.canvas.Cursor = Cursors.Cross;
                    break;
                case DrawToolType.Delete:
                    break;
                default:
                    break;
            }
        }

        #region 指示器
        private void toolMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.mousePos = Mouse.GetPosition(this.window);
                if(e.ClickCount >1 && this.Selection.Count ==1)
                {//折线添加的方法
                    Polyline line = this.Selection.First() as Polyline;
                    if(line != null)
                    {
                        Point topleft = e.GetPosition(this.canvas);
                        topleft.Offset(-2, -2);
                        Point bottomright = new Point(topleft.X + 5, topleft.Y + 5);
                        for (int i = line.Points.Count-1; i >=1; i--)
                        {
                            //if(ShapesHelper.)
                        }
                    }
                }
                else
                {
                    var s = this.GetCanvasHoveredElement();
                    if(s!=null)
                    {
                        if(s!=this.dotsControl)
                        {
                           // if(!this.Selection.Contains(s))
                               // this.selectshap
                        }
                    }

                }
            }
        }

        private UIElement GetCanvasHoveredElement()
        {
            var elems = this.canvas.Children.OfType<UIElement>().Where(e => e.Visibility == Visibility.Visible && e.IsMouseOver);
            return elems.DefaultIfEmpty(null).First();
        }

        private void selectShape(Shape s)
        {
            this.Selection.ClearShapes();
            if(s!=null)
            {
                this.Selection.AddShape(s);
                if(s is Path)
                {
                    Style style = s.Style;
                    var sett = style.Setters.OfType<Setter>().Where(ss => ss.Property == Path.FillProperty);
                    if (sett != null && sett.Count() > 0)
                        this.currentBrush = (sett.First().Value as Brush);
                }
                else if(s is Polyline)
                {

                }
            }
        }



        #endregion

        #region 直线
        /// <summary>
        ///它计算的线型，这取决于所提供的刷子和线宽
        /// </summary>
        public static Style CalculatePolylineStyle(Brush brush, double stroke)
        {
            Brush b = (VisualBrush)XamlReader.Parse(DrawTool.HatchBrushXaml);
            Style style = new Style();

            style.TargetType = typeof(Polyline);
            style.Setters.Add(new Setter(Polyline.StrokeProperty, brush));
            style.Setters.Add(new Setter(Polyline.StrokeThicknessProperty, stroke));
            style.Setters.Add(new Setter(Polyline.StrokeLineJoinProperty, PenLineJoin.Round));

            MultiTrigger mt = new MultiTrigger();
            mt.Conditions.Add(new Condition(Polyline.IsMouseOverProperty, true));
            mt.Conditions.Add(new Condition(Polyline.TagProperty, ShapeTag.None));
            mt.Setters.Add(new Setter(Polyline.StrokeProperty, Brushes.Red));
            style.Triggers.Add(mt);

            Trigger t = new Trigger() { Property = Polyline.TagProperty, Value = ShapeTag.Select };
            t.Setters.Add(new Setter(Polyline.StrokeProperty, Brushes.Red));
            style.Triggers.Add(t);

            t = new Trigger() { Property = Polyline.TagProperty, Value = ShapeTag.Select | ShapeTag.Deleting };
            t.Setters.Add(new Setter(Polyline.StrokeProperty, b));
            style.Triggers.Add(t);

            t = new Trigger() { Property = Polyline.TagProperty, Value = ShapeTag.Deleting };
            t.Setters.Add(new Setter(Polyline.StrokeProperty, b));
            style.Triggers.Add(t);

            return style;
        }
        private void polylineToolMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.mousePos = Mouse.GetPosition(this.canvas);
        }
        private void polylineToolMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.mousePos.HasValue)
            {
                Point pos = e.GetPosition(this.canvas);
                if (this.lastShapeAsLine == null)
                {
                    Polyline line = new Polyline();
                    line.Style = CalculatePolylineStyle(this.currentBrush, 4);
                    line.Points.Add(new Point(this.mousePos.Value.X, this.mousePos.Value.Y));
                    line.Points.Add(line.Points[0]);
                    line.Tag = ShapeTag.None;
                    line.StrokeEndLineCap = PenLineCap.Triangle;
                    this.lastShape = line;
                    this.canvas.Children.Add(line);
                }
                this.lastShapeAsLine.Points[1] = new Point(pos.X, pos.Y);
            }
        }
        private void polylineToolMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.mousePos = null;
                if (this.lastShapeAsLine != null)
                {
                    if ((lastShapeAsLine.ActualWidth * lastShapeAsLine.ActualWidth + lastShapeAsLine.ActualHeight * lastShapeAsLine.ActualHeight) <= 4)
                        this.canvas.Children.Remove(lastShapeAsLine);
                    this.lastShape = null;
                }
            }
        } 


        #endregion




    }
    
}
