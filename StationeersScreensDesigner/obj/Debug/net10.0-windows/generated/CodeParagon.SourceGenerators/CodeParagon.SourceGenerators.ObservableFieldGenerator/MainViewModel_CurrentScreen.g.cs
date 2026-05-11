
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        public StationeersScreensDesigner.ViewModels.ScreenViewModel CurrentScreen
        {
            get => _currentScreen;
            set
            {
                if (!Equals(_currentScreen, value))
                {
                    _currentScreen = value;
                    OnPropertyChanged(nameof(CurrentScreen));
                }
            }
        }
    }
}