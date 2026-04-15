using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace StationeersScreensDesigner.Converters
{
    public class SubtypeToStringConverter : IValueConverter
    {
        public SubtypeToStringConverter()
        {
            
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "Null";

            return value.GetType().Name.Substring(3);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
