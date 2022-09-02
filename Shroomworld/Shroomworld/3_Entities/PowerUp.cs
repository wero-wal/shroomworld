using System;

namespace Shroomworld
{
    internal class PowerUp
    {
        // ---------- Properties ----------
        public int Value { get => _level * _multiplier; }
        
        // ---------- Fields ----------
        // static
        private static int _minLevel;
        private static int _maxLevel;   
        private static int _multiplier;
        
        // non-static
        private int _level;

        // ---------- Constructors ---------- 
        public PowerUp(int level = MinLevel)
        {
            _level = level;
        }

        // ---------- Methods ----------
        public static void SetMinAndMaxLevel(int min, int max)
        {
            if (_minLevel == _maxLevel) // haven't been set yet
            {
                _minLevel = min;
                _maxLevel = max;
            }
        }
        
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
