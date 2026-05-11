
namespace StationeersScreensDesigner.ViewModels
{
    public partial class MainViewModel
    {
        public StationeersScreensDesigner.Models.LuaUIElement DraggedElement
        {
            get => _draggedElement;
            set
            {
                if (!Equals(_draggedElement, value))
                {
                    _draggedElement = value;
                    OnPropertyChanged(nameof(DraggedElement));
                }
            }
        }
    }
}