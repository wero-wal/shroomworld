using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Npc
    {
		// Properties
		public ReadonlyAttackData AttackData => _type.AttackData;
		public HealthData HealthData => _healthData;
		public MovementData MovementData => _movementData;
		public bool AttackCooldownFinished => (DateTime.Now - _startOfAttack).TotalMilliseconds > _type.AttackData.Cooldown;

		// Fields
		public event Action<Entity, ReadonlyAttackData> AttackAttemptInitiated;

		private readonly NpcType _type;
        private readonly HealthData _healthData;
        private readonly MovementData _movementData;

		private DateTime _startOfAttack;

		// Constructors
		public Npc(NpcType type, HealthData healthData = null, MovementData movementData = null)
		{
			_type = type;
			_healthData = healthData;
			_movementData = movementData;
		}

		// Methods
		public void TryToInitiateAttack()
		{
			if (!AttackCooldownFinished) return; // don't even attempt to attack if the cooldown isn't up
			AttackAttemptInitiated?.Invoke(this, _type.AttackData);
		}
		public void InitiateAttack()
		{
			_startOfAttack = DateTime.Now;
		}
		public bool IsInRange(Entity opponent)
		{
			return _movementData.Position.DistanceTo(opponent.MovementData.Position) <= _type.AttackData.Range;
		}
	}
}
