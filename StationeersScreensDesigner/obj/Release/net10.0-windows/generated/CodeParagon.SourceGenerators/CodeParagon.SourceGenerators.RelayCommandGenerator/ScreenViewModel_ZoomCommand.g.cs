
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _ZoomCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand ZoomCommand =>
            _ZoomCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => Zoom(param));
    }
}
#nullable disable