namespace Shroomworld {
	// todo: put key in readme (Readonly prefix means the data is constant)
	/// <summary>Data about the health of a type of entity.</summary>
	public class HealthData {

		// ----- Properties -----
		public int MaxHealth => _maxHealth;
		/// <value>How much to increase the entity's health by each second.</value>
		public int RegenerationPerSecond => _regenerationPerSecond;


		// ----- Fields -----
		private readonly int _maxHealth;
		private readonly int _regenerationPerSecond;


		// ----- Constructors -----
		public HealthData(int maxHealth, int regenerationAmountPerSecond) {
			_maxHealth = maxHealth;
			_regenerationPerSecond = regenerationAmountPerSecond;
		}
	}
}
