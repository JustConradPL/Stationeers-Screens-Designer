using System;
using System.Collections.Generic;
using System.Text;

using CodeParagon.Wpf.MVVM;

namespace StationeersScreensDesigner.ViewModels
{
    public class PropertyGroupViewModel : ViewModelBase
    {
        public string GroupName { get; }
        public List<PropertyViewModel> Properties { get; }

        public PropertyGroupViewModel(string groupName, List<PropertyViewModel> properties) : base(null)
        {
            GroupName = groupName;
            Properties = properties;
        }
    }
}
