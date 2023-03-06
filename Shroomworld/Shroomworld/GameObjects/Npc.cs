using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Npc : Entity
    {
		// Properties
		public AttackData AttackData => _type.AttackData;
		public HealthData HealthData => _healthData;
		public PhysicsData MovementData => _movementData;
		public bool AttackCooldownFinished => (DateTime.Now - _startOfAttack).TotalMilliseconds > _type.AttackData.Cooldown;

		// Fields
		public event Action<Entity, AttackData> AttackAttemptInitiated;

		private readonly FriendlyType _type;
        private readonly HealthData _healthData;
        private readonly PhysicsData _movementData;

		private DateTime _startOfAttack;

		// Constructors
		public Npc(FriendlyType type, HealthData healthData = null, PhysicsData movementData = null)
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
			return (_sprite.Position - opponent.Sprite.Position).Length() <= _type.AttackData.Range;
		}
	}
}
