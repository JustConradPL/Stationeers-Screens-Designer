
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (!Equals(_isEditing, value))
                {
                    _isEditing = value;
                    OnPropertyChanged(nameof(IsEditing));
                }
            }
        }
    }
}