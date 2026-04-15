
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        public System.Windows.Point MousePos
        {
            get => _mousePos;
            set
            {
                if (!Equals(_mousePos, value))
                {
                    _mousePos = value;
                    OnPropertyChanged(nameof(MousePos));
                }
            }
        }
    }
}