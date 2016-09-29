using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace graphiceditor.Tools
{
    public class DrawTool : IMouseOperation
    {
        public ToolsType ToolType { get; set; }

        public Window Window; // 窗口（鼠源）
        public Canvas WorkSpace; //  选取框
        public Border CanvasBorder; // 绘图区域的工作的范围（用于定位）
        public Canvas Canvas; // 工作区域用于绘图
        public Point? MousePosition = null;

        public List<DrawTool> Tools { get; set; }
        public FrameworkElement Element { get; set; }

        public event EventHandler MouseDownEvent;
        public event EventHandler MouseUpEvent;
        public event EventHandler MouseMoveEvent;

        public DrawTool(Window window, Canvas workspace, Border canvasborder, Canvas canvas)
        {
            this.Window = window;
            this.WorkSpace = workspace;
            this.CanvasBorder = canvasborder;
            this.Canvas = canvas;
        }

        public virtual void MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void MouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void MouseMove(object sender, MouseEventArgs e)
        {
        }
    }

    interface IMouseOperation
    {
        void MouseDown(object sender, MouseButtonEventArgs e);
        void MouseUp(object sender, MouseButtonEventArgs e);
        void MouseMove(object sender, MouseEventArgs e);
    }

    public enum ToolsType : byte
    {
        None = 0,
        TSelector = 1,
        TLine,
        TRectangle,
        TCircle,
    }
}
