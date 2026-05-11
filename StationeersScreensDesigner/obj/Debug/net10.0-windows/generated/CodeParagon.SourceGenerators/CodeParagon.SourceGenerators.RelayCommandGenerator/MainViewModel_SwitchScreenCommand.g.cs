
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _SwitchScreenCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand SwitchScreenCommand =>
            _SwitchScreenCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => SwitchScreen(param));
    }
}
#nullable disable