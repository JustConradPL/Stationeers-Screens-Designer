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
        [ObservableProperty] private double _targetWidth = 860;
        [ObservableProperty] private double _targetHeight = 585;
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

        public string GeneratedCode => LuaCodeGenerator.GenerateScreenScript(Name, CanvasElements);



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

            CanvasElements.Add(element);
            OnPropertyChanged(nameof(CanvasElements));
            CurrentVisualID++;
        }
    }
}
