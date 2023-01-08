using System;
using System.Collections.Generic;
namespace Shroomworld
{
	internal class BiomeDictionary : IEnumerable
	{
		public class CapacityReachedException : Exception
		{

		}

		private readonly int[] _biomeStartXs;
		private readonly BiomeType[] _biomeTypes;

		private readonly int _end = 0;
		private readonly int _size;
		public int this[int xValue]
		{
			get
			{
				for (int i = 0; i < _size; i++)
				{
					if (xValue <= _biomeStartXs[i])
					{
						return _biomeTypes[i];
					}
				}
				throw new KeyNotFoundException();
			}
			set
			{
				if (!_biomeStartXs.Contains(xValue))
				{
					throw new KeyNotFoundException();
				}

				_biomeTypes = value;
			}
		}

		public BiomeDictionary(int size)
		{
			_size = size;
			_biomeStartXs = new int[size];
			_biomeTypes = new BiomeType[size];
		}

		public void Add(int x, BiomeType biomeType)
		{
			if (_end == _size)
			{
				throw new CapacityReachedException();
			}

			_biomeStartXs[_end++] = x;
			_biomeTypes[_end++] = biomeType;
		}
	}
}