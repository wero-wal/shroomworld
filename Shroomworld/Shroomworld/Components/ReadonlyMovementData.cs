using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
	internal class ReadonlyMovementData
	{
		public int MovementForce => _movementForce;
		public int Mass => _mass;


		private int _movementForce;
		private int _mass;
	}
}
