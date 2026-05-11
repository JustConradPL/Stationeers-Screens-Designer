
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _OnDropCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand OnDropCommand =>
            _OnDropCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => OnDrop(param));
    }
}
#nullable disable