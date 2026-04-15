using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using static System.Net.Mime.MediaTypeNames;

namespace StationeersScreensDesigner.Models
{
    public class LuaButton : LuaUIElement
    {
        [PropertyMetadata("Text", "Button")]
        public string Text
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        [PropertyMetadata("Background", "Button")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Background));
            }
        }
        [PropertyMetadata("Text Color", "Button")]
        public string TextColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
        [PropertyMetadata("Font Size", "Button")]
        public double FontSize
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }

        public LuaButton() : base(string.Empty)
        {

        }
        public LuaButton(string name) : base(name)
        {

        }
        public LuaButton(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"line\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"props = {{text = \"{Text}\"}},");
            sb.AppendLine($"style = {{ bg = \"{Background}\", text = \"{TextColor}\", font_size = {FontSize} }},");
            sb.AppendLine("on_click = function(playerName)");
            sb.AppendLine("\t--Clicked logic");
            sb.AppendLine("end");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
