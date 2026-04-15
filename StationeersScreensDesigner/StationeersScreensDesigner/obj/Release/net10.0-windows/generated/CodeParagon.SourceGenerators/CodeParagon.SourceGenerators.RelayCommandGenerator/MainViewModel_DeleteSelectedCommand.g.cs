
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _DeleteSelectedCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand DeleteSelectedCommand =>
            _DeleteSelectedCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => DeleteSelected());
    }
}
#nullable disable