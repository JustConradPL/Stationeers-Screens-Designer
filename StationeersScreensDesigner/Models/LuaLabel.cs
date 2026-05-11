﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaLabel : LuaUIElement
    {
        [PropertyMetadata("Text", "Label")]
        public string Content
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("FontSize", "Label")]
        public int FontSize
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Color", "Label")]
        public string Color
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }
        [PropertyMetadata("Align", "Label")]
        public string Align
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        public LuaLabel() : base(string.Empty)
        {
        }
        public LuaLabel(string name) : base(name)
        {
        }

        public LuaLabel(string name, string id) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            return LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "label",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{text = \"{Content}\"}}",
                style: $"{{ font_size = {FontSize}, color = \"{Color}\", align = \"{Align}\" }}",
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
        }
    }
}
