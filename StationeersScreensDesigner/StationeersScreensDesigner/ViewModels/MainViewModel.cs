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
        public ObservableCollection<LuaUIElement> CanvasElements { get; } = new();

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
                int lineCount = GeneratedLuaCode.Split('\n').Length;

                return string.Join(Environment.NewLine, Enumerable.Range(1, lineCount));
            }
        }
        public string GeneratedLuaCode
        {
            get
            {
                var sb = new StringBuilder();
                sb.AppendLine("-- STATIONEERS SCREEN SCRIPT --");
                //sb.AppendLine($"-- Project: {ProjectName}");
                sb.AppendLine();
                sb.AppendLine("local ui = ss.ui.surface(\"main\")\r\nss.ui.activate(\"main\")\r\nlocal size = ui:size()\r\n" +
                    "local W, H = size.w, size.h");
                sb.AppendLine("""
                    local Event = {}
                    Event.__index = Event

                    function Event.new()
                        local instance = { listeners = {} }
                        return setmetatable(instance, Event)
                    end

                    -- Add a function to the event
                    function Event:subscribe(func)
                        table.insert(self.listeners, func)
                    end

                    -- Run all subscribed functions
                    function Event:fire(...)
                        for _, func in ipairs(self.listeners) do
                            func(...)
                        end
                    end

                    """);
                if (CanvasElements.Any(element => element is LuaRadioButton))
                {
                    var tables = CanvasElements
                        .Where(elements => elements is LuaRadioButton e && e.GroupID > 0)
                        .GroupBy(element => ((LuaRadioButton)element).GroupID)
                        .Select(gp => gp.ToList())
                        .ToList();

                    foreach (var table in tables)
                    {
                        sb.AppendLine($"local RadioButtonGroup{((LuaRadioButton)table[0]).GroupID} = {{}}");
                    }

                    sb.AppendLine("""
                        local function updateCheckedById(objectTable, targetId)
                            for i, entry in ipairs(objectTable) do
                                -- Access the 'obj' and 'id' from the entry table
                                local handler = entry.obj
                                local ID = entry.id

                                if ID == targetId then
                                    handler:set_props({selected = true})
                                else
                                    handler:set_props({selected = false})
                                end
                            end
                            ui:commit()
                        end
                        """);
                }
                sb.AppendLine();

                foreach (var element in CanvasElements.OrderBy((el) => el.ZIndex))
                {
                    sb.AppendLine(element.ToLuaCode());
                }

                sb.AppendLine();
                sb.AppendLine("-- Initialize and run the screen");
                sb.AppendLine("ui:commit()");

                return sb.ToString();
            }
        }
        public List<PropertyGroupViewModel>? ActiveProperties
        {
            get
            {
                var selected = CanvasElements.Where(x => x.IsSelected).ToList();
                if (!selected.Any()) return null;

                Type firstType = selected[0].GetType();
                // Strict subtype check: only show properties if ALL selected items are the same type
                if (!selected.All(x => x.GetType() == firstType)) return null;

                // 1. Reflect and create individual wrappers
                var propertyWrappers = firstType.GetProperties()
                    .Select(p => new
                    {
                        Prop = p,
                        Meta = p.GetCustomAttribute<PropertyMetadataAttribute>()
                    })
                    .Where(x => x.Meta != null) // Only show properties with attribute
                    .Select(x => new PropertyViewModel(selected, x.Prop, x.Meta));

                // Group by the GroupName defined in the Attribute
                return propertyWrappers
                    .GroupBy(x => x.GroupName)
                    .Select(g => new PropertyGroupViewModel(g.Key, g.ToList()))
                    .ToList();
            }
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
        public void AddScreen()
        {
            var screen = new ScreenViewModel(_navigationService,$"Screen {Screens.Count + 1}");
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

            _draggedElement.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(GeneratedLuaCode));
                OnPropertyChanged(nameof(LineNumbers));
            };

            CanvasElements.Add(_draggedElement);

            DisableFollower(App.Current.MainWindow);
            _draggedElement = null;
            OnPropertyChanged(nameof(GeneratedLuaCode));
            OnPropertyChanged(nameof(LineNumbers));

        }
        [RelayCommand]
        private void DeselectElements()
        {
            foreach (var item in CanvasElements)
            {
                item.IsSelected = false;
            }
            RefreshPropertyGrid();
        }

        [RelayCommand]
        public void DeleteSelected()
        {
            var itemsToDelete = CanvasElements.Where(x => x.IsSelected).ToList();

            foreach (var item in itemsToDelete)
            {
                CanvasElements.Remove(item);
            }

            OnPropertyChanged(nameof(ActiveProperties));
            OnPropertyChanged(nameof(GeneratedLuaCode));
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
