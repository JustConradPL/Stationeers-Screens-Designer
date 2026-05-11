
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public double TargetWidth
        {
            get => _targetWidth;
            set
            {
                if (!Equals(_targetWidth, value))
                {
                    _targetWidth = value;
                    OnPropertyChanged(nameof(TargetWidth));
                }
            }
        }
    }
}