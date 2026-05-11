﻿using System;
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
            return LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "button",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{text = \"{Text}\"}}",
                style: $"{{ bg = \"{Background}\", text = \"{TextColor}\", font_size = {FontSize} }}",
                extraTableFields: "on_click = function(playerName)\n\t--Clicked logic\nend",
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
        }
    }
}
