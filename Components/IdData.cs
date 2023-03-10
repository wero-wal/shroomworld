namespace Shroomworld {
	/// <summary>Data about the ID of a type.</summary>
	public class IdData {

		// ----- Properties -----
		public int Id => _id;
		public string Name => _name;
		public string PluralName => _pluralName;


		// ----- Fields -----
		private readonly int _id;
		private readonly string _name;
		private readonly string _pluralName;


		// ----- Constructors -----
		public IdData(int id, string name, string pluralName = null) {
			_id = id;
			_name = name;
			_pluralName = pluralName;
		}
	}
}
