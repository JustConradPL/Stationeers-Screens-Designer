
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _StartDragCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand StartDragCommand =>
            _StartDragCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => StartDrag(param));
    }
}
#nullable disable