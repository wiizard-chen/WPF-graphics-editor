using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace ToolTray
{
    public class DTLines : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private TLine tline;

        public Canvas canvas;

        private bool IsNew;

        public DTLines(Canvas parent)
        {
            this.canvas = parent;
        }

        public void DWMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.MousePosition = Mouse.GetPosition(this.canvas);
                this.IsNew = true;
            }
        }

        public void DWMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition.HasValue)
            {
                Point p = e.GetPosition(this.canvas);
                if (IsNew)
                {
                    tline = new TLine(this.MousePosition.Value);
                    this.canvas.Children.Add(tline.line);
                    this.IsNew = false;
                }
                tline.ChangeLine(p);
            }
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
            var layer = AdornerLayer.GetAdornerLayer(this.canvas);
            var adorner = new LineAdorner(tline.line,tline.StartPoint,tline.EndPoint);
            adorner.ElementStartChanged += tline.StartResize;
            adorner.ElementEndChanged += tline.EndResize;
            layer.Add(adorner);
            //adorner.Visibility = Visibility.Hidden;
        }
    }
}
