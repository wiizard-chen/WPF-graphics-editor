using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Linq;
using System.Windows.Shapes;

namespace ToolTray
{
    public class DTSelectors : IDWMouseOperation, IDWKeyboardOperation
    {
        public Point? MousePosition { get; set; }

        private FrameworkElement Selected;

        public Canvas canvas;

        public DTSelectors(Canvas parent)
        {
            this.canvas = parent;
        }

        public void DWMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var point = e.GetPosition(this.canvas);
                if (this.MousePosition != point)
                {
                    this.MousePosition = point;
                    if (Selected != null)
                    {
                        (Selected.Tag as IAdroner).AdronerHidden();
                    }
                }
                Selected = GetShape();
                if (Selected != null)
                    (Selected.Tag as IAdroner).AdronerVisble();
            }
        }

        public void DWMouseMove(object sender, MouseEventArgs e)
        {
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private FrameworkElement GetShape()
        {
            var elems = this.canvas.Children.OfType<FrameworkElement>().Where(s => s.Visibility == Visibility.Visible && s.IsMouseOver);
            return elems.DefaultIfEmpty(null).First();
        }

        public void DWKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && Selected != null)
                this.canvas.Children.Remove(this.Selected);
        }

        public void DWKeyUp(object sender, KeyEventArgs e)
        {

        }

        public IDynamicShape GetDynamicShape()
        {
            return (this.Selected.Tag as IDynamicShape);
        }
    }
}
