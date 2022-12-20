namespace Shroomworld
{
	internal class IdentifyingData
	{
		public int Id => _id;
		public string Name => _name;
		public string PluralName => _pluralName;

		private readonly int _id;
		private readonly string _name;
		private readonly string _pluralName;

		public IdentifyingData(int id, string name, string pluralName = null)
		{
			_id = id;
			_name = name;
			_pluralName = pluralName;
		}
	}
}