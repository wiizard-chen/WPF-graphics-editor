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
    public class LineAdorner : Adorner, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }

        private const double THUMB_SIZE = 12;
        private const double MINIMAL_SIZE = 20;
        private const double MOVE_OFFSET = 8;//20;

        private Point startpoint;
        private Point endpoint;
        private Thumb Start, End;
        //private Thumb mov;
        private VisualCollection visCollec;

        public FrameworkElement Element
        {
            get
            {
                return AdornedElement as FrameworkElement;
            }
        }

        public event EventHandler ElementSizeChanged;


        public LineAdorner(UIElement adorned,Point start,Point end) : base(adorned)
        {
            startpoint = start;
            endpoint = end;
            visCollec = new VisualCollection(this);
            visCollec.Add(Start=getReizeThumb());
            visCollec.Add(End=getReizeThumb());
            //visCollec.Add(mov);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double offset = THUMB_SIZE / 2;
            Size size = new Size(THUMB_SIZE, THUMB_SIZE);
            Start.Arrange(new Rect(new Point(startpoint.X-offset,startpoint.Y-offset), size));
            End.Arrange(new Rect(new Point(endpoint.X - offset, endpoint.Y - offset), size));
            return finalSize;
        }

        private Thumb getReizeThumb()
        {
            Brush b;
            b = GetRect();
            var thumb = new Thumb()
            {
                Background = Brushes.Red,
                Width = THUMB_SIZE,
                Height = THUMB_SIZE,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Template = new ControlTemplate(typeof(Thumb))
                {
                    VisualTree = getRectangleFactory(b)
                }
            };
            thumb.DragDelta += (s, e) =>
             {
                 Point point = new Point(e.HorizontalChange, e.VerticalChange);
                 if (ElementSizeChanged != null)
                     ElementSizeChanged(point, EventArgs.Empty);
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


        private Brush GetRect()
        {
            string lan = "M 0,0 h 5 M 0,0 v 5 M 0,5 h 5 M 5,0 v 5";
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
