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
        private Thumb start, end;
        private Thumb mov;
        private VisualCollection visCollec;

        public FrameworkElement Element
        {
            get
            {
                return AdornedElement as FrameworkElement;
            }
        }


        public LineAdorner(UIElement adorned) :base(adorned)
        {
            visCollec = new VisualCollection(this);
            visCollec.Add(start);
            visCollec.Add(end);
            visCollec.Add(mov);
        }

    }
}
