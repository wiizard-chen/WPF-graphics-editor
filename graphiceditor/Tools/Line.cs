using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace graphiceditor.Tools
{
    public class TLine : DrawTool
    {
        private Line line;
        private TLine T_line;

        public TLine(Window window, Canvas workspace, Border canvasborder, Canvas canvas) : base(window, workspace, canvasborder, canvas)
        {
            this.ToolType = ToolsType.TLine;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.MousePosition = Mouse.GetPosition(this.Canvas);
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition.HasValue)
            {
                Point? pos = e.GetPosition(this.Canvas);
                if (this.line == null)
                {
                    line = new Line();
                    line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;
                    line.StrokeThickness = 10;
                    line.X1 = pos.Value.X;
                    line.Y1 = pos.Value.Y;
                    line.Tag = ToolsType.TLine;
                    this.Canvas.Children.Add(line);
                }
                else
                {
                    line.X2 = pos.Value.X;
                    line.Y2 = pos.Value.Y;
                }
            }
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (line != null)
                {
                    T_line = new TLine(Window, WorkSpace, CanvasBorder, Canvas);
                    T_line.line = this.line;
                    T_line.Element = this.line;
                    this.Tools.Add(T_line);
                    this.line = null;
                }
            }
        }

        public void MoveLine(Point move)
        {
            Point start = new Point(line.X1, line.Y1);
            Point end = new Point(line.X2, line.Y2);
            start.Offset(move.X, move.Y);
            end.Offset(move.X, move.Y);
            line.X1 = start.X;
            line.Y1 = start.Y;
            line.X2 = end.X;
            line.Y2 = end.Y;
        }
    }
}
