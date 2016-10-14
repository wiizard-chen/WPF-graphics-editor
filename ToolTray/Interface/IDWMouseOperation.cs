using System.Windows.Input;

namespace ToolTray
{
    public interface IDWMouseOperation
    {
        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        void DWMouseDown(object sender, MouseButtonEventArgs e);
        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        void DWMouseMove(object sender, MouseEventArgs e);
        /// <summary>
        /// 鼠标松开事件
        /// </summary>
        void DWMouseUp(object sender, MouseButtonEventArgs e);
    }
}
