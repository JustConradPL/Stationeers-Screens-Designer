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
                        (_,convertedValue) = EvaluateMath(strValue);
                        convertedValue = Convert.ChangeType(convertedValue, _info.PropertyType);
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
        private (bool,double) EvaluateMath(string input)
        {
            try
            {
                var table = new System.Data.DataTable();
                var result = table.Compute(input, string.Empty);
                return (true,Convert.ToDouble(result));
            }
            catch
            {
                double.TryParse(input, out double val);
                return (false,val);
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
