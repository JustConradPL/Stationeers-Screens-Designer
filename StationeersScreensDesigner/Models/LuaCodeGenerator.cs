using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Models
{
    public static class LuaCodeGenerator
    {
        /// <summary>
        /// Generates flexible Lua code for a UI element.
        /// Always includes id and type. Optionally includes rect, props, and style.
        /// Allows for injecting additional code and modifying the generated code.
        /// </summary>
        public static string BuildElementLuaCode(
            string id,
            string type,
            string? variableName = null,
            string? rect = null,
            string? props = null,
            string? style = null,
            string? extraTableFields = null,
            string? injectedCode = null,
            Func<string, string>? codeInterceptor = null)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(variableName))
            {
                sb.Append($"local {variableName} = ");
            }

            sb.AppendLine("ui:element({");
            sb.AppendLine($"    id = \"{id}\", type = \"{type}\",");

            if (!string.IsNullOrWhiteSpace(rect)) sb.AppendLine($"    rect = {rect},");
            if (!string.IsNullOrWhiteSpace(props)) sb.AppendLine($"    props = {props},");
            if (!string.IsNullOrWhiteSpace(style)) sb.AppendLine($"    style = {style},");
            if (!string.IsNullOrWhiteSpace(extraTableFields)) sb.AppendLine($"    {extraTableFields}");

            sb.AppendLine("})");

            if (!string.IsNullOrWhiteSpace(injectedCode))
            {
                sb.AppendLine(injectedCode);
            }

            string generatedCode = sb.ToString();

            // Change existing generated code before returning
            if (codeInterceptor != null)
            {
                generatedCode = codeInterceptor(generatedCode);
            }

            return generatedCode.TrimEnd();
        }

        public static string GenerateMainScript(IEnumerable<ScreenViewModel> screens)
        {
            var sb = new StringBuilder();
            sb.AppendLine("-- STATIONEERS SCREEN SCRIPT --");
            sb.AppendLine();

            sb.AppendLine("""
                local Event = {}
                Event.__index = Event

                function Event.new()
                    local instance = { listeners = {} }
                    return setmetatable(instance, Event)
                end

                -- Add a function to the event
                function Event:subscribe(func)
                    table.insert(self.listeners, func)
                end

                -- Run all subscribed functions
                function Event:fire(...)
                    for _, func in ipairs(self.listeners) do
                        func(...)
                    end
                end
                """);

            var superCollection = screens.SelectMany(
                parent => parent.CanvasElements,
                (parent, child) => new { Item = child, Source = parent }
            ).ToList();

            if (superCollection.Any(element => element.Item is LuaRadioButton))
            {
                var tables = superCollection
                    .Where(elements => elements.Item is LuaRadioButton e && e.GroupID > 0)
                    .GroupBy(element => ((LuaRadioButton)element.Item).GroupID)
                    .Select(gp => gp.ToList())
                    .ToList();

                foreach (var table in tables)
                {
                    sb.AppendLine($"local RadioButtonGroup{((LuaRadioButton)table[0].Item).GroupID} = {{}}");
                }

                sb.AppendLine("""
                    local function updateCheckedById(objectTable, targetId,UI)
                        for i, entry in ipairs(objectTable) do
                            -- Access the 'obj' and 'id' from the entry table
                            local handler = entry.obj
                            local ID = entry.id

                            if ID == targetId then
                                handler:set_props({selected = true})
                            else
                                handler:set_props({selected = false})
                            end
                        end
                        UI:commit()
                    end
                    """);
            }
            sb.AppendLine();

            foreach (var currentScreen in superCollection.GroupBy(screen => screen.Source))
            {
                sb.Append("--");
                sb.AppendLine($"   {currentScreen.Key.Name}   ".PadLeft((48 - currentScreen.Key.Name.Length) / 2 + currentScreen.Key.Name.Length, '=').PadRight(48, '='));
                sb.AppendLine(currentScreen.Key.GeneratedCode);
            }

            sb.AppendLine();
            return sb.ToString();
        }

        public static string GenerateScreenScript(
            string screenName,
            IEnumerable<LuaUIElement> elements,
            Func<LuaUIElement, string?>? externalInjector = null,
            Func<LuaUIElement, string, string>? externalInterceptor = null)
        {
            var sb = new StringBuilder();
            string safeName = screenName.Replace(" ", "_");
            string uiVar = $"{safeName}UI";

            sb.AppendLine();
            sb.AppendLine($"local {uiVar} = ss.ui.surface(\"{screenName}\")");
            sb.AppendLine($"ss.ui.activate(\"{screenName}\")");
            sb.AppendLine($"local size = {uiVar}:size()");
            sb.AppendLine("local W, H = size.w, size.h\n");

            foreach (var element in elements.OrderBy(el => el.ZIndex))
            {
                string luaCode = element.ToLuaCode();

                if (externalInjector != null)
                {
                    string? extInjected = externalInjector(element);
                    if (!string.IsNullOrWhiteSpace(extInjected)) luaCode += "\n" + extInjected;
                }

                if (externalInterceptor != null) luaCode = externalInterceptor(element, luaCode);

                luaCode = luaCode
                    .Replace("ui:", $"{uiVar}:", StringComparison.OrdinalIgnoreCase)
                    .Replace("ui_", uiVar);

                sb.AppendLine(luaCode);
            }

            sb.AppendLine("-- Initialize and run the screen");
            sb.AppendLine($"{uiVar}:commit()");

            return sb.ToString();
        }
    }
}