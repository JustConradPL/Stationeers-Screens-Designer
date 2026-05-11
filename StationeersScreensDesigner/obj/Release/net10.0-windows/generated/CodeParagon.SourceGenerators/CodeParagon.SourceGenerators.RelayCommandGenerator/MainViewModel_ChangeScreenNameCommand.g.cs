
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _ChangeScreenNameCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand ChangeScreenNameCommand =>
            _ChangeScreenNameCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => ChangeScreenName());
    }
}
#nullable disable