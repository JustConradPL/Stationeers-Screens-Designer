﻿using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace StationeersScreensDesigner.Models
{
    public class LuaIcon : LuaUIElement
    {
        [PropertyMetadata("Icon Name", "Icon")]
        public string IconName
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(IconName));
            }
        }

        [PropertyMetadata("Color Index", "Icon")]
        public int ColorIndex
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(ColorIndex));
            }
        }

        [PropertyMetadata("Tint", "Icon")]
        public string Tint
        {
            get; set
            {
                field = value;
                OnPropertyChanged(nameof(Tint));
            }
        }



        public LuaIcon() : base(string.Empty)
        {

        }
        public LuaIcon(string name) : base(name)
        {

        }
        public LuaIcon(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            return LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "Icon",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                props: $"{{name = {IconName}, color_index = {ColorIndex}}}",
                style: $"{{tint = \"{Tint}\"}}",
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
        }
    }
}
