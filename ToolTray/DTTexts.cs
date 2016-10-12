using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ToolTray
{
    public class DTTexts : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private TText tText;

        public Canvas canvas;

        private bool IsNew;

        public DTTexts(Canvas parent)
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
                    tText = new TText(p);
                    this.canvas.Children.Add(tText.TextRegion);
                    this.IsNew = false;
                }
                tText.ChangeSelect(p);
            }
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.canvas.Children.Remove(tText.TextRegion);
            tText.NewCanvas();
            this.canvas.Children.Add(tText.Parentcanvas);
            Canvas.SetTop(tText.Parentcanvas, tText.StartPosition.Value.Y);
            Canvas.SetLeft(tText.Parentcanvas, tText.StartPosition.Value.X);
            var layer = AdornerLayer.GetAdornerLayer(this.canvas);
            var adorner = new CanvasAdorner(tText.Parentcanvas);
            layer.Add(adorner);
        }
    }
}
