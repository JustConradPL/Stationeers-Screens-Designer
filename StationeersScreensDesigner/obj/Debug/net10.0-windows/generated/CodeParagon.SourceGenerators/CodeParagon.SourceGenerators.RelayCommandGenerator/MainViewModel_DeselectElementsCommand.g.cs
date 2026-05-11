
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _DeselectElementsCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand DeselectElementsCommand =>
            _DeselectElementsCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => DeselectElements());
    }
}
#nullable disable