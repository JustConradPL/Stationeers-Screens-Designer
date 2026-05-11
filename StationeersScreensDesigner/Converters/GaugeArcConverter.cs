using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace StationeersScreensDesigner.Converters
{
    public class ArcConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Safety check for uninitialized bindings
            if (values.Length < 5 || values[2] == DependencyProperty.UnsetValue || values[3] == DependencyProperty.UnsetValue)
                return Geometry.Empty;

            double startPct = System.Convert.ToDouble(values[0]);
            double endPct = System.Convert.ToDouble(values[1]);
            double width = System.Convert.ToDouble(values[2]);
            double height = System.Convert.ToDouble(values[3]);
            double thickness = System.Convert.ToDouble(values[4]);

            // 1. Calculate Radius
            double radius = (Math.Min(width, height) / 2) - (thickness / 2) - 5;

            // === THE FIX ===
            // If the control hasn't been drawn yet (Width=0) or is too small to draw an arc,
            // abort and return an empty geometry to prevent the crash.
            if (radius <= 0)
            {
                return Geometry.Empty;
            }

            Point center = new Point(width / 2, height / 2);

            // 2. Map Percentages to Stationeers angles (-135° to +135°)
            double startAngle = -135 + (startPct * 270);
            double endAngle = -135 + (endPct * 270);

            // 3. Math to find the Start and End points on the circle
            Point startPoint = ComputePoint(center, radius, startAngle);
            Point endPoint = ComputePoint(center, radius, endAngle);

            // 4. Create the PathGeometry
            bool isLargeArc = Math.Abs(endAngle - startAngle) > 180;

            var segment = new ArcSegment(endPoint, new Size(radius, radius), 0, isLargeArc, SweepDirection.Clockwise, true);
            var figure = new PathFigure(startPoint, new[] { segment }, false);

            return new PathGeometry(new[] { figure });
        }

        private Point ComputePoint(Point center, double radius, double angleInDegrees)
        {
            double angleInRadians = (angleInDegrees - 90) * (Math.PI / 180.0);
            return new Point(
                center.X + radius * Math.Cos(angleInRadians),
                center.Y + radius * Math.Sin(angleInRadians));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;
    }
}
