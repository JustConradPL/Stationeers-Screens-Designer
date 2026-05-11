
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public double TargetHeight
        {
            get => _targetHeight;
            set
            {
                if (!Equals(_targetHeight, value))
                {
                    _targetHeight = value;
                    OnPropertyChanged(nameof(TargetHeight));
                }
            }
        }
    }
}