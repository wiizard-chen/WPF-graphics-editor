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
    public class DTRectangles : Canvas, IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private TRectangle trectangle;

        public Canvas canvas;

        private bool IsNew;

        public DTRectangles(Canvas parent)
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
                if (this.IsNew)
                {
                    trectangle = new TRectangle(this.MousePosition.Value);
                    this.canvas.Children.Add(trectangle.rectangle);
                    this.IsNew = false;
                }
                trectangle.ChangeRectangle(p);
            }
        }
        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.canvas.Children.Remove(trectangle.rectangle);
            this.trectangle.NewCanvas();
            this.canvas.Children.Add(trectangle.Parentcanvas);
            Canvas.SetTop(trectangle.Parentcanvas, trectangle.StartPosition.Value.Y);
            Canvas.SetLeft(trectangle.Parentcanvas, trectangle.StartPosition.Value.X);

            var layer = AdornerLayer.GetAdornerLayer(trectangle.Parentcanvas);
            var adorner = new CanvasAdorner(trectangle.Parentcanvas);
            layer.Add(adorner);
            TESTCANVAS();
        }

        public void TESTCANVAS()
        {
            foreach (var item in this.canvas.Children)
            {
                Debug.WriteLine(item.ToString());
            }
            Debug.WriteLine(trectangle.Parentcanvas.Width);
            Debug.WriteLine(trectangle.Parentcanvas.Height);

        }

    }
}