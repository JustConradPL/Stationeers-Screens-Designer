using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StationeersScreensDesigner.Models
{
    public class LuaLine : LuaUIElement
    {
        [PropertyMetadata("X1", "Line")]
        public double X1
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(X1));
            }
        }
        [PropertyMetadata("X2", "Line")]
        public double X2
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(X2));
            }
        }
        [PropertyMetadata("Y1", "Line")]
        public double Y1
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Y1));
            }
        }
        [PropertyMetadata("Y2", "Line")]
        public double Y2
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Y2));
            }
        }

        [PropertyMetadata("Color", "Line")]
        public string Color
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        [PropertyMetadata("Thickness", "Line")]
        public double Thickness
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Thickness));
            }
        }

        public LuaLine() : base(string.Empty)
        {

        }
        public LuaLine(string name) : base(name)
        {

        }
        public LuaLine(string name, string? id = null) : base(name, id)
        {

        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"line\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"props = {{ x1 = \"{X1}\", y1 = \"{Y2}\", x2 = \"{X2}\", y2 = \"{Y2}\" }},");
            sb.AppendLine($"style = {{color = \"{Color}\",thickness = \"{Thickness}\"}},");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
