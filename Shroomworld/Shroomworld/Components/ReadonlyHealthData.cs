using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
	internal class ReadonlyHealthData // todo: put key in readme (Readonly prefix means the data is constant)
	{
		public int MaxHealth => _maxHealth;
		public int RegenerationAmount => _regenerationAmount;


		private int _maxHealth;
		private int _regenerationAmount;
	}
}
