using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace StationeersScreensDesigner.Controls
{
    public class FollowerAdorner : Adorner
    {
        private UIElement _child;
        private TranslateTransform _transform = new TranslateTransform();

        public FollowerAdorner(UIElement adornedElement, UIElement child)
            : base(adornedElement)
        {
            _child = child;
            _child.RenderTransform = _transform;
            AddVisualChild(_child);
        }

        public void UpdatePosition(Point p)
        {
            _transform.X = p.X;
            _transform.Y = p.Y;

            InvalidateVisual();
        }

        protected override int VisualChildrenCount => 1;
        protected override Visual GetVisualChild(int index) => _child;
        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            _child.Arrange(new Rect(_child.DesiredSize));
            return finalSize;
        }
    }
}
