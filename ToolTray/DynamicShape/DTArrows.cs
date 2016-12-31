using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
namespace ToolTray
{
    public class DTArrows : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private IDynamicShape dynamicShape;

        public Canvas canvas;

        private bool IsNew;

        public DTArrows(Canvas parent)
        {
            this.canvas = parent;
        }

        public void DWMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton==MouseButton.Left)
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
                    dynamicShape = new TArrow(this.MousePosition.Value, this.canvas);
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
