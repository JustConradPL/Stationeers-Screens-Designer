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
            // Inputs: 0:Min, 1:Max, 2:StartVal, 3:EndVal, 4:Radius, 5:Thickness, 6:Invert
            if (values.Length < 7 || values.Any(v => v == DependencyProperty.UnsetValue)) return null;

            double min = (double)values[0];
            double max = (double)values[1];
            double vStart = (double)values[2];
            double vEnd = (double)values[3];
            double radius = (double)values[4] / 2;
            double thickness = (double)values[5];
            bool invert = (bool)values[6];

            double actualRadius = radius - (thickness / 2);

            double GetAngle(double val)
            {
                double ratio = (val - min) / (max - min);
                if (invert) ratio = 1 - ratio;
                return -180 + (ratio * 180);
            }

            double angleStart = GetAngle(vStart);
            double angleEnd = GetAngle(vEnd);

            double drawStart = invert ? angleEnd : angleStart;
            double drawEnd = invert ? angleStart : angleEnd;

            var startPt = ComputePoint(drawStart, actualRadius, radius);
            var endPt = ComputePoint(drawEnd, actualRadius, radius);

            var figure = new PathFigure { StartPoint = startPt, IsClosed = false };
            figure.Segments.Add(new ArcSegment(endPt, new Size(actualRadius, actualRadius), 0, false, SweepDirection.Clockwise, true));

            return new PathGeometry(new[] { figure });
        }

        private Point ComputePoint(double angle, double radius, double center)
        {
            double rad = Math.PI * angle / 180.0;
            return new Point(center + radius * Math.Cos(rad), center + radius * Math.Sin(rad));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;
    }
}
