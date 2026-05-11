
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _SaveProjectCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand SaveProjectCommand =>
            _SaveProjectCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => SaveProject());
    }
}
#nullable disable