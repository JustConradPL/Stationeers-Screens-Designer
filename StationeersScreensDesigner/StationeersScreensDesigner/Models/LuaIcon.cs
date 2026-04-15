using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace StationeersScreensDesigner.Models
{
    public class LuaIcon : LuaUIElement
    {
        [PropertyMetadata("Icon Name", "Icon")]
        public string IconName
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(IconName));
            }
        }

        [PropertyMetadata("Color Index", "Icon")]
        public int ColorIndex
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(ColorIndex));
            }
        }

        [PropertyMetadata("Tint", "Icon")]
        public string Tint
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Tint));
            }
        }



        public LuaIcon() : base(string.Empty)
        {

        }
        public LuaIcon(string name) : base(name)
        {

        }
        public LuaIcon(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"Icon\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"props = {{name = {IconName}, color_index = {ColorIndex}}},");
            sb.AppendLine($"style = {{tint = \"{Tint}\"}},");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
