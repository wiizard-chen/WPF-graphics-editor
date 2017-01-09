using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Documents;

namespace ToolTray
{
    public interface ISaveTheSize
    {
        void SaveTheSize(Canvas canvas, double ratio);
    }
}
