
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _AddElementCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand AddElementCommand =>
            _AddElementCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => AddElement());
    }
}
#nullable disable