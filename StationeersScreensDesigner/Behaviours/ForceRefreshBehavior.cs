using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Xaml.Behaviors;

using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Behaviours
{
    public class ForceRefreshBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            if (AssociatedObject.DataContext is PropertyViewModel inpc)
            {
                inpc.FirstTarget.PropertyChanged += OnVmPropertyChanged;
            }
        }
        protected override void OnDetaching()
        {
            if (AssociatedObject.DataContext is PropertyViewModel inpc)
            {
                inpc.FirstTarget.PropertyChanged -= OnVmPropertyChanged;
            }
        }
        private void OnVmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Unit") return;
            var binding = BindingOperations.GetBindingExpression(AssociatedObject, TextBox.TextProperty);
            binding?.UpdateTarget();
        }
    }
}
