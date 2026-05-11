using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using StationeersScreensDesigner.Controls;

namespace StationeersScreensDesigner.Helpers
{
    public class DragDropFollower
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Win32Point lpPoint);

        private FollowerAdorner? _follower;

        public void EnableFollower(Window root)
        {
            if (_follower != null) return;

            var layer = AdornerLayer.GetAdornerLayer((Visual)root.Content);
            if (layer == null) return;

            Stylus.SetIsFlicksEnabled(root, false);
            Stylus.SetIsPressAndHoldEnabled(root, false);
            Stylus.SetIsTapFeedbackEnabled(root, false);
            Stylus.SetIsTouchFeedbackEnabled(root, false);

            var visual = (UIElement)root.Resources["MyFollowerVisual"];
            _follower = new FollowerAdorner((UIElement)root.Content, visual);

            layer.Add(_follower);
            _follower.CaptureMouse();
            CompositionTarget.Rendering += UpdateFollowerPosition;
        }

        public void DisableFollower(Window root)
        {
            if (_follower == null) return;

            var layer = AdornerLayer.GetAdornerLayer((Visual)root.Content);
            layer?.Remove(_follower);

            CompositionTarget.Rendering -= UpdateFollowerPosition;
            _follower = null;
        }

        private void UpdateFollowerPosition(object? sender, EventArgs e)
        {
            if (_follower == null) return;

            GetCursorPos(out Win32Point mousePos);
            Point localPoint = Application.Current.MainWindow.PointFromScreen(new Point(mousePos.X, mousePos.Y));
            _follower.UpdatePosition(new Point(localPoint.X - 30, localPoint.Y - 30));
        }
    }
}