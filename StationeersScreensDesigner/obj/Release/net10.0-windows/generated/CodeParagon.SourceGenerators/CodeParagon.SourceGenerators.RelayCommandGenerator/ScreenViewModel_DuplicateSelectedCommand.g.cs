
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _DuplicateSelectedCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand DuplicateSelectedCommand =>
            _DuplicateSelectedCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => DuplicateSelected());
    }
}
#nullable disable