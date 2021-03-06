﻿using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace ToolTray
{
    public class DTTexts : IDWMouseOperation
    {
        public Point? MousePosition { get; set; }

        private IDynamicShape dynamicShape;

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
                //Debug.WriteLine(dynamicShape ==null);
                this.MousePosition = Mouse.GetPosition(this.canvas);
                if (dynamicShape != null)
                    (dynamicShape as TText).ReadOnlyStatus();
                this.IsNew = true;
            }
        }

        public void DWMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition.HasValue)
            {
                Point point = e.GetPosition(this.canvas);
                if (IsNew)
                {
                    dynamicShape = new TText(this.MousePosition.Value, this.canvas);
                    this.IsNew = false;
                }
                dynamicShape.GraphicDistortion(point);
            }
        }

        public void DWMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.GetPosition(this.canvas).X == this.MousePosition.Value.X
                && e.GetPosition(this.canvas).Y == this.MousePosition.Value.Y)
            { }
            else
                dynamicShape.GraphicDetermine();
        }
    }
}
