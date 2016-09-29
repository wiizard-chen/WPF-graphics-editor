using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace graphiceditor.Tools
{
    public class ToolTray
    {
        public ToolsType DrawToolType { get; set; }

        public Window Window; // 窗口（鼠源）
        public Canvas WorkSpace; //  选取框
        public Border CanvasBorder; // 绘图区域的工作的范围（用于定位）
        public Canvas Canvas; // 工作区域用于绘图

        public List<DrawTool> tools;
        public DrawTool SingleTool;



        public ToolTray(Window window, Canvas workspace, Border canvasborder, Canvas canvas)
        {
            this.Window = window;
            this.WorkSpace = workspace;
            this.CanvasBorder = canvasborder;
            this.Canvas = canvas;
            this.DrawToolType = ToolsType.None;
            this.tools = new List<DrawTool>();
        }


        public void ChangeTool(ToolsType type)
        {
            if (this.DrawToolType != type)
            {
                InitializeTools(type);
                this.DrawToolType = type;
            }
        }


        private void InitializeTools(ToolsType type)
        {
            if (SingleTool != null)
                RemoveMouseEvent(SingleTool);
            if (type != ToolsType.None)
                CreateSingleTool(type);
        }

        private void CreateSingleTool(ToolsType type)
        {
            string assemblyName = "graphiceditor";
            string typeName = "graphiceditor.Tools." + type;
            SingleTool = Assembly.Load(assemblyName).CreateInstance(
                typeName,
                true, BindingFlags.Default,
                null,
                new object[] { Window, WorkSpace, CanvasBorder, Canvas },
                null,
                null)
                as DrawTool;

            SingleTool.Tools = tools;
            AddMouseEvent(SingleTool);

        }

        private void AddMouseEvent(DrawTool tool)
        {
            Mouse.AddMouseDownHandler(this.Canvas, tool.MouseDown);
            Mouse.AddMouseMoveHandler(this.Canvas, tool.MouseMove);
            Mouse.AddMouseUpHandler(this.Canvas, tool.MouseUp);
        }

        private void RemoveMouseEvent(DrawTool tool)
        {
            Mouse.RemoveMouseDownHandler(this.Canvas, tool.MouseDown);
            Mouse.RemoveMouseMoveHandler(this.Canvas, tool.MouseMove);
            Mouse.RemoveMouseUpHandler(this.Canvas, tool.MouseUp);
        }

        private void GraphicsMouseUp(object sender, EventArgs e)
        {
            InitializeTools(this.DrawToolType);
        }
    }
}
