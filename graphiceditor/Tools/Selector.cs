using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Reflection;
using graphiceditor.ToolsDots;
using System.Windows.Markup;
using System.Windows.Documents;

namespace graphiceditor.Tools
{
    public class TSelector : DrawTool
    {
        public DrawTool SelectedTool { get; set; }

        private Border border;
        private ItemsControl dotsControl;
        private DrawToolDots dots;
        private DrawToolDot selectedDot;


        public TSelector(Window window, Canvas workspace, Border canvasborder, Canvas canvas) : base(window, workspace, canvasborder, canvas)
        {
            this.ToolType = ToolsType.TSelector;

            ///这个需要修改
            this.border = (Border)XamlReader.Parse(StaticXaml.BorderControlXaml);
            this.dotsControl = (ItemsControl)XamlReader.Parse(StaticXaml.DotsItemsControlXaml);
            this.dots = new DrawToolDots();
            this.dotsControl.ItemsSource = this.dots.DotsList;
            this.border.Visibility = Visibility.Hidden;

            this.WorkSpace.Children.Add(this.border);
            this.Canvas.Children.Add(this.dotsControl);

            // AddDotsEvent();

        }

        public void AddDotsEvent()
        {
            Mouse.AddMouseUpHandler(this.dotsControl, this.Dots_MouseUp);
            Mouse.AddMouseDownHandler(this.dotsControl, this.Dots_MouseDown);
            Mouse.AddMouseDownHandler(this.dotsControl, this.Dots_MouseDown);
        }


        private void Dots_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var ht = VisualTreeHelper.HitTest(this.dotsControl, Mouse.GetPosition(this.dotsControl));
            if (ht != null)
            {
                this.selectedDot = (ht.VisualHit as Rectangle).Tag as DrawToolDot;
                if (this.selectedDot.Parent.Source is Path && this.selectedDot.RectPoint == RectPoints.Center)
                {
                    Mouse.OverrideCursor = Cursors.ScrollWE;
                    return;
                }
                Mouse.OverrideCursor = null;
            }
        }

        private void Dots_MouseMove(object sender, MouseButtonEventArgs e)
        {
            if (this.selectedDot != null && e.LeftButton == MouseButtonState.Released)
                this.Dots_MouseUp(sender, null);
        }

        private void Dots_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((e == null || e.ChangedButton == MouseButton.Left) && this.selectedDot != null)
            {
                var intersecteedDots = this.dots.DotsList
                    .Where(d => Math.Abs(d.ID - this.selectedDot.ID) == 1
                    && DrawToolDot.IsDotsIntersect(this.selectedDot, d));
                if (this.selectedDot.Parent.Source is Line)
                {
                    if (intersecteedDots.Count() > 0)
                    {

                    }
                }
            }
        }



        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.MousePosition = e.GetPosition(this.Canvas);
                this.SelectedTool = this.GetCanvasDrawTool();
                if (this.SelectedTool.ToolType == ToolsType.TRectangle)
                {
                    TEST(SelectedTool as TRectangle);
                }
                if (SelectedTool != null)
                {
                    //Mouse.OverrideCursor = Cursors.SizeAll;
                }
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition != null)
            {
                Point pos = Mouse.GetPosition(this.Canvas);
                if (pos == this.MousePosition.Value) return;
                if (SelectedTool != null)
                    this.MoveShapes(pos);
            }
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Mouse.OverrideCursor = Cursors.Arrow;
        }




        /// <summary>
        /// 获取鼠标悬停的控件
        /// </summary>
        /// <returns></returns>
        private DrawTool GetCanvasDrawTool()
        {
            return this.Tools.Where(s =>
            s.Element.IsMouseOver).FirstOrDefault();
        }

        private void MoveShapes(Point point)
        {
            Point move = new Point(point.X - this.MousePosition.Value.X, point.Y - this.MousePosition.Value.Y);
            var tooltype = Type.GetType(this.ToolType.ToString());
            switch (SelectedTool.Element.Tag as ToolsType?)
            {
                case ToolsType.TLine:
                    (SelectedTool as TLine).MoveLine(move);
                    break;
                case ToolsType.TRectangle:
                    (SelectedTool as TRectangle).MoveRectangle(move);
                    break;
                case ToolsType.TCircle:
                    break;
                default:
                    break;
            }
            this.MousePosition = Mouse.GetPosition(this.Canvas);
        }


        private void  TEST(TRectangle rect)
        {
            this.Canvas.Children.Remove(SelectedTool.Element);
            var test = new Canvas();
            test.Width = rect.Width;
            test.Height = rect.Height;
            test.Children.Add(rect.Element);
            this.Canvas.Children.Add(test);
            System.Windows.Controls.Canvas.SetTop(test,rect.StartPosition.X);
            System.Windows.Controls.Canvas.SetLeft(test, rect.StartPosition.Y);


            var layer = AdornerLayer.GetAdornerLayer(test);
            var adorner = new CanvasAdorner(test);
            layer.Add(adorner);
        }
    }
}
