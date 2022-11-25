using System;

namespace Shroomworld
{
    internal class PowerUp // note: i want powerups to be shield, damage, speed
    {
        public int Value { get => _level * _multiplier; }
        

        private static int _minLevel;
        private static int _maxLevel;   
        private static int _multiplier;
        
        private int _level;

        //-----methods-----
        public PowerUp(int level)
        {
            _level = level;
        }


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
        public override string ToString()
        {
            return _level.ToString();
        }
    }
}
