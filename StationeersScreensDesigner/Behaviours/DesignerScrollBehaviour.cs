using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace StationeersScreensDesigner.Behaviours
{
    public class DesignerScrollBehaviour : Behavior<ScrollViewer>
    {


        public ICommand ZoomCommand
        {
            get { return (ICommand)GetValue(ZoomCommandProperty); }
            set { SetValue(ZoomCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ZoomCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ZoomCommandProperty =
            DependencyProperty.Register(nameof(ZoomCommand), typeof(ICommand), typeof(DesignerScrollBehaviour), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.PreviewMouseWheel += HandleWheel;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseWheel -= HandleWheel;
        }
        private void HandleWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                ZoomCommand?.Execute(e.Delta);
                e.Handled = true;
            }

            else if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                if (e.Delta > 0)
                    AssociatedObject.LineLeft();
                else if (e.Delta < 0)
                    AssociatedObject.LineRight();
                e.Handled = true;
            }
        }
    }
}
