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
                    this.IsNew = false;
            }
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
        }
    }
}
