namespace Shroomworld
{
	internal class PowerUps
    {
		// ---------- Enums ----------
		private enum Type
		{
			Shield,
			Damage,
			Speed,
			Count,
		}

		// ---------- Properties ----------
        public byte Shield { get => _powerUps[(byte)Type.Shield].Value; }
        public byte Damage { get => _powerUps[(byte)Type.Damage].Value; }
        public byte Speed { get => _powerUps[(byte)Type.Speed].Value; }
        
		// ---------- Fields ----------
        private PowerUp[] _powerUps;

		// ---------- Constructors ----------
		public PowerUps(string plainText)
		{
            string[] split = plainText.Split(' ');
            byte[] levels = new byte[split.Length];
            for(int i = 0; i < split.Length; i++)
            {
                levels[i] = Convert.ToByte(split[i]);
            }

			_powerUps = new PowerUps[levels.Length];
			for(int i = 0; i < _powerUps.Length; i++)
			{
				_powerUps[i] = new PowerUp(levels[i]);
			}
		}
		private PowerUps()
		{
			for(int i = 0; i < (byte)Type.Count; i++)
			{
				_powerUps[i] = new PowerUp();
			}
		}

		// ---------- Methods ----------
		public static PowerUps CreateNew()
		{
			return new PowerUps();
		}
		public string ToString()
		{
			return FileFormatter.FormatAsPlainText(_powerUps, FileFormatter.SecondarySeparator);
		}
    }    
}
