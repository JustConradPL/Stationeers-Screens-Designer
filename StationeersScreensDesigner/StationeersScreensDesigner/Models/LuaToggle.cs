using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaToggle : LuaUIElement
    {
        public LuaToggle() : base(string.Empty)
        {

        }
        public LuaToggle(string name) : base(name)
        {

        }
        public LuaToggle(string name, string id) : base(name, id)
        {
        }

        [PropertyMetadata("Value", "Toggle")]
        public bool Value
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        [PropertyMetadata("On Color", "Toggle")]
        public string OnColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(OnColor));
            }
        }
        [PropertyMetadata("Off Color", "Toggle")]
        public string OffColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(OffColor));
            }
        }
        [PropertyMetadata("Knob Color", "Toggle")]
        public string KnobColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(KnobColor));
            }
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();
            sb.AppendLine($"local {ID}_previousValue");
            sb.AppendLine($"local {ID}_OnToggle = Event.new()");
            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"toggle\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"props = {{value = \"{Value}\"}},");
            sb.AppendLine($"style = {{on_color = \"{OnColor}\", off_color = \"{OffColor}\", knob = \"{KnobColor}\"}},");
            sb.AppendLine($$"""
                on_click = function(playerName)
                    {{ID}}_previousValue = not {{ID}}_previousValue
                    ui:get("{{ID}}"):set_props({value = {{ID}}_previousValue})
                    {{ID}}_OnToggle:fire({{ID}}_previousValue)
                end
                """);
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
