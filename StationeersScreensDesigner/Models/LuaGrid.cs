namespace StationeersScreensDesigner.Models
{
    public class LuaGrid : LuaUIElement
    {
        public LuaGrid() : base(string.Empty, null)
        {

        }
        public LuaGrid(string name, string? id = null) : base(name, id)
        {
        }

        public override string ToLuaCode()
        {
            throw new NotImplementedException();
        }
    }
}