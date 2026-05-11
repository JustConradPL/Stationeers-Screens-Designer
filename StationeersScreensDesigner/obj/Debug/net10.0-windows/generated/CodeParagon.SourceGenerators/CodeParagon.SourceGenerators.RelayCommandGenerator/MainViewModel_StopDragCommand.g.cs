
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _StopDragCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand StopDragCommand =>
            _StopDragCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => StopDrag(param));
    }
}
#nullable disable