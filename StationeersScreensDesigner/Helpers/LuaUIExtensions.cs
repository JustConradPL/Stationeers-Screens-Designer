using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using StationeersScreensDesigner.Models;

namespace StationeersScreensDesigner.Helpers
{
    public static class LuaUIExtensions
    {
        public static LuaUIElement Clone(this LuaUIElement source)
        {
            if (source == null) return null;

            Type type = source.GetType();


            var clone = Activator.CreateInstance(type) as LuaUIElement;

            if (clone == null) throw new ArgumentNullException("Couldn't clone because the result is a null");

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                if (prop.CanWrite && prop.CanRead)
                {
                    var value = prop.GetValue(source);
                    prop.SetValue(clone, value);
                }
            }

            return clone;
        }
    }
}
