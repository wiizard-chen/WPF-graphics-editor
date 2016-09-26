using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace graphiceditor.Tools
{
    public class Tools
    {
        //public Window window; // 窗口（鼠源）
        //public Canvas workspace; //  选取框
        //public Border canvasBorder; // 绘图区域的工作的范围（用于定位）
        //public Canvas canvas; // 工作区域用于绘图
        //public Point? mousePostion = null;//鼠标点击位置


        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseMove;

        //public Tools(Window Window, Canvas Workspace, Border CanvasBorder, Canvas Canvas)
        //{
        //    this.window = Window;
        //    this.canvas = Canvas;
        //    this.workspace = Workspace;
        //    this.canvasBorder = CanvasBorder;
        //}

    }

    public enum ToolsType :byte
    {
        line = 0,
        Retangle=1
    }
}
