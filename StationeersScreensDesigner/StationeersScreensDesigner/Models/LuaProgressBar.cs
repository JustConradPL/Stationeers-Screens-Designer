using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaProgressBar : LuaUIElement
    {
        [PropertyMetadata("Value", "Progress Bar")]
        public double Value
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("MaxValue", "Progress Bar")]
        public double MaxValue
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("MinValue", "Progress Bar")]
        public double MinValue
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Indeterminate", "Progress Bar")]
        public bool Indeterminate
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Background", "Progress Bar")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("FillColor", "Progress Bar")]
        public string FillColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        public (double, string)[] ColorStops
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Speed", "Progress Bar")]
        public double Speed
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        public LuaProgressBar() : base(string.Empty)
        {

        }
        public LuaProgressBar(string name) : base(name)
        {

        }
        public LuaProgressBar(string name, string id) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"progress\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"props = {{value = {Value}, min = {MinValue}, max = {MaxValue}, indeterminate = {Indeterminate}}},");
            sb.AppendLine($"style = {{bg = \"{Background}\", fill = \"{FillColor}\", speed = {Speed}}},");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
