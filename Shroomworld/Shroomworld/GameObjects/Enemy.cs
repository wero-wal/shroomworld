using System;
using System.Collections.Generic;
using Shroomworld.Physics;

namespace Shroomworld {
	public class Enemy {

		// ----- Enums -----
		// ----- Properties -----
		public int Id => _id;
		public EntityHealthData HealthData => _healthData;


		// ----- Fields -----
		private readonly int _id;
		private readonly Sprite _sprite;
		private readonly Body _body;
		private readonly EntityHealthData _healthData;


		// ----- Constructors -----
		public Enemy(int id, Sprite sprite, Body body, EntityHealthData healthData) {
			_id = id;
			_sprite = sprite;
			_body = body;
			_healthData = healthData;
		}

		// ----- Methods -----
	}
}
