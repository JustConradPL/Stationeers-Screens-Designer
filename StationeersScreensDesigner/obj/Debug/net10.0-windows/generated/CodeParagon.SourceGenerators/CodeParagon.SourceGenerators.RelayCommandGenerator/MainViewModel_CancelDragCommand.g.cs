
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _CancelDragCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand CancelDragCommand =>
            _CancelDragCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => CancelDrag());
    }
}
#nullable disable