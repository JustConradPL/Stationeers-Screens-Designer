using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using Microsoft.Xaml.Behaviors;

using StationeersScreensDesigner.Controls;
using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Behaviours
{
    public class DesignerItemBehavior : Behavior<FrameworkElement>
    {
        private SelectionAdorner _adorner;
        private Point _mouseOffset;
        private bool _isDragging;

        private Point _startMousePos;
        private Point _grabOffset;
        private double _initialPixelX;
        private double _initialPixelY;
        private double _initialWidth;
        private double _initialHeight;

        private const double GridSize = 10.0;

        private Canvas? _parentCanvas;
        private Canvas? ParentCanvas => _parentCanvas ??= FindParent(AssociatedObject, typeof(Canvas)) as Canvas;
        private MainViewModel? Screen => (FindParent(AssociatedObject, typeof(ItemsControl)) as ItemsControl)?.DataContext as MainViewModel;
        private DependencyObject? FindParent(DependencyObject child, Type parentType)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(child);
            while (parent != null && !parentType.IsInstanceOfType(parent))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return parent;
        }
        protected override void OnAttached()
        {
            AssociatedObject.Loaded += (s, e) =>
            {
                if (AssociatedObject.DataContext is LuaUIElement model)
                {
                    // Watch for property changes
                    model.PropertyChanged += OnModelPropertyChanged;
                    // Check initial state
                    UpdateAdorner(model.IsSelected);
                }
            };
            AssociatedObject.MouseDown += MouseDown;
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }

        private void MouseDown(object? sender, MouseButtonEventArgs e)
        {
            if (AssociatedObject.DataContext is LuaUIElement model)
            {
                model.IsSelected = true;
                _isDragging = true;
                _mouseOffset = e.GetPosition(AssociatedObject);
                AssociatedObject.CaptureMouse();

                var presenter = VisualTreeHelper.GetParent(AssociatedObject) as ContentPresenter;
                var itemsControl = ItemsControl.ItemsControlFromItemContainer(presenter);
                bool isControlPressed = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);

               



                if (itemsControl != null && itemsControl.ItemsSource is IEnumerable<LuaUIElement> collection && !isControlPressed)
                {
                    foreach (var item in collection)
                    {
                        if (item == model)
                        {
                            item.IsSelected = true;
                            _startMousePos = e.GetPosition(ParentCanvas);


                            Point visualTopLeft = AssociatedObject.TranslatePoint(new Point(0, 0), ParentCanvas);

                            _initialPixelX = visualTopLeft.X;
                            _initialPixelY = visualTopLeft.Y;

                            // Calculate the grab offset (where inside the element the user clicked)
                            _grabOffset = new Point(_startMousePos.X - _initialPixelX, _startMousePos.Y - _initialPixelY);
                        }
                        else
                        {
                            item.IsSelected = false;
                        }
                    }
                }

                var vm = App.Current.MainWindow.DataContext as MainViewModel;
                if (vm != null) vm.RefreshPropertyGrid();

            }
            e.Handled = true;
        }

        private void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is null) return;
            if (e.PropertyName == nameof(LuaUIElement.IsSelected))
            {
                UpdateAdorner(((LuaUIElement)sender).IsSelected);
            }
        }

        private void UpdateAdorner(bool isSelected)
        {
            var layer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            if (layer == null) return;

            if (isSelected && _adorner == null)
            {
                _adorner = new SelectionAdorner(AssociatedObject);
                layer.Add(_adorner);
            }
            else if (!isSelected && _adorner != null)
            {
                layer.Remove(_adorner);
                _adorner = null;
            }
        }

        //private void OnMouseMove(object sender, MouseEventArgs e)
        //{
        //    if (_isDragging && AssociatedObject.DataContext is LuaUIElement model)
        //    {
        //        var presenter = VisualTreeHelper.GetParent(AssociatedObject) as UIElement;
        //        var canvas = VisualTreeHelper.GetParent(presenter) as UIElement;

        //        if (canvas != null)
        //        {
        //            var currentPos = e.GetPosition(canvas);
        //            double rawX = currentPos.X - _mouseOffset.X;
        //            double rawY = currentPos.Y - _mouseOffset.Y;

        //            // Shift key -> Snap to grid
        //            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        //            {
        //                double gridStep = 10.0;
        //                model.X = Math.Round(rawX / gridStep) * gridStep;
        //                model.Y = Math.Round(rawY / gridStep) * gridStep;
        //            }
        //            else
        //            {
        //                model.X = rawX;
        //                model.Y = rawY;
        //            }
        //        }
        //    }
        //}

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || AssociatedObject.DataContext is not LuaUIElement item || Screen == null || ParentCanvas == null) return;

            Point currentPos = e.GetPosition(ParentCanvas);

            // 1. Calculate the new target position in PIXELS
            double newPixelX = currentPos.X - _grabOffset.X;
            double newPixelY = currentPos.Y - _grabOffset.Y;

            // 2. GRID SNAPPING: If Shift is held, snap the pixel coordinates
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
                newPixelX = Math.Round(newPixelX / GridSize) * GridSize;
                newPixelY = Math.Round(newPixelY / GridSize) * GridSize;
            }

            // 3. CONVERT BACK TO UNIT: Update the ViewModel based on the unit mode
            if (item.Unit == "%")
            {
                // Reverse of: (val / 100) * targetWidth
                item.X = (newPixelX / Screen.CurrentScreen.TargetWidth) * 100.0;
                item.Y = (newPixelY / Screen.CurrentScreen.TargetHeight) * 100.0;
            }
            else
            {
                item.X = newPixelX;
                item.Y = newPixelY;
            }
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            AssociatedObject.ReleaseMouseCapture();
            var vm = App.Current.MainWindow.DataContext as MainViewModel;
            if (vm != null) vm.RefreshPropertyGrid();
        }
    }
}
