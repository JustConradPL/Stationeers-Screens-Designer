using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Selectors
{
    public class PropertyEditorSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate SliderTemplate { get; set; }
        public DataTemplate ColorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PropertyViewModel prop)
            {
                // Logic to choose template
                if (prop.IsSlider) return SliderTemplate;

                // You can also check by type
                if (prop.PropertyType == typeof(System.Windows.Media.Color))
                    return ColorTemplate;
            }

            return DefaultTemplate;
        }
    }
}
