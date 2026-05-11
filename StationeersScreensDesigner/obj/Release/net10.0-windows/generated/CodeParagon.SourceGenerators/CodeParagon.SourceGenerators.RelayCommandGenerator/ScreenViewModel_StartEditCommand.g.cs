
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _StartEditCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand StartEditCommand =>
            _StartEditCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => StartEdit());
    }
}
#nullable disable