﻿using System;
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
        public LuaCheckbox(string name, string id) : base(name, id)
        {

        }

        public override string ToLuaCode()
        {
            string preCode = $"local {ID}_previousValue\nlocal {ID}_OnToggle = Event.new()\n";
            string elementCode = LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "checkbox",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{ text = \"{Text}\", checked = \"{Checked}\" }}",
                style: $"{{check_color = \"{CheckColor}\", bg= \"{Background}\", font_size = {FontSize}, text = \"{TextColor}\"}}",
                extraTableFields: $$"""
                on_click = function(playerName)
                    {{ID}}_previousValue = not {{ID}}_previousValue
                    ui:get("{{ID}}"):set_props({checked = {{ID}}_previousValue})
                    {{ID}}_OnToggle:Fire({{ID}}_previousValue)
                end
                """,
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
            return preCode + elementCode;
        }
    }
}
