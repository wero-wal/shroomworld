namespace Shroomworld
{
	internal class PowerUps
    {
		// ----- Properties -----
        public int Shield { get => _powerUps[_shield].Value; }
        public int Damage { get => _powerUps[_damage].Value; }
        public int Speed { get => _powerUps[_speed].Value; }
        
		// ----- Fields -----
		// constants
		private const int _shield = 0;
		private const int _damage = 1;
		private const int _speed = 2;
		private const int _numOfPowerUps = 4;

		// variables
        private PowerUp[] _powerUps;

		// ----- Constructors -----
		public PowerUps(string plainText)
		{
            string[] parts = plainText.Split(' ');
            int[] levels = new int[parts.Length];
            for(int i = 0; i < parts.Length; i++)
            {
                levels[i] = Convert.ToInt32(parts[i]);
            }

			_powerUps = new PowerUps[levels.Length];
			for(int i = 0; i < _powerUps.Length; i++)
			{
				_powerUps[i] = new PowerUp(levels[i]);
			}
		}
		private PowerUps()
		{
			for(int i = 0; i < _numOfPowerUps; i++)
			{
				_powerUps[i] = new PowerUp();
			}
		}

		// ----- Methods -----
		public static PowerUps CreateNew()
		{
			return new PowerUps();
		}
		
		public string ToString()
		{
			return FileFormatter.FormatAsPlainText(_powerUps, separatorLevel: FileFormatter.Secondary);
		}
    }    
}
