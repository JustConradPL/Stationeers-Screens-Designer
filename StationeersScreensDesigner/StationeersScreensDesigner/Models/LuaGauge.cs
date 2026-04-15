using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StationeersScreensDesigner.Models
{
    public class LuaGauge : LuaUIElement
    {
        [PropertyMetadata("Value", "Gauge")]
        public double Value
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        [PropertyMetadata("Min Value", "Gauge")]
        public double MinValue
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(MinValue));
            }
        }
        [PropertyMetadata("Max Value", "Gauge")]
        public double MaxValue
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(MaxValue));
            }
        }
        [PropertyMetadata("Warn", "Gauge")]
        public double Warn
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Warn));
            }
        }
        [PropertyMetadata("Danger", "Gauge")]
        public double Danger
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Danger));
            }
        }

        [PropertyMetadata("Invert", "Gauge")]
        public bool Invert
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Invert));
            }
        }
        [PropertyMetadata("Label", "Gauge")]
        public string Label
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Label));
            }
        }
        [PropertyMetadata("Unit", "Gauge")]
        public string Unit
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Unit));
            }
        }
        [PropertyMetadata("Background", "Gauge")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        [PropertyMetadata("Arc Border Color", "Gauge")]
        public string ArcBorderColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(ArcBorderColor));
            }
        }

        [PropertyMetadata("Needle Color", "Gauge")]
        public string NeedleColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(NeedleColor));
            }
        }
        [PropertyMetadata("Normal Color", "Gauge")]
        public string NormalColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(NormalColor));
            }
        }
        [PropertyMetadata("Warning Color", "Gauge")]
        public string WarnColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(WarnColor));
            }
        }

        [PropertyMetadata("Danger Color", "Gauge")]
        public string DangerColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(DangerColor));
            }
        }

        [PropertyMetadata("Value Color", "Gauge")]
        public string ValueTextColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(ValueTextColor));
            }
        }
        [PropertyMetadata("Label Color", "Gauge")]
        public string LabelTextColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(LabelTextColor));
            }
        }

        [PropertyMetadata("Arc Thickness", "Gauge")]
        public double ArcThickness
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(ArcThickness));
            }
        }

        [PropertyMetadata("Font Size", "Gauge")]
        public double FontSize
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public LuaGauge() : base(string.Empty)
        {

        }
        public LuaGauge(string name) : base(name)
        {

        }
        public LuaGauge(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"gauge\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($@"props = {{
        value = {Value},
        min = {MinValue},
        max = {MaxValue},
        warn = {Warn},
        danger = {Danger},
        label = ""{Label}"",
        unit = "" {Unit}"",
        invert = {Invert},
    }},
    style = {{
        bg = ""{Background}"",
        arc_thickness = {ArcThickness},
        font_size = {FontSize},
        value_color = ""{ValueTextColor}"",
        label_color = ""{LabelTextColor}"",
        needle_color = ""{NeedleColor}"",
        normal_color = ""{NormalColor}"",
        warn_color = ""{WarnColor}"",
        danger_color=""{DangerColor}"",
        arc_color=""{ArcBorderColor}"",
    }}");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
