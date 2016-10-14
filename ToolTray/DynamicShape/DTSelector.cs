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
    public class DTSelectors : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private IDynamicShape dynamicShape;

        private UIElement Selected;

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
                        //var child = (Selected as Grid).Children.OfType<Shape>().FirstOrDefault();
                        //(child.Tag as IAdroner).AdronerHidden();
                        ((Selected as Grid).Tag as IAdroner).AdronerHidden();
                    }
                }
                Selected = GetShape();
                if (Selected != null)
                    if (Selected is Grid)
                    {
                        ((Selected as Grid).Tag as IAdroner).AdronerVisble();
                        //var child = (Selected as Grid).Children.OfType<Shape>().FirstOrDefault();
                        //(child.Tag as IAdroner).AdronerVisble();
                    }
            }
        }

        public void DWMouseMove(object sender, MouseEventArgs e)
        {
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private UIElement GetShape()
        {
            var elems = this.canvas.Children.OfType<UIElement>().Where(s => s.Visibility == Visibility.Visible && s.IsMouseOver);
            return elems.DefaultIfEmpty(null).First();
        }
    }
}
