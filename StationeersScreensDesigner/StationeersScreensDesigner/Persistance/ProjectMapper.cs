using System;
using System.Collections.Generic;
using System.Text;

using Riok.Mapperly.Abstractions;

using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Persistance
{
    [Mapper]
    public partial class ProjectMapper
    {
        // --- Project Level ---
        public ProjectDto ProjectToDto(string name, List<ScreenViewModel> screens)
        {
            List<ScreenDto> screenDtos = new();
            screens.ForEach(screen => screenDtos.Add(ScreenToDto(screen)));

            return new ProjectDto()
            {
                ProjectName = name,
                Screens = screenDtos,
            };
        }
        public ScreenDto ScreenToDto(ScreenViewModel vm)
        {
            var dto = new ScreenDto()
            {
                Elements = ElementsToDto(vm.CanvasElements),
                Name = vm.Name,
            };
            return dto;
        }
        public List<ElementDto> ElementsToDto(IEnumerable<LuaUIElement> elements)
        {
            var dtoList = new List<ElementDto>();

            foreach (var item in elements)
            {
                dtoList.Add(ElementToDto(item));
            }

            return dtoList;
        }

        public ScreenViewModel MapToScreenVm(ScreenDto dto)
        {
            // Manual rehydration because of the constructor
            var vm = new ScreenViewModel(null, dto.Name);
            if (dto.Elements != null)
            {
                foreach (var elementDto in dto.Elements)
                {
                    vm.CanvasElements.Add(ElementToViewModel(elementDto));
                }
            }
            return vm;
        }

        // --- Element Polymorphism (Save) ---
        // Mapperly will automatically call these when it processes the 'Elements' list in ScreenToDto
        [MapDerivedType(typeof(LuaButton), typeof(ButtonDto))]
        [MapDerivedType(typeof(LuaCheckbox), typeof(CheckboxDto))]
        [MapDerivedType(typeof(LuaGauge), typeof(GaugeDto))]
        [MapDerivedType(typeof(LuaIcon), typeof(IconDto))]
        [MapDerivedType(typeof(LuaLabel), typeof(LabelDto))]
        [MapDerivedType(typeof(LuaLine), typeof(LineDto))]
        [MapDerivedType(typeof(LuaPanel), typeof(PanelDto))]
        [MapDerivedType(typeof(LuaProgressBar), typeof(ProgressBarDto))]
        [MapDerivedType(typeof(LuaRadioButton), typeof(RadioButtonDto))]
        [MapDerivedType(typeof(LuaToggle), typeof(ToggleDto))]
        public partial ElementDto ElementToDto(LuaUIElement model);

        // --- Element Polymorphism (Load) ---
        [MapDerivedType(typeof(ButtonDto), typeof(LuaButton))]
        [MapDerivedType(typeof(CheckboxDto), typeof(LuaCheckbox))]
        [MapDerivedType(typeof(GaugeDto), typeof(LuaGauge))]
        [MapDerivedType(typeof(IconDto), typeof(LuaIcon))]
        [MapDerivedType(typeof(LabelDto), typeof(LuaLabel))]
        [MapDerivedType(typeof(LineDto), typeof(LuaLine))]
        [MapDerivedType(typeof(PanelDto), typeof(LuaPanel))]
        [MapDerivedType(typeof(ProgressBarDto), typeof(LuaProgressBar))]
        [MapDerivedType(typeof(RadioButtonDto), typeof(LuaRadioButton))]
        [MapDerivedType(typeof(ToggleDto), typeof(LuaToggle))]
        public partial LuaUIElement ElementToViewModel(ElementDto dto);

        // --- Special Fix for Label ---
        // In your code LuaLabel uses 'Content', but DTO likely uses 'Content' or 'Text'.
        // If they don't match, add this:
        // [MapProperty(nameof(LuaLabel.Content), nameof(LabelDto.Content))]
        // protected partial LabelDto MapLabel(LuaLabel label);

    }
}
