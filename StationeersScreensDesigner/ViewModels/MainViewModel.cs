using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

using CodeParagon.SourceGenerators.Attributes;
using CodeParagon.Wpf.MVVM;
using CodeParagon.Wpf.MVVM.Navigation;

using StationeersScreensDesigner.Controls;
using StationeersScreensDesigner.Helpers;
using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.Persistance;

namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        #region GetCursorPos PInvoke
        [StructLayout(LayoutKind.Sequential)]
        public struct Win32Point
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out Win32Point lpPoint);
        #endregion
        public ObservableCollection<LuaUIElement> AvailableElements { get; init; }
        [ObservableProperty] private ScreenViewModel? _hoveredScreen;
        public ObservableCollection<ScreenViewModel> Screens { get; set; } = new();
        public MainViewModel(INavigationService navigation, IEnumerable<LuaUIElement> availableLuaElements) : base(navigation)
        {
            AvailableElements = new(availableLuaElements);
            AddScreen();
        }

        [ObservableProperty]
        private ScreenViewModel _currentScreen;
        public string LineNumbers
        {
            get
            {
                int lineCount = GeneratedLuaCode.Count(c => c == '\n') + 1;
                return string.Join(Environment.NewLine, Enumerable.Range(1, lineCount));
            }
        }
        public string GeneratedLuaCode => LuaCodeGenerator.GenerateMainScript(Screens);

        public List<PropertyGroupViewModel>? ActiveProperties => GetActiveProperties();

        private List<PropertyGroupViewModel>? GetActiveProperties()
        {
            if (CurrentScreen == null) return null;

            var selected = CurrentScreen.CanvasElements.Where(x => x.IsSelected).ToList();
            if (!selected.Any()) return null;

            Type firstType = selected[0].GetType();
            if (!selected.All(x => x.GetType() == firstType)) return null;

            var propertyWrappers = firstType.GetProperties()
                .Select(p => new
                {
                    Prop = p,
                    Meta = p.GetCustomAttribute<PropertyMetadataAttribute>()
                })
                .Where(x => x.Meta != null)
                .Select(x =>
                {
                    var p = new PropertyViewModel(selected, x.Prop, x.Meta);
                    return p;
                });

            return propertyWrappers
                .GroupBy(x => x.GroupName)
                .Select(g => new PropertyGroupViewModel(g.Key, g.ToList()))
                .ToList();
        }
        private LuaUIElement? _draggedElement;
        private FollowerAdorner _follower;


        private void EnableFollower(Window root)
        {
            var layer = AdornerLayer.GetAdornerLayer((Visual)root.Content);
            if (_follower == null)
            {
                Stylus.SetIsFlicksEnabled(root, false);
                Stylus.SetIsPressAndHoldEnabled(root, false);
                Stylus.SetIsTapFeedbackEnabled(root, false);
                Stylus.SetIsTouchFeedbackEnabled(root, false);
                var visual = (UIElement)root.Resources["MyFollowerVisual"];
                _follower = new FollowerAdorner((UIElement)root.Content, visual);
            }

            layer.Add(_follower);
            _follower.CaptureMouse();
            CompositionTarget.Rendering += UpdateFollowerPosition;
        }

        private void DisableFollower(Window root)
        {
            var layer = AdornerLayer.GetAdornerLayer((Visual)root.Content);
            if (_follower != null)
            {
                layer.Remove(_follower);
            }
            CompositionTarget.Rendering -= UpdateFollowerPosition;
        }

        [RelayCommand]
        public void SaveProject()
        {
            FileOperator.SaveAs("MainProject", Screens.ToList());
        }
        [RelayCommand]
        public void LoadProject()
        {
            var screens = FileOperator.Load();
            if (screens == null || screens.Count == 0) return;//return if operation cancelled

            Screens.Clear();
            foreach (var screen in screens)
            {
                Screens.Add(screen);
            }
            CurrentScreen = Screens.FirstOrDefault();
        }

        [RelayCommand]
        public void CopyCode()
        {
            OnPropertyChanged(nameof(GeneratedLuaCode));
            Clipboard.SetText(GeneratedLuaCode);
        }

        [RelayCommand]
        public void AddScreen()
        {
            var screen = new ScreenViewModel(_navigationService, $"Screen {Screens.Count + 1}");
            Screens.Add(screen);
            CurrentScreen = screen;

        }

        [RelayCommand]
        public void StartDrag(object e)
        {
            var parameters = e as object[];
            if (parameters is null) return;

            var element = parameters[0] as LuaUIElement;
            var sender = parameters[1] as FrameworkElement;

            if (sender is null || element is null) return;

            _draggedElement = element.Clone();

            EnableFollower(App.Current.MainWindow);

            var result = DragDrop.DoDragDrop(sender, element, DragDropEffects.Copy);
            if (result == DragDropEffects.None) CancelDrag();
        }

        [RelayCommand]
        private void StopDrag(object dropArgs)
        {
            if (dropArgs is not DragEventArgs e) return;
            if (_draggedElement is null) return;

            var canvas = e.Source as IInputElement;
            var mousePoint = e.GetPosition(canvas);

            _draggedElement.X = mousePoint.X;
            _draggedElement.Y = mousePoint.Y;


            _draggedElement.ID = $"VisualElement{CurrentScreen.CurrentVisualID}";
            _currentScreen.AddElement(_draggedElement);

            DisableFollower(App.Current.MainWindow);
            _draggedElement = null;
            OnPropertyChanged(nameof(LineNumbers));

        }
        [RelayCommand]
        private void DeselectElements()
        {
            foreach (var item in CurrentScreen.CanvasElements)
            {
                item.IsSelected = false;
            }
            RefreshPropertyGrid();
        }

        [RelayCommand]
        public void ChangeScreenName()
        {
            if (HoveredScreen == null || HoveredScreen.IsEditing) return;
            HoveredScreen.StartEdit();
        }
        [RelayCommand]
        public void DeleteSelected()
        {
            var itemsToDelete = CurrentScreen.CanvasElements.Where(x => x.IsSelected).ToList();

            if (itemsToDelete.Count == 0 && HoveredScreen != null && Screens.Count > 1)
            {
                var hovered = HoveredScreen;
                var result = MessageBox.Show(
                $"Are you sure you want to delete screen '{HoveredScreen.Name}'?",
                "Delete Screen",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (CurrentScreen == hovered)
                        CurrentScreen = Screens
                            .FirstOrDefault(screen => !ReferenceEquals(screen, hovered));
                    Screens.Remove(hovered);
                }
            }
            else
            {
                foreach (var item in itemsToDelete)
                {
                    CurrentScreen.CanvasElements.Remove(item);
                }
            }
            OnPropertyChanged(nameof(ActiveProperties));
            OnPropertyChanged(nameof(LineNumbers));
        }

        public void RefreshPropertyGrid()
        {
            OnPropertyChanged(nameof(ActiveProperties));
        }
        private void CancelDrag()
        {
            if (_draggedElement is null) return;


            DisableFollower(App.Current.MainWindow);
            _draggedElement = null;
        }
        private void UpdateFollowerPosition(object? sender, EventArgs e)
        {

            GetCursorPos(out Win32Point mousePos);

            Point localPoint = App.Current.MainWindow.PointFromScreen(new Point(mousePos.X, mousePos.Y));
            _follower.UpdatePosition(new Point(localPoint.X - 30, localPoint.Y - 30));
        }
    }
}
