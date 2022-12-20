namespace Shroomworld
{
	internal class ToolItem : InventoryItem
	{
		public static int[] Types => _types;
		private static int[] _types; // possible types of tool (i.e. axe, pickaxe, sword, etc.).

		// ---------- Fields ----------
		private readonly int _type; // type of this specific tool.
		private readonly int _level; // the strength or effectiveness or speed of this tool (where lower = worse).
		
		// ---------- Constructors ----------
		public ToolItem(IdentifyingData idData, int type, int level) : base(idData)
		{
			_type = type;
			_level = level;
		}
	}
}