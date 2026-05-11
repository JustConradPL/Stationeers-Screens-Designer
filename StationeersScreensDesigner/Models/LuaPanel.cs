using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaPanel : LuaUIElement
    {
        [PropertyMetadata("Background", "Panel")]
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
            return LuaCodeGenerator.BuildElementLuaCode(
                id: ID,
                type: "panel",
                variableName: Name != string.Empty ? Name : null,
                rect: $"{{unit =\"{Unit}\", x = {X}, y ={Y},\n w = {Width}, h = {Height}}}",
                style: $"{{bg = \"{Background}\"}}",
                injectedCode: InjectedCode,
                codeInterceptor: CodeInterceptor
            );
        }
    }
}
