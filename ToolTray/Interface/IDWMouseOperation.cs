using System.Windows.Input;

namespace ToolTray
{
    public interface IDWMouseOperation
    {
        void DWMouseDown(object sender, MouseButtonEventArgs e);
        void DWMouseMove(object sender, MouseEventArgs e);
        void DWMouseUp(object sender, MouseButtonEventArgs e);
    }
}
