using System;

namespace Shroomworld
{
	internal class NpcType
	{
		// ----- Enums -----
		// ----- Properties -----
		// ----- Fields -----
		private readonly int _id;
		private readonly string _name;
		private readonly string _pluralName;
		private readonly AttackData _attackData;
		private readonly MovementData _movementData;
		private readonly Quest _quest;

		// ----- Constructors -----
		

		// ----- Methods -----
		public AttackData GetAttackInfo()
		{
			return AttackData.CreateNew(_attackStrength, _attackRange, _attackSpeed, _attackCooldown);
		}
	}
}