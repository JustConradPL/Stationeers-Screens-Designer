
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _LoadProjectCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand LoadProjectCommand =>
            _LoadProjectCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => LoadProject());
    }
}
#nullable disable