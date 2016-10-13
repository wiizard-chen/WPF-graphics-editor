using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolTray;
namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var test = new DTTexts(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
