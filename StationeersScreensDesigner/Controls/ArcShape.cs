using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StationeersScreensDesigner.Controls
{
    public class ArcShape : Shape
    {
        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register("StartAngle", typeof(double), typeof(ArcShape), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public double StartAngle { get => (double)GetValue(StartAngleProperty); set => SetValue(StartAngleProperty, value); }

        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register("EndAngle", typeof(double), typeof(ArcShape), new FrameworkPropertyMetadata(90.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public double EndAngle { get => (double)GetValue(EndAngleProperty); set => SetValue(EndAngleProperty, value); }

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(ArcShape), new FrameworkPropertyMetadata(50.0, FrameworkPropertyMetadataOptions.AffectsRender));
        public double Radius { get => (double)GetValue(RadiusProperty); set => SetValue(RadiusProperty, value); }

        protected override Geometry DefiningGeometry
        {
            get
            {
                // Simple geometry for a straight line as a placeholder.
                // Complete arc geometry requires complex mathematics to convert angles to coordinates.
                return new LineGeometry(new Point(0, 0), new Point(10, 10));
            }
        }
    }
}