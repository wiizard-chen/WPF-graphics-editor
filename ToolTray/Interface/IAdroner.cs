using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolTray
{
    internal interface IAdroner
    {
        /// <summary>
        /// 装饰器显示
        /// </summary>
        void AdronerVisble();
        /// <summary>
        /// 装饰器隐藏
        /// </summary>
        void AdronerHidden();
    }
}
