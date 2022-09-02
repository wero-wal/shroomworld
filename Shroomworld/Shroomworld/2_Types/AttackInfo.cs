using System;

namespace Shroomworld
{
	internal class AttackInfo
	{
		// ---------- Enums ----------
		// ---------- Properties ----------
		// ---------- Fields ----------
		private readonly int _strength;
		private readonly int _range;
		private readonly int _speed;
		private readonly int _cooldown;

		// ---------- Constructors ----------
		public AttackInfo(int strength, int range, int speed, int cooldown)
		{
			_strength = strength;
			_range = range;
			_speed = speed;
			_cooldown = cooldown;
		}

		// ---------- Methods ----------
		
	}
}
