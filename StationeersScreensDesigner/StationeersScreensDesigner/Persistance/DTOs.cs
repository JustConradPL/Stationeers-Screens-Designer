using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StationeersScreensDesigner.Persistance
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public List<ScreenDto> Screens { get; set; } = new();
    }

    public class ScreenDto
    {
        public string Name { get; set; }
        public List<ElementDto> Elements { get; set; } = new();
    }

    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(ButtonDto), "button")]
    [JsonDerivedType(typeof(CheckboxDto), "checkbox")]
    [JsonDerivedType(typeof(GaugeDto), "gauge")]
    [JsonDerivedType(typeof(IconDto), "icon")]
    [JsonDerivedType(typeof(LabelDto), "label")]
    [JsonDerivedType(typeof(LineDto), "line")]
    [JsonDerivedType(typeof(PanelDto), "panel")]
    [JsonDerivedType(typeof(ProgressBarDto), "progressbar")]
    [JsonDerivedType(typeof(RadioButtonDto), "radio")]
    [JsonDerivedType(typeof(ToggleDto), "toggle")]
    public class ElementDto
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int ZIndex { get; set; }
    }
}
