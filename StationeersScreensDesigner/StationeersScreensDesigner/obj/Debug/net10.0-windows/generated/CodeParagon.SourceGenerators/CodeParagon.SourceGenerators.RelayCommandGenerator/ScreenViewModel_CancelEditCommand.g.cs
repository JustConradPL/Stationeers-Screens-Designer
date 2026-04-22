
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _CancelEditCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand CancelEditCommand =>
            _CancelEditCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => CancelEdit());
    }
}
#nullable disable