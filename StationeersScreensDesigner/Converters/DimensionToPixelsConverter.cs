using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StationeersScreensDesigner.Converters
{
    public class DimensionToPixelsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 0: Value (double), 1: DisplayUnit (string), 2: ParentDimension (double)

            if (values == null || values.Length < 3) return 0.0;

            if (values[0] == DependencyProperty.UnsetValue ||
                values[2] == DependencyProperty.UnsetValue ||
                values[0] == null ||
                values[2] == null)
            {
                return 0.0;
            }

            try
            {
                double val = System.Convert.ToDouble(values[0]);
                string unit = values[1] as string ?? "px";
                double parentDim = System.Convert.ToDouble(values[2]);

                if (unit == "%")
                {
                    // Calculate pixel value based on the parent reference
                    return (val / 100.0) * parentDim;
                }

                return val;
            }
            catch
            {
                // Fallback for cases where string-to-double conversion might fail
                return 0.0;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;
    }
}