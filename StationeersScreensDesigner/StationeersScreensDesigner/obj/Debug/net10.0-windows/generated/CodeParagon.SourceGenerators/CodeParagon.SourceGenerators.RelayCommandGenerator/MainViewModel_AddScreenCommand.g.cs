
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _AddScreenCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand AddScreenCommand =>
            _AddScreenCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => AddScreen());
    }
}
#nullable disable