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

namespace shapeeditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public DrawTool selectedTool;
        public MainWindow()
        {
            InitializeComponent();
            this.selectedTool = new DrawTool(this, this.workspace, this.border, this.canvas, DrawToolType.Pointer);
           // this.selectedTool.SelectionChange += selectedTool_SelectionChange;
            //this.UpdateButtons();
        }


    }
}
