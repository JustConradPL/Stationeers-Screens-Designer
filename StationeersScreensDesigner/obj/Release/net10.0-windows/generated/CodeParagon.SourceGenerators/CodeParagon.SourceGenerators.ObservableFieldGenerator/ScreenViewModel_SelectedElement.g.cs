
namespace StationeersScreensDesigner.ViewModels
{
    public partial class ScreenViewModel
    {
        public StationeersScreensDesigner.Models.LuaUIElement SelectedElement
        {
            get => _selectedElement;
            set
            {
                if (!Equals(_selectedElement, value))
                {
                    _selectedElement = value;
                    OnPropertyChanged(nameof(SelectedElement));
                }
            }
        }
    }
}