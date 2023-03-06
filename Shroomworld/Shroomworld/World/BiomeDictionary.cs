using System;
using System.Collections.Generic;
using System.Linq;

namespace Shroomworld;

/// <summary>
/// Use to store biomes and their locations in a world.
/// </summary>
public class BiomeDictionary {
	public class CapacityReachedException : Exception { }

	// ----- Properties -----
	/// <summary>
	/// 
	/// </summary>
	/// <param name="x">x-coordinate at which you want to know the biome.</param>
	/// <returns>The biome at the given <paramref name="x"/> coordinate.</returns>
	/// <exception cref="KeyNotFoundException">Thrown if not enough biomes have been set.</exception>
	public BiomeType this[int x] {
		get {
			for (int i = 0; i < _size; i++) {
				if (x <= _biomeStartXs[i]) {
					return _biomeTypes[i];
				}
			}
			throw new KeyNotFoundException();
		}
		set {
			if (!_biomeStartXs.Contains(x)) {
				throw new KeyNotFoundException();
			}
			_biomeTypes[x] = value;
		}
	}

	// ----- Fields -----
	private readonly int[] _biomeStartXs;
	private readonly BiomeType[] _biomeTypes;

	private readonly int _size;
	
	private int _endPointer = 0;

	// ----- Constructors -----
	public BiomeDictionary(int size) {
		_size = size;
		_biomeStartXs = new int[size];
		_biomeTypes = new BiomeType[size];
	}

	// ----- Methods -----
	public void Add(int x, BiomeType biomeType) {
		if (_endPointer == _size) {
			throw new CapacityReachedException();
		}
		_biomeStartXs[_endPointer++] = x;
		_biomeTypes[_endPointer++] = biomeType;
	}
}