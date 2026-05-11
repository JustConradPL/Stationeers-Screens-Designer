
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public string Name
        {
            get => _name;
            set
            {
                if (!Equals(_name, value))
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
    }
}