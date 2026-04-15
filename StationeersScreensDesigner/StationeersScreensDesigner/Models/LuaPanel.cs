using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaPanel : LuaUIElement
    {
        [PropertyMetadata("Background","Panel")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        public LuaPanel() : base(string.Empty)
        {

        }
        public LuaPanel(string name) : base(name)
        {

        }
        public LuaPanel(string name, string id) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"panel\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($"style = {{bg = \"{Background}\"}},");
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
