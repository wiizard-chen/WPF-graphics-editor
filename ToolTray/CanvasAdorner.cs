using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Diagnostics;

namespace ToolTray
{
    public class CanvasAdorner : Adorner, INotifyPropertyChanged
    {
        #region 接口实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion

        #region 事件实现
        /// <summary>
        /// 位置改变
        /// </summary>
        public event EventHandler ElementPositionChanged;
        /// <summary>
        /// 大小改变
        /// </summary>
        public event EventHandler ElementSizeChanged;
        #endregion

        #region 业务属性
        private const double THUMB_SIZE = 12;
        private const double MINIMAL_SIZE = 20;
        private const double MOVE_OFFSET = 8;//20;
        private Thumb tl, tr, bl, br;
        private Thumb mov;
        private VisualCollection visCollec;
        /// <summary>
        /// 内部控件
        /// </summary>
        public FrameworkElement Element
        {
            get
            {
                return AdornedElement as FrameworkElement;
            }
        }
        #endregion

        #region 构造函数
        public CanvasAdorner(UIElement adorned)
            : base(adorned)
        {
            visCollec = new VisualCollection(this);
            visCollec.Add(tl = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Left, VerticalAlignment.Top));
            visCollec.Add(tr = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Right, VerticalAlignment.Top));
            visCollec.Add(bl = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Left, VerticalAlignment.Bottom));
            visCollec.Add(br = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Right, VerticalAlignment.Bottom));
            visCollec.Add(mov = GetMoveThumb());
        }

        #endregion


        protected override Size ArrangeOverride(Size finalSize)
        {
            double offset = THUMB_SIZE / 2;
            Size sz = new Size(THUMB_SIZE, THUMB_SIZE);
            tl.Arrange(new Rect(new Point(-offset, -offset), sz));
            tr.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - offset, -offset), sz));
            bl.Arrange(new Rect(new Point(-offset, AdornedElement.RenderSize.Height - offset), sz));
            br.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - offset, AdornedElement.RenderSize.Height - offset), sz));
            //mov.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width / 2 - THUMB_SIZE / 2, -MOVE_OFFSET), sz));
            mov.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width / 2-offset, AdornedElement.RenderSize.Height/2-offset), sz));
            //Debug.WriteLine(AdornedElement.RenderSize.Width);
            //Debug.WriteLine(AdornedElement.RenderSize.Height);
            return finalSize;
        }

        private void Resize(FrameworkElement ff)
        {
            if (Double.IsNaN(ff.Width))
                ff.Width = ff.RenderSize.Width;
            if (Double.IsNaN(ff.Height))
                ff.Height = ff.RenderSize.Height;
        }

        private Thumb GetMoveThumb()
        {
            var thumb = new Thumb()
            {
                Width = THUMB_SIZE,
                Height = THUMB_SIZE,
                HorizontalAlignment =HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = Cursors.SizeAll,
                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = GetFactory(GetMoveEllipseBack())
                }
            };
            thumb.DragDelta += (s, e) =>
            {
                var element = AdornedElement as FrameworkElement;
                if (element == null)
                    return;

                Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
                if (ElementPositionChanged != null)
                    ElementPositionChanged(new Point(Canvas.GetLeft(element), Canvas.GetTop(element)), EventArgs.Empty);
            };

            return thumb;
        }
        private Thumb GetResizeThumb(Cursor cur, HorizontalAlignment hor, VerticalAlignment ver)
        { 
            Brush b;
            if (hor == HorizontalAlignment.Left)//左边
            {
                if (ver == VerticalAlignment.Top)
                {
                    b = GetLTBack();
                }
                else
                {
                    b = GetLBBack();
                }
            }
            else//右边
            {
                if (ver == VerticalAlignment.Top)
                {
                    b = GetRTBack();
                }
                else
                {
                    b = GetRBBack();
                }
            }
            var thumb = new Thumb()
            {
                Background = Brushes.Red,
                Width = THUMB_SIZE,
                Height = THUMB_SIZE,
                HorizontalAlignment = hor,
                VerticalAlignment = ver,
                Cursor = cur,
                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = GetRectangleFactory(b)
                }
            };
            thumb.DragDelta += (s, e) =>
            {
                var element = AdornedElement as FrameworkElement;
                if (element == null)
                    return;

                Resize(element);

                switch (thumb.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        if (element.Height + e.VerticalChange > MINIMAL_SIZE)
                        {
                            element.Height += e.VerticalChange;
                        }
                        break;
                    case VerticalAlignment.Top:
                        if (element.Height - e.VerticalChange > MINIMAL_SIZE)
                        {
                            element.Height -= e.VerticalChange;
                            Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
                        }
                        break;
                }
                switch (thumb.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        if (element.Width - e.HorizontalChange > MINIMAL_SIZE)
                        {
                            element.Width -= e.HorizontalChange;
                            Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                        }
                        break;
                    case HorizontalAlignment.Right:
                        if (element.Width + e.HorizontalChange > MINIMAL_SIZE)
                        {
                            element.Width += e.HorizontalChange;
                        }
                        break;
                }
                if (ElementSizeChanged != null)
                    ElementSizeChanged(new Size(element.Width, element.Height), EventArgs.Empty);
                e.Handled = true;
            };
            return thumb;
        }

        #region 装饰器的刷子

        private Brush GetMoveEllipseBack()
        {
            string lan = "M 0,5 h 10 M 5,0 v 10";
            return GetBrush(lan, 2);
        }

        private Brush GetLTBack()
        {
            string lan = "M 0,0 h 5 M 0,0 v 5";
            return GetBrush(lan, 1);
        }

        private Brush GetRTBack()
        {
            string lan = "M 0,0 h 5 M 5,0 v 5";
            return GetBrush(lan, 1);
        }

        private Brush GetLBBack()
        {
            string lan = "M 0,0 v 5 M 0,5 h 5";
            return GetBrush(lan, 1);
        }

        private Brush GetRBBack()
        {
            string lan = "M 0,5 h 5 M 5,0 v 5";
            return GetBrush(lan, 1);
        }

        private static Brush GetBrush(string lan, double w)
        {
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            var geometry = (Geometry)converter.ConvertFrom(lan);
            TileBrush bsh = new DrawingBrush(new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Black, w), geometry));
            bsh.Stretch = Stretch.Fill;
            return bsh;
        }

        #endregion


        private FrameworkElementFactory GetFactory(Brush back)
        {
            back.Opacity = 0.6;
            var fef = new FrameworkElementFactory(typeof(Ellipse));
            fef.SetValue(Ellipse.FillProperty, back);
            fef.SetValue(Ellipse.StrokeProperty, Brushes.White);
            fef.SetValue(Ellipse.StrokeThicknessProperty, (double)1);
            return fef;
        }

        private FrameworkElementFactory GetRectangleFactory(Brush back)
        {
            back.Opacity = 0.6;
            var fef = new FrameworkElementFactory(typeof(Rectangle));
            fef.SetValue(Rectangle.FillProperty, back);
            return fef;
        }

        protected override Visual GetVisualChild(int index)
        {
            return visCollec[index];
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visCollec.Count;
            }
        }

    }
}
