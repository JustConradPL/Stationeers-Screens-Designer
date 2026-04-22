using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace StationeersScreensDesigner.Behaviours
{
    public class TextBoxRenameBehavior : Behavior<TextBox>
    {


        public ICommand CommitCommand
        {
            get { return (ICommand)GetValue(CommitCommandProperty); }
            set { SetValue(CommitCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommitCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommitCommandProperty =
            DependencyProperty.Register(nameof(CommitCommand), typeof(ICommand), typeof(TextBoxRenameBehavior));



        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CancelCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register(nameof(CancelCommand), typeof(ICommand), typeof(TextBoxRenameBehavior));


        

        protected override void OnAttached()
        {
            AssociatedObject.IsVisibleChanged += OnVisibleChanged;
            AssociatedObject.KeyDown += OnKeyDown;
            AssociatedObject.LostFocus += (s, e) => CommitCommand?.Execute(null);
        }

        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                AssociatedObject.Focus();
                AssociatedObject.SelectAll();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) CommitCommand?.Execute(null);
            if (e.Key == Key.Escape) CancelCommand?.Execute(null);
        }
    }
}
