using graphiceditor.Tools;
using System.Windows;

namespace graphiceditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToolTray tooltray;

        public MainWindow()
        {
            InitializeComponent();
            tooltray = new ToolTray(this, this.workspace, this.border, this.canvas);
        }

        private void cmd_Line(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            tooltray.ChangeTool(ToolsType.TLine);
        }

        private void cmd_Rectangle(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            tooltray.ChangeTool(ToolsType.TRectangle);
        }

        private void cmd_Selector(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            tooltray.ChangeTool(ToolsType.TSelector);
        }
    }
}
