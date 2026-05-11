using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace StationeersScreensDesigner.Converters
{
    public class MouseWheelZoomConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MouseWheelEventArgs e && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                return e.Delta;
            }
            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
