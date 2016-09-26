using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace shapeeditor
{
    public partial class MainWindow
    {
        private void cmd_LineTool(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.selectedTool.ToolType == DrawToolType.Polyline)
            {
                this.selectedTool.SetToolType(DrawToolType.Pointer);
            }
            else
            {
                this.selectedTool.SetToolType(DrawToolType.Polyline);
            }
        }


        private void  cmd_LineTool(object sender ,ExceptionEventArgs e)
        {
            this.selectedTool.SetToolType(DrawToolType.Pointer);
        }
    }


    public class Commands
    {
        public static RoutedCommand LineTool { get; set; }

        public static RoutedCommand Pointer { get; set;  }

        static Commands()
        {
            Commands.LineTool = new RoutedCommand("Line", typeof(Commands));
            Commands.Pointer = new RoutedCommand("Pointer", typeof(Commands));
        }
    }
}
