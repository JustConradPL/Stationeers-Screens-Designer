
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        public StationeersScreensDesigner.ViewModels.ScreenViewModel? HoveredScreen
        {
            get => _hoveredScreen;
            set
            {
                if (!Equals(_hoveredScreen, value))
                {
                    _hoveredScreen = value;
                    OnPropertyChanged(nameof(HoveredScreen));
                }
            }
        }
    }
}