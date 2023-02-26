using System;
namespace Shroomworld {
    // Note: i want powerups to be shield, damage, speed.
    internal class PowerUp {
        // ----- Properties -----
        public int Value { get => _level * s_multiplier; }
        

        // ----- Fields -----
        private static int s_minLevel;
        private static int s_maxLevel;   
        private static int s_multiplier;
        
        private int _level;


        // ----- Constructors -----
        public PowerUp(int level) {
            _level = level;
        }


        //----- Methods -----
        public static void SetMinAndMaxLevel(int min, int max) {
            // Check whether they've been set.
            if (s_minLevel == s_maxLevel) {
                s_minLevel = min;
                s_maxLevel = max;
            }
        }
        
        public override string ToString() {
            return _level.ToString();
        }
        public void IncreaseLevel() {
            if (_level < MaxLevel) {
                _level++;
            }
        }
    }
}
