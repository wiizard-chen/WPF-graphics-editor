using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Reflection;

namespace graphiceditor.Tools
{
    public class TSelector : DrawTool
    {
        public DrawTool SelectedTool { get; set; }

        public TSelector(Window window, Canvas workspace, Border canvasborder, Canvas canvas) : base(window, workspace, canvasborder, canvas)
        {
            this.ToolType = ToolsType.TSelector;
        }

        public override void MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.MousePosition = e.GetPosition(this.Canvas);
                this.GetCanvasDrawTool();
                if (SelectedTool != null)
                {
                    Mouse.OverrideCursor = Cursors.SizeAll;
                }
            }
        }

        public override void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && this.MousePosition != null)
            {
                Point pos = Mouse.GetPosition(this.Canvas);
                if (pos == this.MousePosition.Value) return;
                if (SelectedTool != null)
                    this.MoveShapes(pos);
            }
        }

        public override void MouseUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
        }

        /// <summary>
        /// 获取鼠标悬停的控件
        /// </summary>
        /// <returns></returns>
        private void GetCanvasDrawTool()
        {
            SelectedTool = this.Tools.Where(s =>
            s.Element.IsMouseOver).FirstOrDefault();
        }

        private void MoveShapes(Point point)
        {
            Point move = new Point(point.X - this.MousePosition.Value.X, point.Y - this.MousePosition.Value.Y);
            var tooltype = Type.GetType(this.ToolType.ToString());
            switch (SelectedTool.Element.Tag as ToolsType?)
            {
                case ToolsType.TLine:
                    (SelectedTool as TLine).MoveLine(move);
                    break;
                case ToolsType.TRectangle:
                    (SelectedTool as TRectangle).MoveRectangle(move);
                    break;
                case ToolsType.TCircle:
                    break;
                default:
                    break;
            }
            this.MousePosition = Mouse.GetPosition(this.Canvas);
        }
    }
}
