
#nullable enable
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        private CodeParagon.Wpf.MVVM.RelayCommand _CommitEditCommand;
        public CodeParagon.Wpf.MVVM.RelayCommand CommitEditCommand =>
            _CommitEditCommand ??= new CodeParagon.Wpf.MVVM.RelayCommand(
                null,
                _ => CommitEdit());
    }
}
#nullable disable