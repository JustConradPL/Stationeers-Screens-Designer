
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _CopyCodeCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand CopyCodeCommand =>
            _CopyCodeCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => CopyCode());
    }
}
#nullable disable