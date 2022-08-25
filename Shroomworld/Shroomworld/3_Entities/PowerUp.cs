using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class PowerUp
    {
        // ---------- Enums ----------
        public enum Types
        {
            Health,
            Damage,
            Speed,
        }

        // ---------- Properties ----------
        public byte Level { get => _level; }
        public bool Active { get => _level == _minLevel; }

        // ---------- Fields ----------
        private static byte _minLevel;
        private static byte _maxLevel;

        private readonly Types _type;

        private byte _level;

        // ---------- Constructors ----------
        public PowerUp(Types type)
        {
            _type = type;
        }
        public PowerUp(Types type, byte level)
        {
            _type = type;
            _level = level;
        }

        // ---------- Methods ----------
        public static void SetMinAndMaxLevel(byte min, byte max)
        {
            _minLevel = min;
            _maxLevel = max;
        }

        public void IncreaseLevel()
        {
            _level = Math.Clamp(++_level, _minLevel, _maxLevel);
        }
        public void Reset()
        {
            _level = _minLevel;
        }
    }
}
