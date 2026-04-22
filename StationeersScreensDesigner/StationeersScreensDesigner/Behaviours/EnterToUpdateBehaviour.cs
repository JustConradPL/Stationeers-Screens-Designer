using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

using Microsoft.Xaml.Behaviors;

namespace StationeersScreensDesigner.Behaviours
{
    public class EnterToUpdateBehaviour : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.KeyDown += HandleKeyDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.KeyDown -= HandleKeyDown;
        }

        private void HandleKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {

                var tb = sender as TextBox;
                if (tb == null) return;

                BindingOperations.GetBindingExpression(tb, TextBox.TextProperty)?.UpdateSource();
            }
        }
    }
}
