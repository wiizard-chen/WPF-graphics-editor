using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

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

            //this.canvas.Children.Remove(tline.line);
            //this.tline.NewCanvas();
            //this.canvas.Children.Add(tline.Parentcanvas);
            //Canvas.SetTop(tline.Parentcanvas, tline.StartPosition.Value.Y);
            //Canvas.SetLeft(tline.Parentcanvas, tline.StartPosition.Value.X);

            var layer = AdornerLayer.GetAdornerLayer(this.canvas);
            var adorner = new LineAdorner(tline.line, tline.StartPoint, tline.EndPoint);
            adorner.ElementStartChanged += tline.StartResize;
            adorner.ElementEndChanged += tline.EndResize;
            adorner.ElementMove += tline.MoveLine;
            layer.Add(adorner);
            //adorner.Visibility = Visibility.Hidden;
        }
    }
}
