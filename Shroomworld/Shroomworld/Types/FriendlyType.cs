using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class FriendlyType
    {
		public int Id => _idData.Id;
		public string Name => _idData.Name;
		public string PluralName => _idData.PluralName;
		public ReadonlyHealthData HealthData => _healthData;
		public ReadonlyAttackData AttackData => _attackData;
		internal ReadonlyMovementData MovementData => _movementData;
		internal Quest Quest => _quest;

		public bool Friendly => _friendly;


		private readonly IdData _idData;
		private readonly ReadonlyHealthData _healthData;
		private readonly ReadonlyAttackData _attackData;
		private readonly ReadonlyMovementData _movementData;
		private readonly Quest _quest;

		private readonly bool _friendly;


		public FriendlyType(IdData idData, ReadonlyAttackData attackData, ReadonlyMovementData movementData, Quest quest)
		{
			_idData = idData;
			_attackData = attackData;
			_movementData = movementData;
			_quest = quest;

			_friendly = attackData is null;
		}


        //public Npc NewNpc()
        //{
        //    return new Npc(FileManager.LoadTexture(FileManager.NpcDirectory, _id), _movementSpeed, _constantOfRestitution);
        //}
    }
}
