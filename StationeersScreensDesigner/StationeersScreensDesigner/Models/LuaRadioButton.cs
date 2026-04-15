using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaRadioButton : LuaUIElement
    {
        [PropertyMetadata("Text", "Radio Button")]
        public string Text
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        [PropertyMetadata("Background", "Radio Button")]
        public string Background
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Background));
            }
        }

        [PropertyMetadata("Check Color", "Radio Button")]
        public string CheckColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(CheckColor));
            }
        }

        [PropertyMetadata("Text Color", "Radio Button")]
        public string TextColor
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(TextColor));
            }
        }
        [PropertyMetadata("Font Size", "Radio Button")]
        public double FontSize
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }
        [PropertyMetadata("GroupID", "Radio Button")]
        public int GroupID
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(GroupID));
            }
        }

        [PropertyMetadata("Checked", "Radio Button")]
        public bool Checked
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Checked));
            }
        }
        public LuaRadioButton() : base(string.Empty)
        {

        }
        public LuaRadioButton(string name) : base(name)
        {

        }
        public LuaRadioButton(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            if (GroupID <= 0) return ToSimpleLuaCode();
            return ToAdvancedLuaCode();
        }
        private string ToAdvancedLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty)
            {
                sb.Append($"local {Name} = ");
                sb.AppendLine(@"ui:element({");
                sb.AppendLine($"id = \"{ID}\", type = \"radio\",");
                sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
                sb.AppendLine($@" props = {{ text = ""{Text}"", selected = ""{Checked}"" }},");
                sb.AppendLine($@"style = {{check_color = ""{CheckColor}"", bg= ""{Background}"", font_size = {FontSize}, text = ""{TextColor}""}},");
                sb.AppendLine($"""
                on_click = function(playerName)
                    updateCheckedById(RadioButtonGroup{GroupID},"{ID}")
                end
                """);
                sb.AppendLine("})");
                sb.AppendLine($"table.insert(RadioButtonGroup{GroupID},{{obj = {Name}, id = \"{ID}\"}}");
            }
            else
            {
                sb.Append($"table.insert(RadioButtonGroup{GroupID},{{obj = ");
                sb.AppendLine(@"ui:element({");
                sb.AppendLine($"id = \"{ID}\", type = \"radio\",");
                sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
                sb.AppendLine($@" props = {{ text = ""{Text}"", selected = ""{Checked}"" }},");
                sb.AppendLine($@"style = {{check_color = ""{CheckColor}"", bg= ""{Background}"", font_size = {FontSize}, text = ""{TextColor}""}},");
                sb.AppendLine($"""
                on_click = function(playerName)
                    updateCheckedById(RadioButtonGroup{GroupID},"{ID}")
                end
                """);
                sb.AppendLine("})");
                sb.AppendLine($", id = \"{ID}\"}})");
            }

            return sb.ToString();
        }
        private string ToSimpleLuaCode()
        {
            StringBuilder sb = new();

            if (Name != string.Empty) sb.Append($"local {Name} = ");
            sb.AppendLine(@"ui:element({");
            sb.AppendLine($"id = \"{ID}\", type = \"radio\",");
            sb.AppendLine($"rect = {{unit =\"px\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}},");
            sb.AppendLine($@" props = {{ text = ""{Text}"", selected = ""{Checked}"" }},");
            sb.AppendLine($@"style = {{check_color = ""{CheckColor}"", bg= ""{Background}"", font_size = {FontSize}, text = ""{TextColor}""}},");
            sb.AppendLine($"""
                on_click = function(playerName)
                    
                end
                """);
            sb.AppendLine("})");

            return sb.ToString();
        }
    }
}
