﻿using System;
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
            bool hasName = Name != string.Empty;

            string elementCode = LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "radio",
                variableName: hasName ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{ text = \"{Text}\", selected = \"{Checked}\" }}",
                style: $"{{check_color = \"{CheckColor}\", bg= \"{Background}\", font_size = {FontSize}, text = \"{TextColor}\"}}",
                extraTableFields: $"""
                on_click = function(playerName)
                    updateCheckedById(RadioButtonGroup{GroupID},"{ID}",ui{(hasName ? "" : "_")})
                end
                """,
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );

            if (hasName)
            {
                return elementCode + $"\ntable.insert(RadioButtonGroup{GroupID}, {{obj = {Name}, id = \"{ID}\"}})";
            }
            else
            {
                return $"table.insert(RadioButtonGroup{GroupID}, {{obj = \n{elementCode}\n, id = \"{ID}\"}})";
            }
        }
        private string ToSimpleLuaCode()
        {
            return LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "radio",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{ text = \"{Text}\", selected = \"{Checked}\" }}",
                style: $"{{check_color = \"{CheckColor}\", bg= \"{Background}\", font_size = {FontSize}, text = \"{TextColor}\"}}",
                extraTableFields: "on_click = function(playerName)\n    \nend",
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
        }
    }
}
