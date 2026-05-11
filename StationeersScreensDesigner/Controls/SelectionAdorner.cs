using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Controls
{
    public class SelectionAdorner : Adorner
    {
        private VisualCollection _visuals;
        private Thumb _topLeft, _topRight, _bottomLeft, _bottomRight;
        private Thumb _topMid, _bottomMid, _leftMid, _rightMid;

        public SelectionAdorner(UIElement adornedElement) : base(adornedElement)
        {
            _visuals = new VisualCollection(this);

            // Build all 8 handles
            _topLeft = BuildHandle(Cursors.SizeNWSE);
            _topRight = BuildHandle(Cursors.SizeNESW);
            _bottomLeft = BuildHandle(Cursors.SizeNESW);
            _bottomRight = BuildHandle(Cursors.SizeNWSE);
            _topMid = BuildHandle(Cursors.SizeNS);
            _bottomMid = BuildHandle(Cursors.SizeNS);
            _leftMid = BuildHandle(Cursors.SizeWE);
            _rightMid = BuildHandle(Cursors.SizeWE);

            // Hook up the math
            _bottomRight.DragDelta += (s, e) => Resize(0, 0, e.HorizontalChange, e.VerticalChange);
            _topLeft.DragDelta += (s, e) => Resize(e.HorizontalChange, e.VerticalChange, -e.HorizontalChange, -e.VerticalChange);
            _topRight.DragDelta += (s, e) => Resize(0, e.VerticalChange, e.HorizontalChange, -e.VerticalChange);
            _bottomLeft.DragDelta += (s, e) => Resize(e.HorizontalChange, 0, -e.HorizontalChange, e.VerticalChange);

            _topMid.DragDelta += (s, e) => Resize(0, e.VerticalChange, 0, -e.VerticalChange);
            _bottomMid.DragDelta += (s, e) => Resize(0, 0, 0, e.VerticalChange);
            _leftMid.DragDelta += (s, e) => Resize(e.HorizontalChange, 0, -e.HorizontalChange, 0);
            _rightMid.DragDelta += (s, e) => Resize(0, 0, e.HorizontalChange, 0);
        }

        private void Resize(double dx, double dy, double dWidth, double dHeight)
        {
            if (((FrameworkElement)AdornedElement).DataContext is LuaUIElement model)
            {
                bool isSnapping = Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift);
                double gridStep = 10.0;

                if (isSnapping)
                {
                    model.X = Math.Round((model.X + dx) / gridStep) * gridStep;
                    model.Y = Math.Round((model.Y + dy) / gridStep) * gridStep;
                    model.Width = Math.Max(10, Math.Round((model.Width + dWidth) / gridStep) * gridStep);
                    model.Height = Math.Max(10, Math.Round((model.Height + dHeight) / gridStep) * gridStep);
                }
                else
                {
                    model.X += dx;
                    model.Y += dy;
                    model.Width = Math.Max(10, model.Width + dWidth);
                    model.Height = Math.Max(10, model.Height + dHeight);
                }
            }
            var vm = App.Current.MainWindow.DataContext as MainViewModel;
            if (vm != null) vm.RefreshPropertyGrid();
        }

        private Thumb BuildHandle(Cursor cursor)
        {
            var thumb = new Thumb { Width = 8, Height = 8, Background = Brushes.Cyan, Cursor = cursor };
            _visuals.Add(thumb);
            return thumb;
        }

        protected override void OnRender(DrawingContext dc)
        {
            var rect = new Rect(AdornedElement.RenderSize);
            dc.DrawRectangle(null, new Pen(Brushes.Cyan, 1.5) { DashStyle = DashStyles.Dash }, rect);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double w = finalSize.Width;
            double h = finalSize.Height;

            _topLeft.Arrange(new Rect(-4, -4, 8, 8));
            _topRight.Arrange(new Rect(w - 4, -4, 8, 8));
            _bottomLeft.Arrange(new Rect(-4, h - 4, 8, 8));
            _bottomRight.Arrange(new Rect(w - 4, h - 4, 8, 8));

            _topMid.Arrange(new Rect(w / 2 - 4, -4, 8, 8));
            _bottomMid.Arrange(new Rect(w / 2 - 4, h - 4, 8, 8));
            _leftMid.Arrange(new Rect(-4, h / 2 - 4, 8, 8));
            _rightMid.Arrange(new Rect(w - 4, h / 2 - 4, 8, 8));

            return finalSize;
        }

        protected override int VisualChildrenCount => _visuals.Count;
        protected override Visual GetVisualChild(int index) => _visuals[index];
    }
}
