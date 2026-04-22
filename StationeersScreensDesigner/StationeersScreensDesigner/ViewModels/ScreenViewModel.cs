using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using CodeParagon.SourceGenerators.Attributes;
using CodeParagon.Wpf.MVVM;
using CodeParagon.Wpf.MVVM.Navigation;

using StationeersScreensDesigner.Helpers;
using StationeersScreensDesigner.Models;

namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel : ViewModelBase
    {
        [ObservableProperty] private string _name;
        [ObservableProperty] private bool _isEditing;
        [ObservableProperty] private double _zoomLevel = 1.0;
        private string _tempName;
        public ObservableCollection<LuaUIElement> CanvasElements { get; set; } = new();
        [ObservableProperty] private LuaUIElement _selectedElement;
        public int CurrentVisualID
        {
            get
            {
                return field++;
            }
            private set;
        } = 1;
        public string GeneratedCode
        {
            get
            {
                StringBuilder sb = new();
                sb.AppendLine();
                sb.AppendLine($"local {_name.Replace(" ", "_")}UI = ss.ui.surface(\"{_name}\")\r\n" +
                    $"ss.ui.activate(\"{_name}\")\r\n" +
                    $"local size = {_name.Replace(" ", "_")}UI:size()\r\n" +
                    "local W, H = size.w, size.h\n");

                foreach (var element in CanvasElements.OrderBy((el) => el.ZIndex))
                {
                    sb.AppendLine(element.ToLuaCode()
                        .Replace("ui:", $"{_name.Replace(" ", "_")}UI:", StringComparison.OrdinalIgnoreCase))
                        .Replace("ui_", $"{_name.Replace(" ", "_")}UI");
                }

                sb.AppendLine("-- Initialize and run the screen");
                sb.AppendLine($"{_name.Replace(" ", "_")}UI:commit()");

                return sb.ToString();
            }
        }


        public ScreenViewModel(INavigationService navigation) : base(navigation)
        {
            _name = "New Screen";
        }
        public ScreenViewModel(INavigationService navigation, string name) : base(navigation)
        {
            _name = name;
        }

        [RelayCommand]
        public void DuplicateSelected()
        {
            int count = CanvasElements.Count;
            for (int i = 0; i < count; i++)
            {
                if (!CanvasElements[i].IsSelected) continue;
                var newEl = CanvasElements[i].Clone();
                CanvasElements[i].IsSelected = false;
                newEl.ID = $"VisualElement{CurrentVisualID}";

                newEl.X += 30;
                newEl.Y += 30;
                AddElement(newEl);
            }

        }

        [RelayCommand]
        public void Zoom(object delta)
        {
            var lDelta = delta as int?;
            if (!lDelta.HasValue) return;


            if (lDelta == 0) return;

            double step = 0.1;
            if (lDelta > 0) _zoomLevel = Math.Min(3.0, _zoomLevel + step); // Max 300%
            else _zoomLevel = Math.Max(0.2, _zoomLevel - step);          // Min 20%

            OnPropertyChanged(nameof(ZoomLevel));
        }

        [RelayCommand]
        public void DeleteSelected()
        {
            var itemsToDelete = CanvasElements.Where(x => x.IsSelected).ToList();

            foreach (var item in itemsToDelete)
            {
                CanvasElements.Remove(item);
            }
            OnPropertyChanged(nameof(GeneratedCode));
        }

        [RelayCommand]
        public void StartEdit()
        {
            _tempName = Name;
            IsEditing = true;
        }

        [RelayCommand]
        public void CommitEdit()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = _tempName;
            }

            IsEditing = false;
        }

        [RelayCommand]
        public void CancelEdit()
        {
            Name = _tempName;
            IsEditing = false;
        }

        public void AddElement(LuaUIElement element)
        {
            element.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(GeneratedCode));
            };

            CanvasElements.Add(element);
            OnPropertyChanged(nameof(GeneratedCode));
            OnPropertyChanged(nameof(CanvasElements));
        }
    }
}
