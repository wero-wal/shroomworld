using System;

namespace Shroomworld
{
	internal abstract class EnemyType : Type, IDamageableType
	{
		// ---------- Enums ----------
		// ---------- Properties ----------
		// ---------- Fields ----------
		private readonly int _movementSpeed;
		private readonly float _constantOfRestitution;

		private readonly int _maxHealth;
		private readonly int _regenAmountPerSecond;

		private readonly int _attackStrength;
		private readonly int _attackRange;
		private readonly int _attackSpeed;
		private readonly int _attackCooldown;

		// ---------- Constructors ----------
		public EnemyType(string plainText)
		{
			string[] parts = plainText.Split(File.Separator_Chars[File.Primary]);
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
		public Sprite GetSprite()
		{
			return new MoveableSprite(File.LoadTexture(File.EnemyDirectory, _name));
		}
		public HealthInfo GetHealthInfo()
		{
			return HealthInfo.CreateNew(_maxHealth, _regenAmountPerSecond);
		}
		public AttackInfo GetAttackInfo()
		{
			return AttackInfo.CreateNew(_attackStrength, _attackRange, _attackSpeed, _attackCooldown);
		}
	}
}