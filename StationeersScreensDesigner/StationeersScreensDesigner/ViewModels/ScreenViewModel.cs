using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using CodeParagon.SourceGenerators.Attributes;
using CodeParagon.Wpf.MVVM;
using CodeParagon.Wpf.MVVM.Navigation;

using StationeersScreensDesigner.Models;

namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel : ViewModelBase
    {
        [ObservableProperty] private string _name;

        public ObservableCollection<LuaUIElement> CanvasElements { get; set; } = new();
        [ObservableProperty] private LuaUIElement _selectedElement;

        public ScreenViewModel(INavigationService navigation) : base(navigation)
        {
            _name = "New Screen";
        }
        public ScreenViewModel(INavigationService navigation,string name) : base(navigation)
        {
            _name = name;
        }
    }
}
