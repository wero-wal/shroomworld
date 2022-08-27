using System;

namespace Shroomworld
{
    internal class PowerUp
    {
        // ---------- Properties ----------
        public byte Value { get => _level * _multiplier; }
        
        // ---------- Fields ----------
        private static byte _minLevel;
        private static byte _maxLevel;   
        private static byte _multiplier;
        
        private byte _level;

        // ---------- Constructors ---------- 
        public PowerUp(byte level = MinLevel)
        {
            _level = level;
        }

        // ---------- Methods ----------
        public void IncreaseLevel()
        {
            if (_level < MaxLevel)
            {
                _level++;
            }
        }

        public string ToString()
        {
            return _level.ToString();
        }
    }
}
