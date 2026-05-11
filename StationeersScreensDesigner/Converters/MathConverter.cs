using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace StationeersScreensDesigner.Converters
{
    public class MathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double val = System.Convert.ToDouble(value);
            double multiplier = double.Parse(parameter.ToString());
            return val * multiplier;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
        
    }
}
