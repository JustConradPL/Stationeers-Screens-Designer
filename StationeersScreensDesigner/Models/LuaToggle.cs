﻿using System;
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
            string preCode = $"local {ID}_previousValue\nlocal {ID}_OnToggle = Event.new()\n";
            string elementCode = LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "toggle",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{value = \"{Value}\"}}",
                style: $"{{on_color = \"{OnColor}\", off_color = \"{OffColor}\", knob = \"{KnobColor}\"}}",
                extraTableFields: $$"""
                on_click = function(playerName)
                    {{ID}}_previousValue = not {{ID}}_previousValue
                    ui:get("{{ID}}"):set_props({value = {{ID}}_previousValue})
                    {{ID}}_OnToggle:fire({{ID}}_previousValue)
                end
                """,
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
            return preCode + elementCode;
        }
    }
}
