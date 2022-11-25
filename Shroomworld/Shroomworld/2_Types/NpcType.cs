using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal class NpcType
    {
		public int Id => _id;
		public string Name => _name;
		public string PluralName => _pluralName;
		public ReadonlyHealthData HealthData => _healthData;
		public ReadonlyAttackData AttackData => _attackData;
		internal ReadonlyMovementData MovementData => _movementData;
		internal Quest Quest => _quest;

		public bool Friendly => _friendly;


		private readonly int _id;
		private readonly string _name;
		private readonly string _pluralName;
		private readonly ReadonlyHealthData _healthData;
		private readonly ReadonlyAttackData _attackData;
		private readonly ReadonlyMovementData _movementData;
		private readonly Quest _quest;

		private readonly bool _friendly;


		public NpcType(int id, string name, string pluralName, ReadonlyAttackData attackData, ReadonlyMovementData movementData, Quest quest)
		{
			_id = id;
			_name = name;
			_pluralName = pluralName;
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
