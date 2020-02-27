using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace AppDebitiV2.ViewModel.Structure
{
   
        public class BoolToVisibility : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
                System.Convert.ToBoolean(value) ? Visibility.Visible : Visibility.Collapsed;
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
        }

}
