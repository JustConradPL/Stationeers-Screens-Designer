
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _GlobalMouseMoveCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand GlobalMouseMoveCommand =>
            _GlobalMouseMoveCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                param => GlobalMouseMove(param));
    }
}
#nullable disable