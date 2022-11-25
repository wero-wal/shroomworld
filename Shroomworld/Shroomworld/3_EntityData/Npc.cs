using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class Npc
    {
		public bool AttackCooldownUp => (DateTime.Now - _startOfAttack).TotalMilliseconds > _type.AttackData.Cooldown;
		public int PercentHealth => (int) (100 * _healthData.Health / _type.HealthData.MaxHealth);


		private readonly NpcType _type;
        private readonly HealthData _healthData;
        private readonly MovementData _movementData;

		private DateTime _startOfAttack;


		public Npc(NpcType type, HealthData healthData = null, MovementData movementData = null)
		{
			_type = type;
			_healthData = healthData;
			_movementData = movementData;
		}


		public void InitiateAttack()
		{
			_startOfAttack = DateTime.Now;
		}
	}
}
