using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace graphiceditor.Command
{
    public class Commands
    {

        public static RoutedCommand Line { get; set; }

        public static RoutedCommand Rectangle { get; set; }

        public static RoutedCommand Selector { get; set; }

        public static RoutedCommand AddPolyline { get; set; }

        public static RoutedCommand AddRectangle { get; set; }

        public static RoutedCommand Delete { get; set; }

        static Commands()
        {
            Commands.Line = new RoutedCommand("Open", typeof(Commands));
            Commands.Rectangle = new RoutedCommand("Save", typeof(Commands));
            Commands.Selector = new RoutedCommand("Pointer", typeof(Commands));
            Commands.AddPolyline = new RoutedCommand("AddPolyline", typeof(Commands));
            Commands.AddRectangle = new RoutedCommand("AddRectangle", typeof(Commands));
            Commands.Delete = new RoutedCommand("Delete", typeof(Commands));
        }
    }
}
