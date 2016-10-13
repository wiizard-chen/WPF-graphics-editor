using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolTray
{
    public interface IDynamicShape
    {
        /// <summary>
        /// 新建一个动态编辑的图形
        /// </summary>
        /// <param name="point">起始坐标</param>
        /// <param name="canvas">父类</param>
        /// <returns></returns>
        IDynamicShape GetNewShape(Point point, Canvas canvas);
        /// <summary>
        /// 图形变形
        /// </summary>
        /// <param name="point">变换位置</param>
        void GraphicDistortion(Point point);
        /// <summary>
        /// 图形定型
        /// </summary>
        void GraphicDetermine();

    }
}
