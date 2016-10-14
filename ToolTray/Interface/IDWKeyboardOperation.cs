using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ToolTray
{
   public interface IDWKeyboardOperation
    {
        /// <summary>
        /// 键盘按下事件
        /// </summary>
        void DWKeyDown(object sender, KeyEventArgs e);

        /// <summary>
        /// 键盘松开事件
        /// </summary>
        void DWKeyUp(object sender, KeyEventArgs e);

    }
}
