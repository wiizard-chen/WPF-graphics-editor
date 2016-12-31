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
    public class ArrowAdorner : Adorner, INotifyPropertyChanged
    {
        #region 接口实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion



        private const double THUMB_SIZE = 12;
        private const double MINIMAL_SIZE = 20;
        private const double MOVE_OFFSET = 8;

        private Point startpoint;
        private Point endpoint;
        private Thumb Start, End;
        private Thumb Move;
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


        public event EventHandler ElementStartChanged;
        public event EventHandler ElementEndChanged;
        public event EventHandler ElementMove;

        #region 构造函数

        public ArrowAdorner(UIElement adorned, Point start, Point end) : base(adorned)
        {
            startpoint = start;
            endpoint = end;
            visCollec = new VisualCollection(this);
            visCollec.Add(Start = getReizeThumb(HorizontalAlignment.Left, VerticalAlignment.Top));
            visCollec.Add(End = getReizeThumb(HorizontalAlignment.Right, VerticalAlignment.Bottom));
            visCollec.Add(Move = getMoveThumb());
        }
        #endregion

        protected override Size ArrangeOverride(Size finalSize)
        {
            double offset = THUMB_SIZE / 2;
            Size size = new Size(THUMB_SIZE, THUMB_SIZE);
            Start.Arrange(new Rect(new Point(startpoint.X - offset, startpoint.Y - offset), size));
            End.Arrange(new Rect(new Point(endpoint.X - offset, endpoint.Y - offset), size));
            Move.Arrange(new Rect(
                new Point(
                   startpoint.X - (startpoint.X - endpoint.X) / 2 - offset,
                    startpoint.Y - (startpoint.Y - endpoint.Y) / 2 - offset),
                    size));
            return finalSize;
        }

        private Thumb getReizeThumb(HorizontalAlignment hoa, VerticalAlignment vra)
        {
            Brush b;
            b = GetRect();
            var thumb = new Thumb()
            {
                Background = Brushes.Red,
                Width = THUMB_SIZE,
                Height = THUMB_SIZE,

                HorizontalAlignment = hoa,
                VerticalAlignment = vra,

                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = getRectangleFactory(b)
                }
            };
            thumb.DragDelta += (s, e) =>
            {
                Point point = new Point(e.HorizontalChange, e.VerticalChange);

                switch (thumb.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        this.startpoint.Offset(e.HorizontalChange, e.VerticalChange);
                        if (ElementStartChanged != null)
                            ElementStartChanged(point, EventArgs.Empty);
                        break;
                    case VerticalAlignment.Bottom:
                        this.endpoint.Offset(e.HorizontalChange, e.VerticalChange);
                        if (ElementEndChanged != null)
                            ElementEndChanged(point, EventArgs.Empty);
                        break;
                }
            };
            thumb.DragCompleted += (s, e) =>
            {

            };
            return thumb;
        }

        private Thumb getMoveThumb()
        {
            Brush b = GetMoveEllipseBack();
            var thumb = new Thumb()
            {
                Width = THUMB_SIZE,
                Height = THUMB_SIZE,
                Cursor = Cursors.SizeAll,
                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = getFactory(b)
                }
            };
            thumb.DragDelta += (s, e) =>
            {
                Point point = new Point(e.HorizontalChange, e.VerticalChange);
                this.startpoint.Offset(e.HorizontalChange, e.VerticalChange);
                this.endpoint.Offset(e.HorizontalChange, e.VerticalChange);

                if (ElementMove != null)
                    ElementMove(point, EventArgs.Empty);
            };
            return thumb;
        }

        private FrameworkElementFactory getRectangleFactory(Brush back)
        {
            back.Opacity = 0.6;
            var fef = new FrameworkElementFactory(typeof(Rectangle));
            fef.SetValue(Rectangle.FillProperty, back);
            return fef;
        }



        private FrameworkElementFactory getFactory(Brush back)
        {
            back.Opacity = 0.6;
            var fef = new FrameworkElementFactory(typeof(Ellipse));
            fef.SetValue(Ellipse.FillProperty, back);
            fef.SetValue(Ellipse.StrokeProperty, Brushes.Transparent);
            fef.SetValue(Ellipse.StrokeThicknessProperty, (double)1);
            return fef;
        }

        private Brush GetRect()
        {
            string lan = "M 0,0 h 5 M 0,0 v 5 M 0,5 h 5 M 5,0 v 5";
            return GetBrush(lan, 1);
        }

        private Brush GetMoveEllipseBack()
        {
            string lan = "M 0,5 h 10 M 5,0 v 10";
            return GetBrush(lan, 2);
        }



        private static Brush GetBrush(string lan, double w)
        {
            var converter = TypeDescriptor.GetConverter(typeof(Geometry));
            var geometry = (Geometry)converter.ConvertFrom(lan);
            TileBrush bsh = new DrawingBrush(new GeometryDrawing(Brushes.Transparent, new Pen(Brushes.Black, w), geometry));
            bsh.Stretch = Stretch.Fill;
            return bsh;
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
