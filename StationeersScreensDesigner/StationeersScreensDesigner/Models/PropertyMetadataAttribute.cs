using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyMetadataAttribute(string displayName, string groupName = "General", bool isSlider = false) : Attribute
    {
        public string DisplayName { get; } = displayName;
        public string GroupName { get; } = groupName;
        public bool IsSlider { get; } = isSlider;
    }
}
