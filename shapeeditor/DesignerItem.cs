using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace test
{
   public class DesignerItem :ContentControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
                                DependencyProperty.Register("IsSelected", typeof(bool),
                                typeof(DesignerItem),
                                new FrameworkPropertyMetadata(false));
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }


    }
}
