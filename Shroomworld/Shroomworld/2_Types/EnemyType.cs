using Shroomworld._2_Types;
using System;

namespace Shroomworld
{
	internal abstract class EnemyType : EntityType, IDamageableType
	{
		// ---------- Enums ----------
		// ---------- Properties ----------
		// ---------- Fields ----------
		private readonly int _attackStrength;
		private readonly int _attackRange;
		private readonly int _attackSpeed;
		private readonly int _attackCooldown;

		// ---------- Constructors ----------
		public EnemyType(string plainText)
		{
			string[] parts = plainText.Split(File.Separators[File.Primary]);
			int i = 0;
			ParseNamesAndId(ref i, parts);

			_movementSpeed = Convert.ToInt32(parts[i++]);
			_constantOfRestitution = Convert.ToSingle(parts[i++]);

			_maxHealth = Convert.ToInt32(parts[i++]);
			_regenAmountPerSecond = Convert.ToInt32(parts[i++]);

			_attackStrength = Convert.ToInt32(parts[i++]);
			_attackRange = Convert.ToInt32(parts[i++]);
			_attackSpeed = Convert.ToInt32(parts[i++]);
			_attackCooldown = Convert.ToInt32(parts[i++]);

			AddToDictionary();
		}

		// ---------- Methods ----------
		public AttackInfo GetAttackInfo()
		{
			return AttackInfo.CreateNew(_attackStrength, _attackRange, _attackSpeed, _attackCooldown);
		}
	}
}