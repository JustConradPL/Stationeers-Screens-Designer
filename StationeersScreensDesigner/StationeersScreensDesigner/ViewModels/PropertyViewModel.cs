using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using CodeParagon.Wpf.MVVM;

using StationeersScreensDesigner.Models;

namespace StationeersScreensDesigner.ViewModels
{
    public class PropertyViewModel : ViewModelBase
    {
        private readonly List<LuaUIElement> _targets;
        private readonly PropertyInfo _info;

        public string DisplayName { get; }
        public string GroupName { get; }
        public bool IsSlider { get; }
        public Type PropertyType
        {
            get
            {
                return _info.PropertyType;
            }
        }
        public object Value
        {
            get => _info.GetValue(_targets.First());
            set
            {
                try
                {
                    object convertedValue = value;

                    if (value is string strValue && _info.PropertyType != typeof(string))
                    {
                        convertedValue = Convert.ChangeType(strValue, _info.PropertyType);
                    }

                    foreach (var target in _targets)
                    {
                        _info.SetValue(target, convertedValue);
                    }

                    OnPropertyChanged();
                }
                catch
                {
                    OnPropertyChanged();
                }
            }
        }

        public PropertyViewModel(List<LuaUIElement> targets, PropertyInfo info, PropertyMetadataAttribute meta) : base(null)
        {
            _targets = targets;
            _info = info;
            DisplayName = meta.DisplayName;
            GroupName = meta.GroupName;
            IsSlider = meta.IsSlider;
        }
    }
}
