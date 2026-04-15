using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{

    public class LuaCheckbox : LuaUIElement
    {
        [PropertyMetadata("Text", "Checkbox")]
        public string Text
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        [PropertyMetadata("Background", "Checkbox")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        [PropertyMetadata("Check Color", "Checkbox")]
        public string CheckColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(CheckColor));
            }
        }

        [PropertyMetadata("Text Color", "Checkbox")]
        public string TextColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
        [PropertyMetadata("Font Size", "Checkbox")]
        public double FontSize
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
        [PropertyMetadata("Checked", "Checkbox")]
        public bool Checked
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Checked));
            }
        }
        public LuaCheckbox() : base(string.Empty)
        {
            
        }
        public LuaCheckbox(string name) : base(name)
        {
            
        }
        public LuaCheckbox(string name,string id) : base(name,id)
        {
            
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            sb.AppendLine($"local {ID}_previousValue");
            sb.AppendLine($"local {ID}_OnToggle = Event.new()");
            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"checkbox\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($@" props = {{ text = ""{Text}"", checked = ""{Checked}"" }},");
            sb.AppendLine($@"style = {{check_color = ""{CheckColor}"", bg= ""{Background}"", font_size = {FontSize}, text = ""{TextColor}""}},");
            sb.AppendLine($$"""
                on_click = function(playerName)
                    {{ID}}_previousValue = not {{ID}}_previousValue
                    ui:get("{{ID}}"):set_props({checked = {{ID}}_previousValue})
                    {{ID}}_OnToggle:Fire({{ID}}_previousValue)
                end
                """);
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
