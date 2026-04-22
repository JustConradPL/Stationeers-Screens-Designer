
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public double ZoomLevel
        {
            get => _zoomLevel;
            set
            {
                if (!Equals(_zoomLevel, value))
                {
                    _zoomLevel = value;
                    OnPropertyChanged(nameof(ZoomLevel));
                }
            }
        }
    }
}