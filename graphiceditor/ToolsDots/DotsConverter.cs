using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace graphiceditor.ToolsDots
{
    /// <summary>
    /// 点与坐标的转换
    /// </summary>
    public class DotsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue
                || values[1] == DependencyProperty.UnsetValue
                || !(values[0] is double)
                || !(values[1] is double)
                )
                return DependencyProperty.UnsetValue;
            return (double)values[0] - (double)values[1] / 2;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
