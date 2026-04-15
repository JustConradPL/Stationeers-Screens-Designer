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
                        item.IsSelected = (item == model);
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

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && AssociatedObject.DataContext is LuaUIElement model)
            {
                var presenter = VisualTreeHelper.GetParent(AssociatedObject) as UIElement;
                var canvas = VisualTreeHelper.GetParent(presenter) as UIElement;

                if (canvas != null)
                {
                    var currentPos = e.GetPosition(canvas);
                    double rawX = currentPos.X - _mouseOffset.X;
                    double rawY = currentPos.Y - _mouseOffset.Y;

                    // Shift key -> Snap to grid
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        double gridStep = 10.0;
                        model.X = Math.Round(rawX / gridStep) * gridStep;
                        model.Y = Math.Round(rawY / gridStep) * gridStep;
                    }
                    else
                    {
                        model.X = rawX;
                        model.Y = rawY;
                    }
                }
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
