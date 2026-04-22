using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public abstract class LuaUIElement(string name, string? id = null) : INotifyPropertyChanged
    {
        [PropertyMetadata("Name")]
        public string Name
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        } = name;
        [PropertyMetadata("ID")]

        public string? ID
        {
            get; set
            {
                field = value?.Replace(" ","_");
                OnPropertyChanged();
            }
        } = id;

        public bool IsSelected
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("ZIndex")]
        public int ZIndex
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("X", "Position")]
        public double X
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Y", "Position")]
        public double Y
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Width", "Size")]
        public double Width
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Height", "Size")]

        public double Height
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public abstract string ToLuaCode();
    }
}
