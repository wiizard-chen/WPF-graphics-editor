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
        private IDWMouseOperation test;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//select
            Remove();
            test = new DTSelectors(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
            Keyboard.AddPreviewKeyDownHandler(this, (test as IDWKeyboardOperation).DWKeyDown);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTRectangles(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Remove()
        {
            if (test != null)
            {
                Mouse.RemoveMouseDownHandler(this.canvas, test.DWMouseDown);
                Mouse.RemoveMouseMoveHandler(this.canvas, test.DWMouseMove);
                Mouse.RemoveMouseUpHandler(this.canvas, test.DWMouseUp);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTTexts(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTLines(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void canvas_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {

        }
    }
}
