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
    public class DTRectangles : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private IDynamicShape dynamicShape;

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
                Point point = e.GetPosition(this.canvas);
                if (this.IsNew)
                {
                    dynamicShape = new TRectangle(this.MousePosition.Value, this.canvas);
                    this.IsNew = false;
                }
                dynamicShape.GraphicDistortion(point);
            }
        }
        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
            dynamicShape.GraphicDetermine();
        }
    }
}